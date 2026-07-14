using System;
using System.Collections.Generic;

namespace KinoLab07.AI.Commands
{
    [Serializable]
    public class AICommandBatch
    {
        public string version="1.0";
        public List<AICommand> commands=new();
    }
}
