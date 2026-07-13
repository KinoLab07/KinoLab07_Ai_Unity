using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Controllers.Reference
{
    public static class PrefabReferenceController
    {
        public static List<string> FindPrefabsUsingScript(MonoScript script)
        {
            List<string> prefabs = new();

            if (script == null)
                return prefabs;

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
                        prefabs.Add(path);
                        break;
                    }
                }
            }

            return prefabs;
        }
    }
}