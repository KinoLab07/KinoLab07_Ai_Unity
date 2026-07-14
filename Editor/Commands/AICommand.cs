using System;

namespace KinoLab07.AI.Commands
{
    [Serializable]
    public class AICommand
    {
        public AICommandType type;
        public string name;
        public string target;
        public string component;
        public float x,y,z;
        public float rx,ry,rz;
        public float sx=1,sy=1,sz=1;
    }
}
