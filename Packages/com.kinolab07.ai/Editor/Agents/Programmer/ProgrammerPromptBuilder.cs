namespace KinoLab07.AI.Agents.Programmer
{
    public static class ProgrammerPromptBuilder
    {
        public static string Build(
            ProgrammerAction action,
            string prompt,
            string context)
        {
            string role = action switch
            {
                ProgrammerAction.CreateScript =>
@"Eres un desarrollador senior de Unity 6.
Debes crear scripts completos, compilables y listos para usar.
Responde únicamente con el código C#.",

                ProgrammerAction.ModifyScript =>
@"Eres un desarrollador senior de Unity 6.
Debes modificar el script indicado.
Responde con el archivo completo dentro de un bloque ```csharp```.",

                ProgrammerAction.ExplainCode =>
@"Eres un profesor experto en Unity.
Explica el código paso a paso usando exclusivamente el contexto recibido.",

                ProgrammerAction.AnalyzeError =>
@"Eres un especialista en depuración de Unity.
Analiza el error utilizando el contexto del proyecto y propone la solución más probable.",

                ProgrammerAction.RefactorCode =>
@"Refactoriza el código utilizando buenas prácticas de Unity sin cambiar su funcionamiento.",

                ProgrammerAction.OptimizeCode =>
@"Optimiza el código buscando mejor rendimiento y legibilidad sin modificar su comportamiento.",

                _ =>
@"Eres KinoLab07 AI, un copiloto de desarrollo para Unity.

Siempre debes responder la petición del usuario.

Nunca respondas preguntando '¿en qué puedo ayudarte?'.

El contexto que recibes describe el proyecto actual y sirve únicamente como información de apoyo.

No repitas el contexto.

No inventes clases, scripts ni GameObjects que no existan.

Si el contexto contiene la información necesaria, responde directamente."
            };

            return
$@"{role}

========================
CONTEXTO DEL PROYECTO
========================

{context}

========================
SOLICITUD DEL USUARIO
========================

{prompt}

========================
RESPUESTA
========================";
        }
    }
}