using UnityEngine;

namespace KinoLab07.AI.Commands
{
    public static class AICommandExecutor
    {
        public static AICommandResult Execute(AICommandBatch batch)
        {
            CommandContext context = new();

            foreach (AICommand command in batch.commands)
            {
                switch (command.type)
                {
                    case AICommandType.CreateGameObject:
                        context.LastCreatedObject =
                            GameObjects.CreateGameObjectCommand.Empty(command.name);
                        break;

                    case AICommandType.AddComponent:
                        GameObjects.AddComponentCommand.Execute(command, context);
                        break;

                    case AICommandType.SetTransform:
                        GameObjects.SetTransformCommand.Execute(command, context);
                        break;

                    case AICommandType.CreateMaterial:
                        context.LastCreatedMaterial =
                            Materials.CreateMaterialCommand.Execute(command);
                        break;

                    case AICommandType.AssignMaterial:
                        Materials.AssignMaterialCommand.Execute(command, context);
                        break;
                }
            }

            return new AICommandResult
            {
                Success = true,
                Message = "Commands executed."
            };
        }
    }
}