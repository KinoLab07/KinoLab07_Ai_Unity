using UnityEngine;

namespace KinoLab07.AI.Agents.Programmer
{
    public static class ProgrammerToolSelector
    {
        public static ProgrammerTool Select(string prompt)
        {
            string p = prompt.ToLower();

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

            if (p.Contains("usa") ||
                p.Contains("uso") ||
                p.Contains("usos") ||
                p.Contains("referencia") ||
                p.Contains("referencias") ||
                p.Contains("dónde se usa") ||
                p.Contains("donde se usa"))
            {
                Debug.Log("ReferenceSearch detectado");
                return ProgrammerTool.ReferenceSearch;
            }

            return ProgrammerTool.None;
        }
    }
}