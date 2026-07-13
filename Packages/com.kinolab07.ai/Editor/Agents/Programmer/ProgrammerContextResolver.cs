using System.Text;
using KinoLab07.AI.Controllers;

namespace KinoLab07.AI.Agents.Programmer
{
    public static class ProgrammerContextResolver
    {
        public static string Resolve(ProgrammerTool tool)
        {
            StringBuilder sb = new();

            switch (tool)
            {
                case ProgrammerTool.ReadSelection:
                    sb.AppendLine(SelectionController.GetSelectionContext());
                    break;

                case ProgrammerTool.ReadScene:
                    sb.AppendLine(SceneController.GetSceneContext());
                    break;

                case ProgrammerTool.ReadPrefab:
                    sb.AppendLine(PrefabController.GetPrefabContext());
                    break;

                case ProgrammerTool.ReadConsole:
    sb.AppendLine("Lectura de consola disponible en una versión posterior.");
    break;

                case ProgrammerTool.ReadSelectedScript:
                    sb.AppendLine(ProjectController.GetScripts());
                    break;

                default:
                    sb.AppendLine(ProjectController.GetScripts());
                    sb.AppendLine();
                    sb.AppendLine(SceneController.GetSceneContext());
                    sb.AppendLine();
                    sb.AppendLine(SelectionController.GetSelectionContext());
                    break;
            }

            return sb.ToString();
        }
    }
}