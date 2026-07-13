using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace KinoLab07.AI.Services
{
    [System.Serializable]
    public class OllamaModel
    {
        public string name;
    }

    [System.Serializable]
    public class OllamaTags
    {
        public List<OllamaModel> models;
    }

    public static class OllamaModels
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string[]> GetModels()
        {
            string json = await client.GetStringAsync(
                "http://127.0.0.1:11434/api/tags");

            OllamaTags tags = JsonUtility.FromJson<OllamaTags>(json);

            if (tags == null || tags.models == null)
                return new string[] { "gpt-oss:20b" };

            string[] result = new string[tags.models.Count];

            for (int i = 0; i < result.Length; i++)
                result[i] = tags.models[i].name;

            return result;
        }
    }
}