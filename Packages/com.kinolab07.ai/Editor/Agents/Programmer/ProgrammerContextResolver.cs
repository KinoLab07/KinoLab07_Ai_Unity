using System.Text;
using UnityEditor;
using UnityEngine;
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

                case ProgrammerTool.ReferenceSearch:
                {
                    MonoScript script = SelectionController.GetSelectedScript();

                    if (script == null)
                    {
                        sb.AppendLine("No hay un script seleccionado.");
                        break;
                    }

                    var objects = ReferenceController.FindObjectsUsingScript(script);

                    sb.AppendLine("===== REFERENCIAS =====");
                    sb.AppendLine(script.name);
                    sb.AppendLine();

                    foreach (GameObject go in objects)
                        sb.AppendLine("- " + go.name);

                    break;
                }

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