using UnityEditor;
using KinoLab07.AI.Models;
using KinoLab07.AI.Controllers;

namespace KinoLab07.AI.Agents.Programmer.LocalTools
{
    public static class LocalToolExecutor
    {
        public static bool CanExecute(ProgrammerTool tool)
        {
            return tool == ProgrammerTool.ReferenceSearch;
        }

        public static AIResponse Execute(ProgrammerTool tool)
        {
            return tool switch
            {
                ProgrammerTool.ReferenceSearch => ExecuteReferenceSearch(),
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
    }
}