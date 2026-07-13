using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace KinoLab07.AI.Unity
{
    public static class UnityContext
    {
        public static string GetCurrentContext()
        {
            StringBuilder sb = new StringBuilder();

            GameObject go = Selection.activeGameObject;

            sb.AppendLine("===== ESCENA =====");
            sb.AppendLine(EditorSceneManager.GetActiveScene().name);
            sb.AppendLine();

            if (go != null)
            {
                sb.AppendLine("===== GAMEOBJECT =====");
                sb.AppendLine(go.name);
                sb.AppendLine();

                sb.AppendLine("===== COMPONENTES =====");

                foreach (Component c in go.GetComponents<Component>())
                {
                    if (c != null)
                        sb.AppendLine("- " + c.GetType().Name);
                }

                sb.AppendLine();

                sb.AppendLine("===== SCRIPT SELECCIONADO =====");
                sb.AppendLine(ScriptReader.GetScripts(go));
                sb.AppendLine();
            }


            return sb.ToString();
        }
    }
}