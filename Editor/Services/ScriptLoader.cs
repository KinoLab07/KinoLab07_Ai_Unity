using System.IO;

namespace KinoLab07.AI.Services
{
    public static class ScriptLoader
    {
        public static string Load(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath))
                return "";

            if (!File.Exists(assetPath))
                return "";

            return File.ReadAllText(assetPath);
        }
    }
}