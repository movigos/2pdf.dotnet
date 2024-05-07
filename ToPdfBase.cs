using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToPdf.Helpers;
using ToPdf.Models;

namespace ToPdf
{
    public abstract class ToPdfBase<T>
    {
        public string BaseUrl => "https://api.2pdf.io";
        public string ApiVersion { get; private set; } = "v1.0";
        public string Route { get; }
        public string ApiKey { get; }

        public Dictionary<T, object> Parameters { get; set; } = new Dictionary<T, object>();
        
        public ToPdfBase(string apiKey, string route)
        {
            ApiKey = apiKey;
            Route = route;
        }
        
        public ToPdfBase<T> AddParameters(Dictionary<T, object> parameters)
        {
            Parameters = parameters;
            return this;
        }

        public ToPdfBase<T> AddParameter(T key, object value)
        {
            Parameters.Add(key, value);
            return this;
        }

        public ToPdfBase<T> SetApiVersion(string apiVersion)
        {
            ApiVersion = apiVersion;
            return this;
        }

        public virtual async Task<DocumentModel> SendAsync(string sourceData)
        {
            try
            {
                var endpoint = $"{BaseUrl}/{ApiVersion}/{Route}";
                
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

                    var requestData = new ExpandoObject();
                    var expandoDict = (IDictionary<string, object>)requestData;
                    expandoDict.Add("source", sourceData);
                    if (Parameters != null)
                    {
                        foreach (var pair in Parameters)
                        {
                            expandoDict.Add(pair.Key.ToString().ToSnakeCase(), pair.Value);
                        }
                    }

                    var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(endpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var bytes = await response.Content.ReadAsByteArrayAsync();
                        var format = response.Content.Headers
                            .FirstOrDefault(x => x.Key == "Content-Type").Value
                            .FirstOrDefault()?.ToString().Split('/')[1];
                        var document = new DocumentModel
                        {
                            Bytes = bytes,
                            Format = format
                        };
                    
                        return document;
                    }

                    throw new Exception($"Failed to convert to PDF. Status code: {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to convert to PDF. Status code: {e.Message}");
            }
        }
    }
}