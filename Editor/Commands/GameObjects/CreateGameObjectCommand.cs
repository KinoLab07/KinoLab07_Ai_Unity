using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Commands.GameObjects
{
    /// <summary>
    /// Helpers de creación de GameObjects primitivos.
    /// Usados tanto por LocalToolExecutor (atajos por keyword)
    /// como por AICommandExecutor (comandos generados por el LLM).
    /// </summary>
    public static class CreateGameObjectCommand
    {
        public static GameObject Cube(string name = null) =>
            CreatePrimitive(PrimitiveType.Cube, name);

        public static GameObject Sphere(string name = null) =>
            CreatePrimitive(PrimitiveType.Sphere, name);

        public static GameObject Capsule(string name = null) =>
            CreatePrimitive(PrimitiveType.Capsule, name);

        public static GameObject Plane(string name = null) =>
            CreatePrimitive(PrimitiveType.Plane, name);

        public static GameObject Empty(string name = null)
        {
            GameObject go = new GameObject(
                string.IsNullOrWhiteSpace(name) ? "GameObject" : name);

            Undo.RegisterCreatedObjectUndo(go, "KinoLab AI: Crear GameObject vacío");
            Selection.activeGameObject = go;

            return go;
        }

        private static GameObject CreatePrimitive(PrimitiveType type, string name)
        {
            GameObject go = GameObject.CreatePrimitive(type);

            if (!string.IsNullOrWhiteSpace(name))
                go.name = name;

            Undo.RegisterCreatedObjectUndo(go, $"KinoLab AI: Crear {type}");
            Selection.activeGameObject = go;

            return go;
        }

        /// <summary>
        /// Crea un primitivo a partir de un nombre de forma en texto libre
        /// (usado por el motor de comandos kinolab, donde el campo "component"
        /// del comando indica la forma deseada).
        /// </summary>
        public static GameObject FromShapeName(string shape, string name = null)
        {
            switch ((shape ?? "").Trim().ToLowerInvariant())
            {
                case "cube": return Cube(name);
                case "sphere": return Sphere(name);
                case "capsule": return Capsule(name);
                case "plane": return Plane(name);
                case "empty":
                case "": return Empty(name);
                default: return Empty(name);
            }
        }
    }
}
