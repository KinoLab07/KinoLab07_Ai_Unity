using System;

namespace KinoLab07.AI.Commands
{
    [Serializable]
    public class AICommand
    {
        public AICommandType type;

        public string target;

        public string name;

        public string value;

        public string component;

        public float x;
        public float y;
        public float z;

        public float rx;
        public float ry;
        public float rz;

        public float sx = 1;
        public float sy = 1;
        public float sz = 1;
    }
}