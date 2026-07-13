using System.IO;
using UnityEditor;

namespace KinoLab07.AI.Services
{
    public static class ScriptWriter
    {
        public static void CreateScript(string folder, string className, string code)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, className + ".cs");

            File.WriteAllText(path, code);

            AssetDatabase.Refresh();
        }
    }
}