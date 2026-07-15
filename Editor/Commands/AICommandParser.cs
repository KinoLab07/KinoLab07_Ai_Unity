using UnityEngine;

namespace KinoLab07.AI.Commands
{
    public static class AICommandParser
    {
        public static AICommandBatch Parse(string json)
        {
            return JsonUtility.FromJson<AICommandBatch>(json);
        }

        /// <summary>
        /// Intenta parsear un bloque kinolab. Nunca lanza excepción:
        /// si el JSON es inválido o está vacío devuelve false y batch=null.
        /// </summary>
        public static bool TryParse(string json, out AICommandBatch batch, out string error)
        {
            batch = null;
            error = null;

            if (string.IsNullOrWhiteSpace(json))
            {
                error = "Bloque kinolab vacío.";
                return false;
            }

            try
            {
                batch = JsonUtility.FromJson<AICommandBatch>(json);

                if (batch == null)
                {
                    error = "No fue posible interpretar el JSON del bloque kinolab.";
                    return false;
                }

                batch.commands ??= new System.Collections.Generic.List<AICommand>();

                return true;
            }
            catch (System.Exception ex)
            {
                error = $"JSON inválido en el bloque kinolab: {ex.Message}";
                return false;
            }
        }
    }
}
