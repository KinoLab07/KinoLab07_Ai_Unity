using System.Text;
using UnityEditor;
using KinoLab07.AI.Services.Reference;

namespace KinoLab07.AI.Controllers
{
    public static class ReferenceController
    {
        public static string GetReferenceContext(MonoScript script)
        {
            if (script == null)
                return "No hay un script seleccionado.";

            ReferenceResult result =
                ReferenceService.FindReferences(script);

            StringBuilder sb = new();

            sb.AppendLine("===== REFERENCIAS =====");
            sb.AppendLine($"Script: {script.name}");
            sb.AppendLine();

            sb.AppendLine("GameObjects:");

            if (result.GameObjects.Count == 0)
                sb.AppendLine("- Ninguno");
            else
                foreach (string go in result.GameObjects)
                    sb.AppendLine($"- {go}");

            sb.AppendLine();

            sb.AppendLine("Prefabs:");

            if (result.Prefabs.Count == 0)
                sb.AppendLine("- Ninguno");
            else
                foreach (string prefab in result.Prefabs)
                    sb.AppendLine($"- {prefab}");

            sb.AppendLine();

            sb.AppendLine("Escenas:");

            if (result.Scenes.Count == 0)
                sb.AppendLine("- Ninguna");
            else
                foreach (string scene in result.Scenes)
                    sb.AppendLine($"- {scene}");

            return sb.ToString();
        }
    }
}