using System.Collections.Generic;
using System.Threading.Tasks;
using ToPdf.Helpers;
using ToPdf.Models;
using ToPdf.Requests;

namespace ToPdf.Examples
{
    public class ToPdfExample
    {
        private string _apiKey = "api_key";
        
        public ToPdfExample()
        {
        }

        public async Task HtmlToPdf()
        {
            var toPdfService = new HtmlToPdf(_apiKey);
            var pdfDocument = await toPdfService.SendAsync(HtmlSource.HtmlContent);
            pdfDocument.SaveToFile("html_document.pdf");
        }

        public async Task UrlToPdf()
        {
            var toPdfService = new UrlToPdf(_apiKey);
            var pdfDocument = await toPdfService.SendAsync(HtmlSource.HtmlContent);
            pdfDocument.SaveToFile("url_document.pdf");
        }

        public async Task HtmlToImage()
        {
            var toPdfService = new HtmlToImage(_apiKey);
            var pdfDocument = await toPdfService.SendAsync(HtmlSource.HtmlContent);
            pdfDocument.SaveToFile("html_document.png");
        }

        public async Task UrlToImage()
        {
            var toPdfService = new UrlToImage(_apiKey);
            var pdfDocument = await toPdfService.SendAsync(HtmlSource.HtmlContent);
            pdfDocument.SaveToFile("url_document.png");
        }

        public async Task HtmlToPdfParams()
        {
            var toPdfService = new HtmlToPdf(_apiKey);
            var pdfDocument = await toPdfService
                .AddParameters(new Dictionary<ParameterPdfKey, object>
                {
                    { ParameterPdfKey.Grayscale, null }
                })
                .SendAsync(HtmlSource.HtmlContent);
            pdfDocument.SaveToFile("html_document.pdf");
        }

        public async Task UrlToPdfParams()
        {
            var toPdfService = new UrlToPdf(_apiKey);
            var pdfDocument = await toPdfService
                .AddParameter(ParameterPdfKey.Grayscale, null)
                .AddParameter(ParameterPdfKey.Orientation, "Album")
                .SendAsync(HtmlSource.HtmlContent);
            pdfDocument.SaveToFile("url_document.pdf");
        }

        public async Task HtmlToImageParams()
        {
            var toPdfService = new HtmlToImage(_apiKey);
            var pdfDocument = await toPdfService
                .AddParameters(new Dictionary<ParameterImageKey, object>
                {
                    { ParameterImageKey.Height, 500 },
                    { ParameterImageKey.Width, 200 },
                })
                .SendAsync(HtmlSource.HtmlContent);
            pdfDocument.SaveToFile("html_document.png");
        }

        public async Task UrlToImageParams()
        {
            var toPdfService = new UrlToImage(_apiKey);
            var pdfDocument = await toPdfService
                .AddParameter(ParameterImageKey.CropH, 10)
                .AddParameter(ParameterImageKey.CropW, 10)
                .AddParameter(ParameterImageKey.CropX, 10)
                .AddParameter(ParameterImageKey.CropY, 10)
                .SendAsync(HtmlSource.HtmlContent);
            pdfDocument.SaveToFile("url_document.png");
        }
    }
}