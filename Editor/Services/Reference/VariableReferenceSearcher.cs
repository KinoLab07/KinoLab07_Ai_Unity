using System.Collections.Generic;
using UnityEditor;

namespace KinoLab07.AI.Services.Reference
{
    public static class VariableReferenceSearcher
    {
        public static List<string> Find(
            MonoScript script,
            string variableName)
        {
            List<string> results = new();

            if (script == null)
                return results;

            // Implementación pendiente:
            // Buscar referencias a esta variable
            // en todos los scripts del proyecto.

            return results;
        }
    }
}