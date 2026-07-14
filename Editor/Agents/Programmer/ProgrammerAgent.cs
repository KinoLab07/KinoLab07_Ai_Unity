using System.Threading.Tasks;
using KinoLab07.AI.Commands;
using KinoLab07.AI.Models;
using KinoLab07.AI.Services;
using KinoLab07.AI.Agents.Programmer.LocalTools;

namespace KinoLab07.AI.Agents.Programmer
{
    public static class ProgrammerAgent
    {
        public static async Task<AIResponse> Ask(
            string model,
            string prompt,
            string context)
        {
            ProgrammerAction action =
                ProgrammerClassifier.Classify(prompt);

            ProgrammerTool tool =
                ProgrammerToolSelector.Select(prompt);

            if (LocalToolExecutor.CanExecute(tool))
                return LocalToolExecutor.Execute(tool);

            string smartContext =
                ProgrammerContextResolver.Resolve(tool);

            if (string.IsNullOrWhiteSpace(smartContext))
                smartContext = context;

            string finalPrompt =
                ProgrammerPromptBuilder.Build(
                    action,
                    prompt,
                    smartContext);

            AIResponse response =
                await OllamaClient.Generate(
                    model,
                    finalPrompt);

            AIExecutionService.TryExecute(response);

            return response;
        }
    }
}