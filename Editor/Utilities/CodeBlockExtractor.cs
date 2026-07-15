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

        /// <summary>
        /// Extrae el contenido de un bloque ```kinolab ... ``` de la respuesta
        /// del modelo. Devuelve null si no hay ningún bloque kinolab.
        /// </summary>
        public static string ExtractKinoLabBlock(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            const string openTag = "```kinolab";

            int start = text.IndexOf(openTag, System.StringComparison.OrdinalIgnoreCase);

            if (start < 0)
                return null;

            int contentStart = start + openTag.Length;

            int end = text.IndexOf("```", contentStart, System.StringComparison.Ordinal);

            string content = end >= 0
                ? text.Substring(contentStart, end - contentStart)
                : text.Substring(contentStart); // fence sin cerrar: toma el resto

            return content.Trim();
        }
    }
}