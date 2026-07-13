using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Unity
{
    public static class SelectedScript
    {
        public static string GetPath()
        {
            GameObject go = Selection.activeGameObject;

            if (go == null)
                return null;

            MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                if (script == null)
                    continue;

                MonoScript mono = MonoScript.FromMonoBehaviour(script);

                return AssetDatabase.GetAssetPath(mono);
            }

            return null;
        }
    }
}