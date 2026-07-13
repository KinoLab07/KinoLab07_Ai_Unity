using UnityEditor;
using UnityEngine;
using KinoLab07.AI.Models;
using KinoLab07.AI.Services;
using KinoLab07.AI.Unity;
using KinoLab07.AI.Utilities;
using KinoLab07.AI.Controllers;
using KinoLab07.AI.Agents.Programmer;

namespace KinoLab07.AI
{
    public class KinoLabWindow : EditorWindow
    {
        private string prompt = "";
        private string response = "";
        private Vector2 scrollPosition;

        private string[] models = { "gpt-oss:20b" };
        private int selectedModel = 0;

        [MenuItem("KinoLab07 AI/Open")]
        public static void Open()
        {
            GetWindow<KinoLabWindow>("KinoLab07 AI");
        }

        private async void OnEnable()
        {
            await RefreshModels();
        }

        private async System.Threading.Tasks.Task RefreshModels()
        {
            try
            {
                models = await OllamaModels.GetModels();

                if (models == null || models.Length == 0)
                    models = new[] { "gpt-oss:20b" };

                if (selectedModel >= models.Length)
                    selectedModel = 0;

                Repaint();
            }
            catch
            {
                models = new[] { "gpt-oss:20b" };
            }
        }

        private void OnGUI()
        {
            GUILayout.Space(8);

            GUILayout.Label("KinoLab07 AI", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();

            GUILayout.Label("Modelo", GUILayout.Width(60));

            selectedModel = EditorGUILayout.Popup(selectedModel, models);

            if (GUILayout.Button("Actualizar", GUILayout.Width(90)))
                _ = RefreshModels();

            GUILayout.EndHorizontal();

            GUILayout.Space(8);

            GUILayout.Label("Instrucción");

            prompt = EditorGUILayout.TextArea(prompt, GUILayout.Height(100));

            GUILayout.Space(8);

            if (GUILayout.Button("💬 Preguntar"))
                AskOllama();

            if (GUILayout.Button("✨ Crear Script"))
                CreateScript();

            if (GUILayout.Button("✏️ Modificar Script"))
                UpdateSelectedScript();

            if (GUILayout.Button("🔴 Analizar Consola"))
                AnalyzeConsole();

            if (GUILayout.Button("📂 Indexar Scripts"))
                IndexScripts();

            GUILayout.Space(10);

            GUILayout.Label("Respuesta");

            scrollPosition = EditorGUILayout.BeginScrollView(
                scrollPosition,
                GUILayout.Height(260));

            response = EditorGUILayout.TextArea(
                response,
                GUILayout.ExpandHeight(true));

            EditorGUILayout.EndScrollView();
        }

        private async void AskOllama()
        {
            response = "Consultando...";
            Repaint();

            string contexto = ContextBuilder.Build();

            AIResponse ai = await ProgrammerAgent.Ask(
                models[selectedModel],
                prompt,
                contexto);

            response = ai.Text;

            Repaint();
        }

        private async void CreateScript()
        {
            response = "Generando...";
            Repaint();

            AIResponse ai = await ScriptController.Create(
                models[selectedModel],
                prompt);

            response = ai.Text;

            Repaint();
        }

        private async void UpdateSelectedScript()
        {
            response = "Modificando...";
            Repaint();

            string contexto = UnityContext.GetCurrentContext();

            AIResponse ai = await OllamaClient.Generate(
                models[selectedModel],
$@"
Modifica este script.

Devuelve únicamente un bloque ```csharp```.

{contexto}

Cambios:

{prompt}");

            string code = CodeBlockExtractor.ExtractCode(ai.Text);

            string path = SelectedScript.GetPath();

            if (ScriptUpdater.UpdateScript(path, code))
                response = "Script actualizado.";
            else
                response = "No fue posible actualizar el script.";

            Repaint();
        }

        private async void AnalyzeConsole()
        {
            response = "Analizando consola...";
            Repaint();

            AIResponse ai = await ScriptController.AnalyzeConsole(
                models[selectedModel]);

            response = ai.Text;

            Repaint();
        }

        private void IndexScripts()
        {
            response = ProjectController.GetScripts();
            Repaint();
        }
    }
}