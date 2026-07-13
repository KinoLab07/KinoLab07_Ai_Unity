using System.Text;
using KinoLab07.AI.Controllers;
using KinoLab07.AI.Unity;

namespace KinoLab07.AI.Services
{
    public static class ContextBuilder
    {
        public static string Build()
        {
            StringBuilder sb = new();

            // Contexto general de Unity
            sb.AppendLine(UnityContext.GetCurrentContext());
            sb.AppendLine();

            // Objeto seleccionado
            sb.AppendLine(SelectionController.GetSelectionContext());
            sb.AppendLine();

            // Prefab del objeto seleccionado
            sb.AppendLine(PrefabController.GetPrefabContext());
            sb.AppendLine();

            // Escena activa
            sb.AppendLine(SceneController.GetSceneContext());
            sb.AppendLine();

            // Scripts del proyecto
            sb.AppendLine(ProjectController.GetScripts());

            return sb.ToString();
        }
    }
}