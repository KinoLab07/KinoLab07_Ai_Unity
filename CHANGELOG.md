# CHANGELOG

Todas las versiones importantes de KinoLab07 AI se registrarán aquí.


## v0.1.0

### Añadido

-   Integración con Ollama.
-   Chat desde Unity.
-   Selector automático de modelos.
-   Generación automática de scripts C#.
-   Modificación de scripts existentes.
-   Lectura del GameObject seleccionado.
-   Lectura del script seleccionado.
-   Análisis básico de la consola de Unity.
-   Integración con Git y GitHub.

### Estado

Primera versión funcional del plugin.

------------------------------------------------------------------------

## Próximas versiones

### v0.2.0

-   Vista previa antes de aplicar cambios.
-   Historial de conversación.
-   Memoria del proyecto.

### v0.3.0

-   Corrección automática de errores.
-   Agente de consola.

### v0.4.0

-   Agente de escena.
-   Agente de prefabs.

## v0.6

### Added
- ReferenceSearch para Prefabs.
- ReferenceSearch para Escenas.

### Removed
- Se retiraron `ClassReferenceSearcher`, `MethodReferenceSearcher` y
  `VariableReferenceSearcher`: quedaron escritos pero nunca conectados a
  ningún flujo (dead code). Se recrearán cuando esa feature se implemente
  de verdad. Ver ROADMAP v0.8.

## v0.7 — Motor de comandos kinolab

### Added
- `AICommand` / `AICommandBatch` / `AICommandType` / `AICommandResult`:
  modelo de datos para comandos generados por el LLM.
- `AICommandParser.TryParse`: parseo tolerante a JSON inválido/ausente.
- `AICommandExecutor`: ejecuta un `AICommandBatch` contra la API de Unity
  (CreateGameObject, DeleteGameObject, RenameGameObject,
  DuplicateGameObject, AddComponent, RemoveComponent, SetTransform,
  SetParent, CreateMaterial, AssignMaterial, CreatePrefab), agrupado en
  una sola operación de `Undo`.
- `CreateMaterial` con color real (`Color` por nombre en inglés o hex
  `#RRGGBB`), compatible con URP (`_BaseColor`) y Built-in RP (`_Color`).
- `AIExecutionService`: extrae el bloque ` ```kinolab ` de la respuesta de
  Ollama (tolerante a fences sin cerrar), lo parsea y ejecuta, anexando un
  resumen legible (✅/❌ por comando) a la respuesta.
- `CreateGameObjectCommand`: helpers de creación de primitivos reutilizados
  tanto por `LocalToolExecutor` como por `AICommandExecutor`.
- `ProgrammerPromptBuilder` documenta el schema completo de `kinolab`
  (tipos de comando y campos válidos) para que el modelo genere JSON
  ejecutable de forma consistente.

### Fixed
- El proyecto no compilaba: `AIExecutionService` y `CreateGameObjectCommand`
  estaban referenciados pero nunca se habían creado (prototipo incompleto).
- `AICommand.type` pasó de `AICommandType` a `string`: `JsonUtility`
  deserializa enums por índice numérico, no por nombre, así que un JSON
  con `"type":"CreateGameObject"` (natural en un LLM) siempre caía en el
  valor por defecto (`Unknown`). Ahora se convierte con `Enum.TryParse`.
- Los atajos locales (`LocalToolExecutor`) usaban `Contains` para detectar
  frases como "crea un cubo", así que un prompt como "crea un cubo rojo
  llamado Enemigo" también matcheaba el atajo y **ignoraba** el resto de
  la instrucción. Ahora requieren coincidencia casi exacta; cualquier
  detalle adicional se enruta al motor kinolab.

### Removed
- Carpetas y `.meta` huérfanos sin contenido real: `Documentation.meta`,
  `Tests.meta`, `Runtime/` (Core, Models, Services), `Editor/Context/`
  (Builders, Models, Providers).
