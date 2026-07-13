using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Services.Reference
{
    public static class GameObjectReferenceSearcher
    {
        public static List<string> Find(MonoScript script)
        {
            List<string> results = new();

            if (script == null)
                return results;

            MonoBehaviour[] behaviours =
                Object.FindObjectsByType<MonoBehaviour>(
                    FindObjectsSortMode.None);

            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour == null)
                    continue;

                MonoScript monoScript =
                    MonoScript.FromMonoBehaviour(behaviour);

                if (monoScript == script)
                    results.Add(behaviour.gameObject.name);
            }

            return results;
        }
    }
}