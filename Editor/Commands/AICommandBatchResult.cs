using System.Collections.Generic;

namespace KinoLab07.AI.Commands
{
    /// <summary>
    /// Resultado de ejecutar un AICommandBatch completo.
    /// </summary>
    public class AICommandBatchResult
    {
        public bool Success;
        public string Summary;
        public List<AICommandResult> Results = new();
    }
}
