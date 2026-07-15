using System.Linq;
using System.Text;
using KinoLab07.AI.Commands;
using KinoLab07.AI.Models;
using KinoLab07.AI.Utilities;

namespace KinoLab07.AI.Services
{
    /// <summary>
    /// Puente entre la respuesta cruda del LLM (Ollama) y el motor de comandos.
    ///
    /// Flujo:
    ///   1. Busca un bloque ```kinolab``` en el texto de respuesta.
    ///   2. Si no hay bloque, no hace nada (es una respuesta conversacional normal).
    ///   3. Si hay bloque, lo parsea a AICommandBatch.
    ///   4. Ejecuta el batch contra la API de Unity mediante AICommandExecutor.
    ///   5. Anexa al AIResponse.Text un resumen legible de lo ejecutado.
    /// </summary>
    public static class AIExecutionService
    {
        public static bool TryExecute(AIResponse response)
        {
            if (response == null || string.IsNullOrWhiteSpace(response.Text))
                return false;

            string kinolabJson = CodeBlockExtractor.ExtractKinoLabBlock(response.Text);

            if (kinolabJson == null)
                return false;

            if (!AICommandParser.TryParse(kinolabJson, out AICommandBatch batch, out string parseError))
            {
                response.Text += $"\n\n⚠️ No fue posible ejecutar el bloque kinolab: {parseError}";
                return false;
            }

            AICommandBatchResult result = AICommandExecutor.Execute(batch);

            response.Text += "\n\n" + BuildSummary(result);

            return result.Success;
        }

        private static string BuildSummary(AICommandBatchResult result)
        {
            StringBuilder sb = new();

            sb.AppendLine("========================");
            sb.AppendLine(result.Success ? "✅ EJECUCIÓN KINOLAB" : "⚠️ EJECUCIÓN KINOLAB (con errores)");
            sb.AppendLine("========================");
            sb.AppendLine(result.Summary);

            foreach (AICommandResult r in result.Results)
            {
                string icon = r.Success ? "✅" : "❌";
                sb.AppendLine($"{icon} {r.Message}");
            }

            return sb.ToString();
        }
    }
}
