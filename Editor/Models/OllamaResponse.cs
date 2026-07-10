using System;
using KinoLab07.AI.Models;

namespace KinoLab07.AI.Models
{
    [Serializable]
    public class OllamaResponse
    {
        public string model;
        public string response;
        public bool done;
    }
}