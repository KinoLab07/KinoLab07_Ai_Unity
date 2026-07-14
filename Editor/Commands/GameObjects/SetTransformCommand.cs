using UnityEngine;

namespace KinoLab07.AI.Commands.GameObjects
{
    public static class SetTransformCommand
    {
        public static void Execute(
            AICommand command,
            CommandContext context)
        {
            GameObject go = context.LastCreatedObject;

            if (go == null)
                return;

            go.transform.position =
                new Vector3(command.x, command.y, command.z);

            go.transform.eulerAngles =
                new Vector3(command.rx, command.ry, command.rz);

            go.transform.localScale =
                new Vector3(command.sx, command.sy, command.sz);
        }
    }
}