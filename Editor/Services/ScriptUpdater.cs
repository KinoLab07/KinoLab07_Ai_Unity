using System.IO;
using UnityEditor;

namespace KinoLab07.AI.Services
{
    public static class ScriptUpdater
    {
        public static bool UpdateScript(string assetPath, string code)
        {
            if (string.IsNullOrEmpty(assetPath))
                return false;

            if (!File.Exists(assetPath))
                return false;

            File.WriteAllText(assetPath, code);

            AssetDatabase.Refresh();

            return true;
        }
    }
}