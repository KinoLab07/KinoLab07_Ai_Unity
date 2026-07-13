using System.Collections.Generic;
using UnityEditor;

namespace KinoLab07.AI.Services.Reference
{
    public static class ClassReferenceSearcher
    {
        public static List<string> Find(
            MonoScript script)
        {
            List<string> results = new();

            if (script == null)
                return results;

            // Implementación pendiente:
            // Buscar referencias a esta clase
            // en todos los scripts del proyecto.

            return results;
        }
    }
}