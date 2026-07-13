using KinoLab07.AI.Models;

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
            return new AIResponse
            {
                Text = "Herramienta local no implementada."
            };
        }
    }
}