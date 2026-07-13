using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Controllers
{
    public static class ReferenceController
    {
        public static string GetReferenceContext(MonoScript script)
        {
            StringBuilder sb = new();

            if (script == null)
            {
                sb.AppendLine("No hay un script seleccionado.");
                return sb.ToString();
            }

            List<GameObject> objects = FindObjectsUsingScript(script);

            sb.AppendLine("===== REFERENCIAS =====");
            sb.AppendLine($"Script: {script.name}");
            sb.AppendLine();

            if (objects.Count == 0)
            {
                sb.AppendLine("No se encontraron GameObjects usando este script.");
                return sb.ToString();
            }

            sb.AppendLine("GameObjects:");

            foreach (GameObject go in objects)
                sb.AppendLine($"- {go.name}");

            return sb.ToString();
        }

        public static List<GameObject> FindObjectsUsingScript(MonoScript script)
        {
            List<GameObject> results = new();

            if (script == null)
                return results;

            MonoBehaviour[] behaviours = Object.FindObjectsByType<MonoBehaviour>(
                FindObjectsSortMode.None);

            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour == null)
                    continue;

                MonoScript monoScript = MonoScript.FromMonoBehaviour(behaviour);

                if (monoScript == script)
                    results.Add(behaviour.gameObject);
            }

            return results;
        }
    }
}