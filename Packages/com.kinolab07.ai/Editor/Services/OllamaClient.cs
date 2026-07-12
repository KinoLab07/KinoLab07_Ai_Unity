using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using KinoLab07.AI.Models;

namespace KinoLab07.AI.Services
{
    [System.Serializable]
    internal class OllamaRequest
    {
        public string model;
        public string prompt;
        public bool stream;
    }

    public static class OllamaClient
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<AIResponse> Generate(string model, string prompt)
{
    var request = new OllamaRequest
    {
        model = model,
        prompt = prompt,
        stream = false
    };

    string json = JsonUtility.ToJson(request);

    var response = await client.PostAsync(
        "http://127.0.0.1:11434/api/generate",
        new StringContent(json, Encoding.UTF8, "application/json"));

    response.EnsureSuccessStatusCode();

    string raw = await response.Content.ReadAsStringAsync();

    OllamaResponse data = JsonUtility.FromJson<OllamaResponse>(raw);

    return new AIResponse
    {
        Success = true,
        Text = data.response,
        Raw = raw
    };
}
    }
}