using UnityEngine;

namespace KinoLab07.AI.Commands.GameObjects
{
    public static class AddComponentCommand
    {
        public static void Execute(
            AICommand command,
            CommandContext context)
        {
            GameObject go = context.LastCreatedObject;

            if (go == null)
                return;

            switch (command.component)
            {
                case "Rigidbody":
                    go.AddComponent<Rigidbody>();
                    break;

                case "BoxCollider":
                    go.AddComponent<BoxCollider>();
                    break;

                case "Light":
                    go.AddComponent<Light>();
                    break;
            }
        }
    }
}