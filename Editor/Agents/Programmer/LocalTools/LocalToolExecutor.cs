using UnityEditor;
using UnityEngine;
using KinoLab07.AI.Models;
using KinoLab07.AI.Controllers;
using KinoLab07.AI.Unity;

namespace KinoLab07.AI.Agents.Programmer.LocalTools
{
    public static class LocalToolExecutor
    {
        public static bool CanExecute(ProgrammerTool tool)
        {
            return tool == ProgrammerTool.ReferenceSearch ||
                   tool == ProgrammerTool.ShowSelectedScript ||
                   tool == ProgrammerTool.CopySelectedScript ||
                   tool == ProgrammerTool.OpenSelectedScript ||

                   tool == ProgrammerTool.CreateCube ||
                   tool == ProgrammerTool.CreateSphere ||
                   tool == ProgrammerTool.CreateCapsule ||
                   tool == ProgrammerTool.CreatePlane ||
                   tool == ProgrammerTool.CreateEmptyGameObject;
        }

        public static AIResponse Execute(ProgrammerTool tool)
        {
            return tool switch
            {
                ProgrammerTool.ReferenceSearch => ExecuteReferenceSearch(),

                ProgrammerTool.ShowSelectedScript => ExecuteShowSelectedScript(),

                ProgrammerTool.CopySelectedScript => ExecuteCopySelectedScript(),

                ProgrammerTool.OpenSelectedScript => ExecuteOpenSelectedScript(),

                ProgrammerTool.CreateCube => GameObjectToolExecutor.CreateCube(),

                ProgrammerTool.CreateSphere => GameObjectToolExecutor.CreateSphere(),

                ProgrammerTool.CreateCapsule => GameObjectToolExecutor.CreateCapsule(),

                ProgrammerTool.CreatePlane => GameObjectToolExecutor.CreatePlane(),

                ProgrammerTool.CreateEmptyGameObject => GameObjectToolExecutor.CreateEmpty(),

                _ => new AIResponse
                {
                    Text = "Herramienta local no implementada."
                }
            };
        }

        private static AIResponse ExecuteReferenceSearch()
        {
            MonoScript script = SelectionController.GetSelectedScript();

            return new AIResponse
            {
                Text = ReferenceController.GetReferenceContext(script)
            };
        }

        private static AIResponse ExecuteShowSelectedScript()
        {
            return new AIResponse
            {
                Text = ScriptReader.GetSelectedScript()
            };
        }

        private static AIResponse ExecuteCopySelectedScript()
        {
            string code = ScriptReader.GetSelectedScript();

            EditorGUIUtility.systemCopyBuffer = code;

            return new AIResponse
            {
                Text = "✅ El código del script seleccionado fue copiado al portapapeles."
            };
        }

        private static AIResponse ExecuteOpenSelectedScript()
        {
            MonoScript script = SelectionController.GetSelectedScript();

            if (script == null)
            {
                return new AIResponse
                {
                    Text = "No hay un script seleccionado."
                };
            }

            AssetDatabase.OpenAsset(script);

            return new AIResponse
            {
                Text = "✅ Script abierto."
            };
        }
    }
}