using UnityEngine;

namespace KinoLab07.AI.Agents.Programmer
{
    public static class ProgrammerToolSelector
    {
        public static ProgrammerTool Select(string prompt)
        {
            string p = prompt.ToLower();

            // Herramientas de búsqueda (PRIORIDAD ALTA)
            if (p.Contains("dónde se usa") ||
                p.Contains("donde se usa") ||
                p.Contains("referencia") ||
                p.Contains("referencias") ||
                p.Contains("usa") ||
                p.Contains("uso") ||
                p.Contains("usos"))
            {
                Debug.Log("ReferenceSearch detectado");
                return ProgrammerTool.ReferenceSearch;
            }

            if (p.Contains("script"))
                return ProgrammerTool.ReadSelectedScript;

            if (p.Contains("escena") || p.Contains("scene"))
                return ProgrammerTool.ReadScene;

            if (p.Contains("objeto") ||
                p.Contains("gameobject") ||
                p.Contains("seleccionado"))
                return ProgrammerTool.ReadSelection;

            if (p.Contains("prefab"))
                return ProgrammerTool.ReadPrefab;

            if (p.Contains("error") ||
                p.Contains("console") ||
                p.Contains("consola"))
                return ProgrammerTool.ReadConsole;

            if (p.Contains("buscar clase"))
                return ProgrammerTool.SearchClasses;

            if (p.Contains("buscar método") ||
                p.Contains("buscar metodo"))
                return ProgrammerTool.SearchMethods;

            if (p.Contains("buscar variable"))
                return ProgrammerTool.SearchVariables;

            return ProgrammerTool.None;
        }
    }
}