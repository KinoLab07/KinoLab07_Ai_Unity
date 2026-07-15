using UnityEngine;

namespace KinoLab07.AI.Agents.Programmer
{
    public static class ProgrammerToolSelector
    {
        public static ProgrammerTool Select(string prompt)
        {
            string p = Normalize(prompt);

            // ==========================================
            // Crear GameObjects (LOCAL) — solo con frase
            // casi exacta. Si el prompt trae detalles extra
            // (nombre, color, posición, componentes...), NO
            // se dispara el atajo local: cae al motor kinolab
            // vía Ollama, que sí puede honrar esos detalles.
            // ==========================================

            if (IsBareMatch(p, "crea un cubo", "crear un cubo"))
                return ProgrammerTool.CreateCube;

            if (IsBareMatch(p, "crea una esfera", "crear una esfera"))
                return ProgrammerTool.CreateSphere;

            if (IsBareMatch(p, "crea una capsula", "crear una capsula"))
                return ProgrammerTool.CreateCapsule;

            if (IsBareMatch(p, "crea un plano", "crear un plano"))
                return ProgrammerTool.CreatePlane;

            if (IsBareMatch(p,
                    "crea un gameobject", "crear un gameobject",
                    "gameobject vacio", "crea un gameobject vacio",
                    "crear un gameobject vacio"))
                return ProgrammerTool.CreateEmptyGameObject;

            // ==========================================
            // Referencias
            // ==========================================

            if (p.Contains("donde se usa") ||
                p.Contains("quien usa") ||
                p.Contains("referencia") ||
                p.Contains("referencias") ||
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
                p.Contains("que hace") ||
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

            if (p.Contains("buscar clase") ||
                p.Contains("buscar metodo") ||
                p.Contains("buscar variable"))
                return ProgrammerTool.None; // TODO: pendiente de implementar (ver ROADMAP)

            return ProgrammerTool.None;
        }

        /// <summary>
        /// Minúsculas, sin tildes, sin puntuación final, para comparar
        /// de forma tolerante pero exacta con IsBareMatch.
        /// </summary>
        private static string Normalize(string prompt)
        {
            string p = prompt.ToLowerInvariant().Trim();

            p = p.Replace("á", "a").Replace("é", "e").Replace("í", "i")
                 .Replace("ó", "o").Replace("ú", "u");

            p = p.TrimEnd('.', '!', '¡', '?', '¿', ' ');

            return p;
        }

        /// <summary>
        /// True solo si el prompt normalizado ES una de las frases dadas
        /// (match exacto), no si simplemente la contiene como substring.
        /// Evita que "crea un cubo rojo llamado X" dispare el atajo local
        /// e ignore el resto de la instrucción.
        /// </summary>
        private static bool IsBareMatch(string normalizedPrompt, params string[] phrases)
        {
            foreach (string phrase in phrases)
            {
                if (normalizedPrompt == phrase)
                    return true;
            }

            return false;
        }
    }
}