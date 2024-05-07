using ToPdf.Models;

namespace ToPdf.Requests
{
    public class HtmlToImage : ToPdfBase<ParameterImageKey>
    {
        public HtmlToImage(string apiKey) : base(apiKey, "html/image") { }
    }
}