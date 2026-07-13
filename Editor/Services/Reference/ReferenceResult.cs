using System.Collections.Generic;

namespace KinoLab07.AI.Services.Reference
{
    public class ReferenceResult
    {
        public List<string> GameObjects { get; } = new();

        public List<string> Prefabs { get; } = new();

        public List<string> Scenes { get; } = new();

        public bool HasReferences =>
            GameObjects.Count > 0 ||
            Prefabs.Count > 0 ||
            Scenes.Count > 0;
    }
}