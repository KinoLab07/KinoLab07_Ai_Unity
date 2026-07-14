namespace KinoLab07.AI.Commands
{
    public enum AICommandType
    {
        Unknown,

        // GameObjects
        CreateGameObject,
        DeleteGameObject,
        RenameGameObject,
        DuplicateGameObject,

        // Components
        AddComponent,
        RemoveComponent,

        // Transform
        SetTransform,
        SetParent,

        // Materials
        CreateMaterial,
        AssignMaterial,

        // Assets
        CreateScript,
        CreatePrefab
    }
}