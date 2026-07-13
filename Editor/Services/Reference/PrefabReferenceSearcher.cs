using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Services.Reference
{
    public static class PrefabReferenceSearcher
    {
        public static List<string> Find(MonoScript script)
        {
            List<string> results = new();

            if (script == null)
                return results;

            string[] guids = AssetDatabase.FindAssets("t:Prefab");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                GameObject prefab =
                    AssetDatabase.LoadAssetAtPath<GameObject>(path);

                if (prefab == null)
                    continue;

                MonoBehaviour[] behaviours =
                    prefab.GetComponentsInChildren<MonoBehaviour>(true);

                foreach (MonoBehaviour behaviour in behaviours)
                {
                    if (behaviour == null)
                        continue;

                    MonoScript monoScript =
                        MonoScript.FromMonoBehaviour(behaviour);

                    if (monoScript == script)
                    {
                        results.Add(prefab.name);
                        break;
                    }
                }
            }

            return results;
        }
    }
}