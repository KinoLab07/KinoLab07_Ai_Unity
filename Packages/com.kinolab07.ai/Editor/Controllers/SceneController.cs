using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using KinoLab07.AI.Services;

namespace KinoLab07.AI.Controllers
{
    public static class SceneController
    {
        public static string GetSceneContext()
        {
            StringBuilder sb = new();

            Scene scene = SceneManager.GetActiveScene();

            sb.AppendLine("===== ESCENA ACTIVA =====");
            sb.AppendLine(scene.name);
            sb.AppendLine();

            sb.AppendLine("===== GAMEOBJECTS =====");

            GameObject[] roots = scene.GetRootGameObjects();

            foreach (GameObject go in roots)
            {
                if (SceneFilter.Ignore(go))
                    continue;

                AppendGameObject(go.transform, sb, 0);
            }

            return sb.ToString();
        }

        private static void AppendGameObject(
            Transform t,
            StringBuilder sb,
            int level)
        {
            if (SceneFilter.Ignore(t.gameObject))
                return;

            sb.AppendLine($"{new string(' ', level * 2)}- {t.name}");

            Component[] components = t.GetComponents<Component>();

            foreach (Component c in components)
            {
                if (c != null)
                    sb.AppendLine($"{new string(' ', level * 2 + 2)}[{c.GetType().Name}]");
            }

            foreach (Transform child in t)
            {
                AppendGameObject(child, sb, level + 1);
            }
        }
    }
}