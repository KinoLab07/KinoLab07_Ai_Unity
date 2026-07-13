using System.Collections.Generic;
using UnityEditor;

namespace KinoLab07.AI.Services
{
    public static class ProjectIndexer
    {
        public static List<string> GetScripts()
        {
            List<string> scripts = new();

            string[] guids = AssetDatabase.FindAssets("t:MonoScript");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                if (!path.StartsWith("Assets/Scripts/"))
                    continue;

                scripts.Add(path);
            }

            scripts.Sort();

            return scripts;
        }
    }
}