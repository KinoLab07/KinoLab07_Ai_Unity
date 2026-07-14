using UnityEngine;

namespace KinoLab07.AI.Commands
{
    public static class AICommandParser
    {
        public static AICommandBatch Parse(string json)
        {
            return JsonUtility.FromJson<AICommandBatch>(json);
        }
    }
}