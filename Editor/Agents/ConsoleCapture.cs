using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KinoLab07.AI.Services
{
    [InitializeOnLoad]
    public static class ConsoleCapture
    {
        private static readonly List<string> logs = new();

        static ConsoleCapture()
        {
            Application.logMessageReceived += OnLog;
        }

        private static void OnLog(string condition, string stackTrace, LogType type)
        {
            logs.Add($"[{type}] {condition}");

            if (logs.Count > 200)
                logs.RemoveAt(0);
        }

        public static string GetLogs()
        {
            return string.Join("\n", logs);
        }

        public static void Clear()
        {
            logs.Clear();
        }
    }
}