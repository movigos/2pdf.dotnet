using ToPdf.Models;

namespace ToPdf.Requests
{
    public class HtmlToPdf : ToPdfBase<ParameterPdfKey>
    {
        public HtmlToPdf(string apiKey) : base(apiKey, "html/pdf") { }
    }
}