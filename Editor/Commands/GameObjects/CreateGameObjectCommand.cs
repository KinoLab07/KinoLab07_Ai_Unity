using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Commands.GameObjects
{
    public static class CreateGameObjectCommand
    {
        public static GameObject Empty(string name = "GameObject")
        {
            GameObject go = new(name);

            Undo.RegisterCreatedObjectUndo(go, "Create GameObject");

            Selection.activeGameObject = go;

            return go;
        }

        public static GameObject Cube(string name = "Cube")
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

            go.name = name;

            Undo.RegisterCreatedObjectUndo(go, "Create Cube");

            Selection.activeGameObject = go;

            return go;
        }

        public static GameObject Sphere(string name = "Sphere")
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            go.name = name;

            Undo.RegisterCreatedObjectUndo(go, "Create Sphere");

            Selection.activeGameObject = go;

            return go;
        }

        public static GameObject Capsule(string name = "Capsule")
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Capsule);

            go.name = name;

            Undo.RegisterCreatedObjectUndo(go, "Create Capsule");

            Selection.activeGameObject = go;

            return go;
        }

        public static GameObject Plane(string name = "Plane")
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);

            go.name = name;

            Undo.RegisterCreatedObjectUndo(go, "Create Plane");

            Selection.activeGameObject = go;

            return go;
        }
    }
}