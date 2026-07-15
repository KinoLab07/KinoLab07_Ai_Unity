using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using KinoLab07.AI.Commands.GameObjects;

namespace KinoLab07.AI.Commands
{
    /// <summary>
    /// Ejecuta un AICommandBatch (parseado desde el bloque ```kinolab```)
    /// contra la API de Unity. Cada comando se agrupa en una sola operación
    /// de Undo, para que el usuario pueda deshacer todo el batch con Ctrl+Z.
    /// </summary>
    public static class AICommandExecutor
    {
        public static AICommandBatchResult Execute(AICommandBatch batch)
        {
            var result = new AICommandBatchResult();

            if (batch == null || batch.commands == null || batch.commands.Count == 0)
            {
                result.Success = false;
                result.Summary = "El bloque kinolab no contenía comandos.";
                return result;
            }

            Undo.SetCurrentGroupName("KinoLab AI: Ejecutar comandos");
            int group = Undo.GetCurrentGroup();

            foreach (AICommand cmd in batch.commands)
                result.Results.Add(ExecuteSingle(cmd));

            Undo.CollapseUndoOperations(group);

            int ok = result.Results.Count(r => r.Success);
            result.Success = ok == result.Results.Count;
            result.Summary = $"{ok}/{result.Results.Count} comandos ejecutados correctamente.";

            return result;
        }

        private static AICommandResult ExecuteSingle(AICommand cmd)
        {
            if (cmd == null)
                return Fail(null, "Comando nulo.");

            // JsonUtility no deserializa enums por nombre (solo por índice
            // numérico), así que "type" llega como string y lo convertimos
            // acá con Enum.TryParse (case-insensitive).
            if (!Enum.TryParse(cmd.type, ignoreCase: true, out AICommandType type))
                return Fail(cmd, $"Tipo de comando desconocido: '{cmd.type}'.");

            try
            {
                return type switch
                {
                    AICommandType.CreateGameObject => CreateGameObject(cmd),
                    AICommandType.DeleteGameObject => DeleteGameObject(cmd),
                    AICommandType.RenameGameObject => RenameGameObject(cmd),
                    AICommandType.DuplicateGameObject => DuplicateGameObject(cmd),
                    AICommandType.AddComponent => AddComponent(cmd),
                    AICommandType.RemoveComponent => RemoveComponent(cmd),
                    AICommandType.SetTransform => SetTransform(cmd),
                    AICommandType.SetParent => SetParent(cmd),
                    AICommandType.CreateMaterial => CreateMaterial(cmd),
                    AICommandType.AssignMaterial => AssignMaterial(cmd),
                    AICommandType.CreateScript => Fail(cmd, "CreateScript vía kinolab aún no implementado. Usa el botón 'Crear Script'."),
                    AICommandType.CreatePrefab => CreatePrefabFromTarget(cmd),
                    _ => Fail(cmd, $"Tipo de comando desconocido: {type}")
                };
            }
            catch (Exception ex)
            {
                return Fail(cmd, $"Excepción ejecutando {type}: {ex.Message}");
            }
        }

        // ==========================================
        // GameObjects
        // ==========================================

        private static AICommandResult CreateGameObject(AICommand cmd)
        {
            GameObject go = CreateGameObjectCommand.FromShapeName(cmd.component, cmd.name);
            ApplyTransform(go.transform, cmd, onlyIfProvided: true);

            return Ok(cmd, $"GameObject '{go.name}' creado.");
        }

        private static AICommandResult DeleteGameObject(AICommand cmd)
        {
            GameObject go = FindByName(cmd.target);
            if (go == null)
                return Fail(cmd, $"No se encontró el GameObject '{cmd.target}'.");

            Undo.DestroyObjectImmediate(go);
            return Ok(cmd, $"GameObject '{cmd.target}' eliminado.");
        }

        private static AICommandResult RenameGameObject(AICommand cmd)
        {
            GameObject go = FindByName(cmd.target);
            if (go == null)
                return Fail(cmd, $"No se encontró el GameObject '{cmd.target}'.");

            Undo.RecordObject(go, "KinoLab AI: Renombrar GameObject");
            go.name = cmd.name;
            return Ok(cmd, $"GameObject renombrado a '{cmd.name}'.");
        }

        private static AICommandResult DuplicateGameObject(AICommand cmd)
        {
            GameObject go = FindByName(cmd.target);
            if (go == null)
                return Fail(cmd, $"No se encontró el GameObject '{cmd.target}'.");

            GameObject clone = UnityEngine.Object.Instantiate(go, go.transform.parent);
            clone.name = string.IsNullOrWhiteSpace(cmd.name) ? go.name + " (Copy)" : cmd.name;

            Undo.RegisterCreatedObjectUndo(clone, "KinoLab AI: Duplicar GameObject");
            return Ok(cmd, $"GameObject '{cmd.target}' duplicado como '{clone.name}'.");
        }

        // ==========================================
        // Componentes
        // ==========================================

        private static AICommandResult AddComponent(AICommand cmd)
        {
            GameObject go = FindByName(cmd.target);
            if (go == null)
                return Fail(cmd, $"No se encontró el GameObject '{cmd.target}'.");

            Type type = ResolveComponentType(cmd.component);
            if (type == null)
                return Fail(cmd, $"Componente desconocido: '{cmd.component}'.");

            Undo.AddComponent(go, type);
            return Ok(cmd, $"Componente '{type.Name}' agregado a '{cmd.target}'.");
        }

        private static AICommandResult RemoveComponent(AICommand cmd)
        {
            GameObject go = FindByName(cmd.target);
            if (go == null)
                return Fail(cmd, $"No se encontró el GameObject '{cmd.target}'.");

            Type type = ResolveComponentType(cmd.component);
            if (type == null)
                return Fail(cmd, $"Componente desconocido: '{cmd.component}'.");

            Component component = go.GetComponent(type);
            if (component == null)
                return Fail(cmd, $"'{cmd.target}' no tiene el componente '{cmd.component}'.");

            Undo.DestroyObjectImmediate(component);
            return Ok(cmd, $"Componente '{type.Name}' eliminado de '{cmd.target}'.");
        }

        private static Type ResolveComponentType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(SafeGetTypes)
                .FirstOrDefault(t =>
                    typeof(Component).IsAssignableFrom(t) &&
                    !t.IsAbstract &&
                    string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        private static Type[] SafeGetTypes(System.Reflection.Assembly assembly)
        {
            try { return assembly.GetTypes(); }
            catch { return Array.Empty<Type>(); }
        }

        // ==========================================
        // Transform / jerarquía
        // ==========================================

        private static AICommandResult SetTransform(AICommand cmd)
        {
            GameObject go = FindByName(cmd.target);
            if (go == null)
                return Fail(cmd, $"No se encontró el GameObject '{cmd.target}'.");

            Undo.RecordObject(go.transform, "KinoLab AI: Set Transform");
            ApplyTransform(go.transform, cmd, onlyIfProvided: false);

            return Ok(cmd, $"Transform de '{cmd.target}' actualizado.");
        }

        private static void ApplyTransform(Transform t, AICommand cmd, bool onlyIfProvided)
        {
            // En modo "creación", solo aplicamos si el LLM especificó algo
            // distinto del default (0,0,0 / escala 1,1,1) para no pisar valores.
            bool hasPosition = cmd.x != 0 || cmd.y != 0 || cmd.z != 0;
            bool hasRotation = cmd.rx != 0 || cmd.ry != 0 || cmd.rz != 0;
            bool hasScale = cmd.sx != 1 || cmd.sy != 1 || cmd.sz != 1;

            if (!onlyIfProvided || hasPosition)
                t.localPosition = new Vector3(cmd.x, cmd.y, cmd.z);

            if (!onlyIfProvided || hasRotation)
                t.localEulerAngles = new Vector3(cmd.rx, cmd.ry, cmd.rz);

            if (!onlyIfProvided || hasScale)
                t.localScale = new Vector3(cmd.sx, cmd.sy, cmd.sz);
        }

        private static AICommandResult SetParent(AICommand cmd)
        {
            GameObject child = FindByName(cmd.target);
            if (child == null)
                return Fail(cmd, $"No se encontró el GameObject hijo '{cmd.target}'.");

            GameObject parent = FindByName(cmd.name);
            if (parent == null)
                return Fail(cmd, $"No se encontró el GameObject padre '{cmd.name}'.");

            Undo.SetTransformParent(child.transform, parent.transform, "KinoLab AI: Set Parent");
            return Ok(cmd, $"'{cmd.target}' ahora es hijo de '{cmd.name}'.");
        }

        // ==========================================
        // Materiales
        // ==========================================

        private static AICommandResult CreateMaterial(AICommand cmd)
        {
            string folder = "Assets/Materials";
            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            string matName = string.IsNullOrWhiteSpace(cmd.name) ? "NewMaterial" : cmd.name;
            string path = $"{folder}/{matName}.mat";

            Shader shader = Shader.Find("Universal Render Pipeline/Lit")
                             ?? Shader.Find("Standard")
                             ?? Shader.Find("Sprites/Default");

            Material mat = new Material(shader);

            Color color = ParseColor(cmd.color, Color.white);
            ApplyMaterialColor(mat, color);

            AssetDatabase.CreateAsset(mat, path);
            AssetDatabase.SaveAssets();

            string colorInfo = string.IsNullOrWhiteSpace(cmd.color)
                ? "" : $" (color: {cmd.color})";

            return Ok(cmd, $"Material '{matName}' creado en {path}{colorInfo}.");
        }

        /// <summary>
        /// URP usa "_BaseColor" y Built-in RP usa "_Color". Material.color
        /// solo escribe "_Color", así que en URP se queda blanco en
        /// silencio si no detectamos la propiedad correcta.
        /// </summary>
        private static void ApplyMaterialColor(Material mat, Color color)
        {
            if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", color);
            else if (mat.HasProperty("_Color"))
                mat.SetColor("_Color", color);
            else
                mat.color = color;
        }

        private static Color ParseColor(string value, Color fallback)
        {
            if (string.IsNullOrWhiteSpace(value))
                return fallback;

            string v = value.Trim();

            switch (v.ToLowerInvariant())
            {
                case "red": return Color.red;
                case "green": return Color.green;
                case "blue": return Color.blue;
                case "black": return Color.black;
                case "white": return Color.white;
                case "yellow": return Color.yellow;
                case "cyan": return Color.cyan;
                case "magenta": return Color.magenta;
                case "gray":
                case "grey": return Color.gray;
            }

            if (!v.StartsWith("#"))
                v = "#" + v;

            if (ColorUtility.TryParseHtmlString(v, out Color parsed))
                return parsed;

            return fallback;
        }

        private static AICommandResult AssignMaterial(AICommand cmd)
        {
            GameObject go = FindByName(cmd.target);
            if (go == null)
                return Fail(cmd, $"No se encontró el GameObject '{cmd.target}'.");

            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer == null)
                return Fail(cmd, $"'{cmd.target}' no tiene Renderer.");

            string[] guids = AssetDatabase.FindAssets($"{cmd.name} t:Material");
            if (guids.Length == 0)
                return Fail(cmd, $"No se encontró un material llamado '{cmd.name}'.");

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            Undo.RecordObject(renderer, "KinoLab AI: Assign Material");
            renderer.sharedMaterial = mat;

            return Ok(cmd, $"Material '{cmd.name}' asignado a '{cmd.target}'.");
        }

        // ==========================================
        // Prefabs
        // ==========================================

        private static AICommandResult CreatePrefabFromTarget(AICommand cmd)
        {
            GameObject go = FindByName(cmd.target);
            if (go == null)
                return Fail(cmd, $"No se encontró el GameObject '{cmd.target}'.");

            string folder = "Assets/Prefabs";
            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            string prefabName = string.IsNullOrWhiteSpace(cmd.name) ? go.name : cmd.name;
            string path = $"{folder}/{prefabName}.prefab";

            PrefabUtility.SaveAsPrefabAsset(go, path);

            return Ok(cmd, $"Prefab '{prefabName}' creado en {path}.");
        }

        // ==========================================
        // Utilidades
        // ==========================================

        private static GameObject FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            // GameObject.Find solo encuentra objetos activos en la escena.
            return GameObject.Find(name);
        }

        private static AICommandResult Ok(AICommand cmd, string message) =>
            new AICommandResult { Success = true, Message = message };

        private static AICommandResult Fail(AICommand cmd, string message) =>
            new AICommandResult { Success = false, Message = message };
    }
}
