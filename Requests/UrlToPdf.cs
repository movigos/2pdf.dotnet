using System;
using System.Threading.Tasks;
using ToPdf.Models;

namespace ToPdf.Requests
{
    public class UrlToPdf : ToPdfBase<ParameterPdfKey>
    {
        public UrlToPdf(string apiKey) : base(apiKey, "url/pdf") { }

        public override Task<DocumentModel> SendAsync(string sourceData)
        {
            if (Uri.IsWellFormedUriString(sourceData, UriKind.Absolute))
            {
                return base.SendAsync(sourceData);
            }

            throw new Exception("This Url is not correct");
        }
    }
}