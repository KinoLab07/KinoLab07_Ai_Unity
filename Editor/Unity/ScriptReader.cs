using System.IO;
using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Unity
{
    public static class ScriptReader
    {
        public static string GetScripts(GameObject go)
        {
            if (go == null)
                return "";

            string result = "";

            MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                if (script == null)
                    continue;

                MonoScript mono = MonoScript.FromMonoBehaviour(script);

                string path = AssetDatabase.GetAssetPath(mono);

                result += "\n========================================\n";
                result += mono.name + "\n";
                result += path + "\n";
                result += "========================================\n\n";

                if (File.Exists(path))
                {
                    result += File.ReadAllText(path);
                    result += "\n\n";
                }
            }

            return result;
        }
    }
}