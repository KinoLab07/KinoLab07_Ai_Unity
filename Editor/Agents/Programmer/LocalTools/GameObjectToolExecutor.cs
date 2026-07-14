using KinoLab07.AI.Commands.GameObjects;
using KinoLab07.AI.Models;

namespace KinoLab07.AI.Agents.Programmer.LocalTools
{
    public static class GameObjectToolExecutor
    {
        public static AIResponse CreateCube()
        {
            CreateGameObjectCommand.Cube();

            return new AIResponse
            {
                Text = "✅ Cubo creado."
            };
        }

        public static AIResponse CreateSphere()
        {
            CreateGameObjectCommand.Sphere();

            return new AIResponse
            {
                Text = "✅ Esfera creada."
            };
        }

        public static AIResponse CreateCapsule()
        {
            CreateGameObjectCommand.Capsule();

            return new AIResponse
            {
                Text = "✅ Cápsula creada."
            };
        }

        public static AIResponse CreatePlane()
        {
            CreateGameObjectCommand.Plane();

            return new AIResponse
            {
                Text = "✅ Plano creado."
            };
        }

        public static AIResponse CreateEmpty()
        {
            CreateGameObjectCommand.Empty();

            return new AIResponse
            {
                Text = "✅ GameObject vacío creado."
            };
        }
    }
}