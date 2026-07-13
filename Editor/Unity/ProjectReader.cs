using System.IO;
using System.Text;
using UnityEditor;

namespace KinoLab07.AI.Unity
{
    public static class ProjectReader
    {
        public static string GetProjectScripts(int maxFiles = 20)
{
    StringBuilder sb = new StringBuilder();

    string[] guids = AssetDatabase.FindAssets("t:MonoScript");

    int count = 0;

    foreach (string guid in guids)
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);

        // Solo Assets
        if (!path.StartsWith("Assets/"))
            continue;

        // Ignorar ejemplos
        if (path.Contains("TutorialInfo"))
            continue;

        if (path.Contains("Samples"))
            continue;

        if (path.Contains("Plugins"))
            continue;

        if (path.Contains("ThirdParty"))
            continue;

        if (path.Contains("Packages"))
            continue;

        if (!File.Exists(path))
            continue;

        sb.AppendLine("================================");
        sb.AppendLine(path);
        sb.AppendLine("================================");
        sb.AppendLine(File.ReadAllText(path));
        sb.AppendLine();

        count++;

        if (count >= maxFiles)
            break;
    }

    return sb.ToString();
}
    }
}