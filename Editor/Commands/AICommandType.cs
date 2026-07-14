namespace KinoLab07.AI.Commands
{
    public enum AICommandType
    {
        Unknown,
        CreateGameObject,
        DeleteGameObject,
        RenameGameObject,
        DuplicateGameObject,
        AddComponent,
        RemoveComponent,
        SetTransform,
        SetParent,
        CreateMaterial,
        AssignMaterial,
        CreateScript,
        CreatePrefab
    }
}
