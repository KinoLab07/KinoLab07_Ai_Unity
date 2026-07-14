using System.IO;
using System.Text;
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

            StringBuilder sb = new();

            MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                if (script == null)
                    continue;

                MonoScript mono = MonoScript.FromMonoBehaviour(script);

                AppendScript(sb, mono);
            }

            return sb.ToString();
        }

        public static string GetSelectedScript()
{
    MonoScript script = SelectedScript.Get();

    if (script == null)
        return "";

    StringBuilder sb = new();

    AppendScript(sb, script);

    return sb.ToString();
}

        private static void AppendScript(
            StringBuilder sb,
            MonoScript script)
        {
            string path = AssetDatabase.GetAssetPath(script);

            sb.AppendLine("========================================");
            sb.AppendLine(script.name);
            sb.AppendLine(path);
            sb.AppendLine("========================================");
            sb.AppendLine();

            if (File.Exists(path))
            {
                sb.AppendLine(File.ReadAllText(path));
                sb.AppendLine();
            }
        }
    }
}