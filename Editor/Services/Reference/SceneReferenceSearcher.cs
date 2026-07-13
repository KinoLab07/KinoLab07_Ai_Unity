using System.Collections.Generic;
using UnityEditor;

namespace KinoLab07.AI.Services.Reference
{
    public static class SceneReferenceSearcher
    {
        public static List<string> Find(MonoScript script)
        {
            List<string> results = new();

            if (script == null)
                return results;

            // Se implementará utilizando EditorSceneManager
            // para abrir cada escena de forma temporal,
            // inspeccionarla y cerrarla sin modificar el proyecto.

            return results;
        }
    }
}