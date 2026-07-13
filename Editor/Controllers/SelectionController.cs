using System.Text;
using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Controllers
{
    public static class SelectionController
    {
        public static string GetSelectionContext()
        {
            StringBuilder sb = new();

            GameObject go = Selection.activeGameObject;

            if (go == null)
            {
                sb.AppendLine("===== OBJETO SELECCIONADO =====");
                sb.AppendLine("Ninguno");
                return sb.ToString();
            }

            sb.AppendLine("===== OBJETO SELECCIONADO =====");
            sb.AppendLine(go.name);
            sb.AppendLine();

            sb.AppendLine("===== COMPONENTES =====");

            foreach (Component c in go.GetComponents<Component>())
            {
                if (c != null)
                    sb.AppendLine("- " + c.GetType().Name);
            }

            return sb.ToString();
        }

        public static MonoScript GetSelectedScript()
        {
            // Caso 1: seleccionado desde el Project
            if (Selection.activeObject is MonoScript script)
                return script;

            // Caso 2: seleccionado desde la Hierarchy
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
    }
}