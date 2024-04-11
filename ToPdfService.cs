using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToPdf.Builders;
using ToPdf.Helpers;
using ToPdf.Models;

namespace ToPdf
{
    public class ToPdfService
    {
        private const string BaseUrl = "https://api.2pdf.io";
        private const string ApiVer = "v1";
        private readonly string _apiKey;
        private string _sourceData;
        private string _endpoint;
        private ParametersBuilder<ParameterPdfKey> _parameterPdfBuilder;
        private ParametersBuilder<ParameterImageKey> _parameterImageBuilder;
        private ExpandoObject _requestDataBody;
        
        public ToPdfService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public ParametersBuilder<ParameterPdfKey> UrlToPdf(string sourceData)
        {
            _parameterPdfBuilder = new ParametersBuilder<ParameterPdfKey>(this);
            _endpoint = $"{BaseUrl}/{ApiVer}/url/pdf";
            _sourceData = sourceData;
            return _parameterPdfBuilder;
        }

        public ParametersBuilder<ParameterPdfKey> HtmlToPdf(string sourceData)
        {
            _parameterPdfBuilder = new ParametersBuilder<ParameterPdfKey>(this);
            _endpoint = $"{BaseUrl}/{ApiVer}/html/pdf";
            _sourceData = sourceData;
            return _parameterPdfBuilder;
        }

        public ParametersBuilder<ParameterImageKey> UrlToImage(string sourceData)
        {
            _parameterImageBuilder = new ParametersBuilder<ParameterImageKey>(this);
            _endpoint = $"{BaseUrl}/{ApiVer}/url/image";
            _sourceData = sourceData;
            return _parameterImageBuilder;
        }

        public ParametersBuilder<ParameterImageKey> HtmlToImage(string sourceData)
        {
            _parameterImageBuilder = new ParametersBuilder<ParameterImageKey>(this);
            _endpoint = $"{BaseUrl}/{ApiVer}/html/image";
            _sourceData = sourceData;
            return _parameterImageBuilder;
        }

        public async Task<PdfDocumentModel> SendAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                    var requestData = new ExpandoObject();
                    var expandoDict = (IDictionary<string, object>)requestData;
                    expandoDict.Add("source", _sourceData);
                    var paramList = _parameterPdfBuilder?.Parameters == null ? _parameterImageBuilder?.Parameters : default;
                    if (paramList != null)
                    {
                        foreach (var pair in paramList)
                        {
                            expandoDict.Add(pair.Key.ToString().ToSnakeCase(), pair.Value);
                        }
                    }

                    var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(_endpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var bytes = await response.Content.ReadAsByteArrayAsync();
                        var format = response.Content.Headers
                            .FirstOrDefault(x => x.Key == "Content-Type").Value
                            .FirstOrDefault()?.ToString().Split('/')[1];
                        var document = new PdfDocumentModel
                        {
                            Bytes = bytes,
                            Format = format
                        };
                    
                        return document;
                    }
                    else
                    {
                        throw new Exception($"Failed to convert to PDF. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to convert to PDF. Status code: {e.Message}");
            }
        }
        
        public async void Send(Action<PdfDocumentModel> onComplete, Action<PdfError> onError)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                    var content = new StringContent(JsonConvert.SerializeObject(_requestDataBody), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(_endpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var bytes = await response.Content.ReadAsByteArrayAsync();
                        var format = response.Content.Headers
                            .FirstOrDefault(x => x.Key == "Content-Type").Value
                            .FirstOrDefault()?.ToString().Split('/')[1];
                        var document = new PdfDocumentModel
                        {
                            Bytes = bytes,
                            Format = format
                        };
                    
                        onComplete.Invoke(document);
                    }
                    else
                    {
                        onError.Invoke(new PdfError
                        {
                            Message = $"Failed to convert to PDF. Status code: {response.StatusCode}",
                            Details = response.ReasonPhrase,
                        });
                    }
                }
            }
            catch (Exception e)
            {
                onError.Invoke(new PdfError
                {
                    Message = $"Failed to convert to PDF. Status code: {e.Message}",
                    Details = e.StackTrace,
                });
            }
        }
    }
}