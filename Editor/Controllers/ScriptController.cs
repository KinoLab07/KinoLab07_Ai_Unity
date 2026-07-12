using System.Threading.Tasks;
using KinoLab07.AI.Models;
using KinoLab07.AI.Services;
using KinoLab07.AI.Utilities;

namespace KinoLab07.AI.Controllers
{
    public static class ScriptController
    {
        public static async Task<AIResponse> Create(string model, string prompt)
        {
            AIResponse ai = await OllamaClient.Generate(
                model,
$@"Genera únicamente código C# para Unity.

No expliques nada.

Devuelve solamente el código.

{prompt}");

            string code = CodeBlockExtractor.ExtractCode(ai.Text);

            string className = CodeBlockExtractor.ExtractClassName(code);

            ScriptWriter.CreateScript(
                "Assets/Scripts",
                className,
                code);

            ai.Text = $"Script creado: {className}.cs";

            return ai;
        }

        public static async Task<AIResponse> AnalyzeConsole(string model)
        {
            ConsoleError error = ConsoleAnalyzer.GetLastError();

            string code = ScriptLoader.Load(error.File);

            string prompt =
$@"Eres un experto en Unity 6.

Analiza este error.

ERROR:
{error.Message}

ARCHIVO:
{error.File}

LINEA:
{error.Line}

CODIGO:

{code}

Explica:

1. Qué ocurre.
2. Cómo solucionarlo.
3. Si puedes, devuelve el código corregido completo.";

            return await OllamaClient.Generate(model, prompt);
        }
    }
}