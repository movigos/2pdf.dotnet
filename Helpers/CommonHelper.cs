using System.IO;
using System.Text;
using ToPdf.Models;

namespace ToPdf.Helpers
{
    public static class CommonHelper
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var builder = new StringBuilder();
            for (var i = 0; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]) && i > 0)
                {
                    builder.Append('_');
                }

                builder.Append(char.ToLower(input[i]));
            }

            return builder.ToString();
        }
        
        public static void SaveToFile(this DocumentModel document, string fileName)
        {
            File.WriteAllBytes(fileName, document.Bytes);
        }

    }
}