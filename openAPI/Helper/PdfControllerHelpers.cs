using System.Text.RegularExpressions;

namespace openAPI.Helper
{
    internal static class PdfControllerHelpers
    {
        public static string RemoveStopWords(string input, HashSet<string> stopWords)
        {
            foreach (string stopWord in stopWords)
            {
                string pattern = string.Format(@"\b{0}\b", stopWord);
                input = Regex.Replace(input, pattern, string.Empty);
            }
            return input;
        }
    }
}