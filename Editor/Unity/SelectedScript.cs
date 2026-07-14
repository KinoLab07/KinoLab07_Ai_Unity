using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Unity
{
    public static class SelectedScript
    {
        public static MonoScript Get()
        {
            // Caso 1: Script seleccionado en el Project
            if (Selection.activeObject is MonoScript script)
                return script;

            // Caso 2: GameObject seleccionado en la Hierarchy
            GameObject go = Selection.activeGameObject;

            if (go == null)
                return null;

            MonoBehaviour[] behaviours = go.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour == null)
                    continue;

                return MonoScript.FromMonoBehaviour(behaviour);
            }

            return null;
        }

        public static string GetPath()
        {
            MonoScript script = Get();

            if (script == null)
                return null;

            return AssetDatabase.GetAssetPath(script);
        }
    }
}