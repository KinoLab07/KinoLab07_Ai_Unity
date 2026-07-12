using System.Text.RegularExpressions;

namespace KinoLab07.AI.Utilities
{
    public static class CodeBlockExtractor
    {
        public static string ExtractCode(string text)
        {
            Match match = Regex.Match(
                text,
                @"```(?:csharp|cs|c#)?\s*(.*?)```",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (match.Success)
                return match.Groups[1].Value.Trim();

            return text.Trim();
        }

        public static string ExtractClassName(string code)
        {
            Match match = Regex.Match(
                code,
                @"class\s+([A-Za-z_][A-Za-z0-9_]*)");

            if (match.Success)
                return match.Groups[1].Value;

            return "NewScript";
        }
    }
}