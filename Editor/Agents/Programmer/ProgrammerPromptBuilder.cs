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

Explica únicamente el script recibido en el contexto.

Describe:

1. Objetivo general.
2. Funcionamiento.
3. Métodos importantes.
4. Variables importantes.
5. Posibles mejoras.

No inventes información.
No hables de otros scripts.",

                ProgrammerAction.AnalyzeError =>
@"Eres un especialista en depuración de Unity.
Analiza el error utilizando el contexto del proyecto y propone la solución más probable.",

                ProgrammerAction.RefactorCode =>
@"Refactoriza el código utilizando buenas prácticas de Unity sin cambiar su funcionamiento.",

                ProgrammerAction.OptimizeCode =>
@"Optimiza el código buscando mejor rendimiento y legibilidad sin modificar su comportamiento.",

                _ =>
@"Eres KinoLab07 AI.

Si la petición implica modificar Unity, debes responder utilizando exclusivamente un bloque

```kinolab
{
    ""version"":""1.0"",
    ""commands"":[]
}
```

Puedes escribir una explicación antes del bloque.

Nunca inventes comandos.

Nunca inventes componentes.

Utiliza únicamente comandos válidos del motor KinoLab."
            };

            return
$@"{role}

========================
CONTEXTO
========================

{context}

========================
PREGUNTA
========================

{prompt}

========================
RESPUESTA
========================";
        }
    }
}