using UnityEngine;

namespace KinoLab07.AI.Commands.Materials
{
    public static class AssignMaterialCommand
    {
        public static void Execute(
            AICommand command,
            CommandContext context)
        {
            if (context.LastCreatedObject == null)
                return;

            if (context.LastCreatedMaterial == null)
                return;

            Renderer renderer =
                context.LastCreatedObject.GetComponent<Renderer>();

            if (renderer == null)
                return;

            renderer.sharedMaterial =
                context.LastCreatedMaterial;
        }
    }
}