using System.Collections.Generic;
using UnityEditor;

namespace KinoLab07.AI.Services.Reference
{
    public static class MethodReferenceSearcher
    {
        public static List<string> Find(
            MonoScript script,
            string methodName)
        {
            List<string> results = new();

            if (script == null)
                return results;

            // Implementación pendiente:
            // Buscar llamadas al método en todos los scripts.

            return results;
        }
    }
}