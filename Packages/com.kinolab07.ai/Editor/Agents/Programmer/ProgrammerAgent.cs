using System.Threading.Tasks;
using UnityEngine;
using KinoLab07.AI.Models;
using KinoLab07.AI.Services;

namespace KinoLab07.AI.Agents.Programmer
{
    public static class ProgrammerAgent
    {
        public static async Task<AIResponse> Ask(
            string model,
            string prompt,
            string context)
        {
            Debug.Log("ProgrammerAgent iniciado");

            ProgrammerAction action =
                ProgrammerClassifier.Classify(prompt);

            ProgrammerTool tool =
                ProgrammerToolSelector.Select(prompt);

            Debug.Log("Acción: " + action);
            Debug.Log("Herramienta: " + tool);

            string smartContext =
                ProgrammerContextResolver.Resolve(tool);

            if (string.IsNullOrWhiteSpace(smartContext))
                smartContext = context;

            string finalPrompt =
                ProgrammerPromptBuilder.Build(
                    action,
                    prompt,
                    smartContext);

            Debug.Log("Prompt construido");

            AIResponse response =
                await OllamaClient.Generate(
                    model,
                    finalPrompt);

            Debug.Log("Respuesta recibida");

            return response;
        }
    }
}