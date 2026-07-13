using System.Text;
using System.IO;
using KinoLab07.AI.Services;

namespace KinoLab07.AI.Controllers
{
    public static class ProjectController
    {
        public static string GetScripts()
        {
            var scripts = ProjectIndexer.GetScripts();

            StringBuilder sb = new();

            sb.AppendLine("===== SCRIPTS DEL PROYECTO =====");
            sb.AppendLine();

            foreach (var script in scripts)
            {
                sb.AppendLine($"===== SCRIPT =====");
                sb.AppendLine(script);
                sb.AppendLine();

                if (File.Exists(script))
                {
                    sb.AppendLine("```csharp");
                    sb.AppendLine(File.ReadAllText(script));
                    sb.AppendLine("```");
                }

                sb.AppendLine();
            }

            sb.AppendLine($"Total Scripts: {scripts.Count}");

            return sb.ToString();
        }
    }
}