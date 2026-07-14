using UnityEngine;

namespace KinoLab07.AI.Commands.Materials
{
    public static class CreateMaterialCommand
    {
        public static Material Execute(AICommand command)
        {
            Material material =
                new Material(Shader.Find("Universal Render Pipeline/Lit"));

            material.name = command.name;

            return material;
        }
    }
}