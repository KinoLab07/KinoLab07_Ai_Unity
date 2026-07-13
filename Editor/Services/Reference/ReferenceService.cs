using UnityEditor;

namespace KinoLab07.AI.Services.Reference
{
    public static class ReferenceService
    {
        public static ReferenceResult FindReferences(MonoScript script)
        {
            ReferenceResult result = new();

            if (script == null)
                return result;

            result.GameObjects.AddRange(
                GameObjectReferenceSearcher.Find(script));

            result.Prefabs.AddRange(
                PrefabReferenceSearcher.Find(script));

            result.Scenes.AddRange(
                SceneReferenceSearcher.Find(script));

            return result;
        }
    }
}