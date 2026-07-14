using UnityEngine;

namespace KinoLab07.AI.Agents.Programmer
{
    public static class ProgrammerToolSelector
    {
        public static ProgrammerTool Select(string prompt)
        {
            string p = prompt.ToLower();

            // ==========================================
            // Crear GameObjects (LOCAL)
            // ==========================================

            if (p.Contains("crea un cubo") ||
                p.Contains("crear un cubo"))
                return ProgrammerTool.CreateCube;

            if (p.Contains("crea una esfera") ||
                p.Contains("crear una esfera"))
                return ProgrammerTool.CreateSphere;

            if (p.Contains("crea una cápsula") ||
                p.Contains("crea una capsula") ||
                p.Contains("crear una cápsula") ||
                p.Contains("crear una capsula"))
                return ProgrammerTool.CreateCapsule;

            if (p.Contains("crea un plano") ||
                p.Contains("crear un plano"))
                return ProgrammerTool.CreatePlane;

            if (p.Contains("crea un gameobject") ||
                p.Contains("crear un gameobject") ||
                p.Contains("gameobject vacío") ||
                p.Contains("gameobject vacio"))
                return ProgrammerTool.CreateEmptyGameObject;

            // ==========================================
            // Referencias
            // ==========================================

            if (p.Contains("dónde se usa") ||
                p.Contains("donde se usa") ||
                p.Contains("quién usa") ||
                p.Contains("quien usa") ||
                p.Contains("referencia") ||
                p.Contains("referencias") ||
                p.Contains("está siendo usado") ||
                p.Contains("esta siendo usado") ||
                p.Contains("usa") ||
                p.Contains("uso") ||
                p.Contains("usos"))
            {
                return ProgrammerTool.ReferenceSearch;
            }

            // ==========================================
            // Mostrar script
            // ==========================================

            if (p.Contains("mostrar script") ||
                p.Contains("mostrar este script") ||
                p.Contains("muestra este script") ||
                p.Contains("muéstrame este script") ||
                p.Contains("muestrame este script") ||
                p.Contains("ver script"))
            {
                return ProgrammerTool.ShowSelectedScript;
            }

            // ==========================================
            // Copiar script
            // ==========================================

            if (p.Contains("copia este script") ||
                p.Contains("copiar este script") ||
                p.Contains("copiar código") ||
                p.Contains("copiar codigo"))
            {
                return ProgrammerTool.CopySelectedScript;
            }

            // ==========================================
            // Abrir script
            // ==========================================

            if (p.Contains("abre este script") ||
                p.Contains("abrir este script") ||
                p.Contains("abrir script"))
            {
                return ProgrammerTool.OpenSelectedScript;
            }

            // ==========================================
            // Explicar código
            // ==========================================

            if (p.Contains("explica") ||
                p.Contains("explicar") ||
                p.Contains("qué hace") ||
                p.Contains("que hace") ||
                p.Contains("cómo funciona") ||
                p.Contains("como funciona") ||
                p.Contains("resume"))
            {
                return ProgrammerTool.ExplainScript;
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