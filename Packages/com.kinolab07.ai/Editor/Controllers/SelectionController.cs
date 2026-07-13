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
            GameObject go = Selection.activeGameObject;

            if (go == null)
                return null;

            MonoBehaviour behaviour = go.GetComponent<MonoBehaviour>();

            if (behaviour == null)
                return null;

            return MonoScript.FromMonoBehaviour(behaviour);
        }
    }
}