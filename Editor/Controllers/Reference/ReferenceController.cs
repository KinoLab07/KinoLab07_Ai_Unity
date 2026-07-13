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

    List<string> prefabs =
        Reference.PrefabReferenceController.FindPrefabsUsingScript(script);

    sb.AppendLine("===== REFERENCIAS =====");
    sb.AppendLine($"Script: {script.name}");
    sb.AppendLine();

    sb.AppendLine("GameObjects:");

    if (objects.Count == 0)
    {
        sb.AppendLine("- Ninguno");
    }
    else
    {
        foreach (GameObject go in objects)
            sb.AppendLine($"- {go.name}");
    }

    sb.AppendLine();
    sb.AppendLine("Prefabs:");

    if (prefabs.Count == 0)
    {
        sb.AppendLine("- Ninguno");
    }
    else
    {
        foreach (string prefab in prefabs)
            sb.AppendLine($"- {prefab}");
    }

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