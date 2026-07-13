using System.Text;
using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Controllers
{
    public static class PrefabController
    {
        public static string GetPrefabContext()
        {
            StringBuilder sb = new();

            GameObject go = Selection.activeGameObject;

            if (go == null)
                return "";

            GameObject prefab =
                PrefabUtility.GetCorrespondingObjectFromSource(go);

            sb.AppendLine("===== PREFAB =====");

            if (prefab == null)
            {
                sb.AppendLine("No pertenece a un Prefab.");
                return sb.ToString();
            }

            string path = AssetDatabase.GetAssetPath(prefab);

            sb.AppendLine(prefab.name);
            sb.AppendLine(path);

            return sb.ToString();
        }
    }
}