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
    Object obj = Selection.activeObject;

    if (obj is MonoScript script)
        return script;

    return null;
}
    }
}