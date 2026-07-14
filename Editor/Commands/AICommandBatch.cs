using System;
using System.Collections.Generic;

namespace KinoLab07.AI.Commands
{
    [Serializable]
    public class AICommandBatch
    {
        public List<AICommand> commands = new();
    }
}