using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Controllers
{
    public static class ReferenceController
    {
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