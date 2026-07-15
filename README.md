# KinoLab07 AI

**Asistente de Inteligencia Artificial para Unity 6 utilizando Ollama y modelos LLM ejecutados localmente.**

KinoLab07 AI es un copiloto de desarrollo para Unity que integra modelos locales mediante Ollama directamente en el Editor. Su objetivo es asistir al desarrollador comprendiendo el contexto del proyecto y ejecutando herramientas tanto mediante IA como directamente sobre la API de Unity.

---

# Características actuales (v0.7)

## IA

- Chat con modelos locales mediante Ollama.
- Selector automático de modelos instalados.
- Creación automática de scripts C#.
- Modificación automática de scripts.
- Análisis básico de la consola de Unity.

## Motor de comandos kinolab

- El agente puede responder con un bloque ` ```kinolab ` que contiene un
  lote (`AICommandBatch`) de comandos en JSON.
- `AICommandExecutor` ejecuta ese lote directamente sobre la API de Unity:
  crear/eliminar/renombrar/duplicar GameObjects, agregar/quitar componentes,
  mover/rotar/escalar (`SetTransform`), reparentar (`SetParent`), crear
  materiales con color (`CreateMaterial`, compatible con URP y Built-in RP),
  asignar materiales (`AssignMaterial`) y crear prefabs (`CreatePrefab`).
- Todo el batch se agrupa en una sola operación de `Undo` (Ctrl+Z deshace
  el lote completo).
- `AIExecutionService` conecta la respuesta cruda de Ollama con el executor:
  extrae el bloque kinolab (tolerante a fences sin cerrar), lo parsea y
  anexa un resumen legible (✅/❌ por comando) a la respuesta mostrada.

## Contexto inteligente

- Lectura del GameObject seleccionado.
- Lectura del script seleccionado.
- Lectura de la escena activa.
- Lectura de prefabs.
- Indexación automática de scripts del proyecto.

## Herramientas locales

- Arquitectura LocalToolExecutor.
- Atajos instantáneos (sin pasar por el LLM) para crear primitivos
  (cubo, esfera, cápsula, plano, GameObject vacío) — solo se activan con
  la frase mínima ("crea un cubo"); cualquier instrucción con detalles
  adicionales (nombre, color, posición...) se enruta al motor kinolab.
- ReferenceSearch.
- Búsqueda de referencias desde un script seleccionado en el Project.
- Búsqueda de referencias desde un GameObject seleccionado en la Hierarchy.

---

# Requisitos

- Unity 6.x
- Ollama
- Git (opcional)
- macOS (probado)
- Windows (pendiente de pruebas)

---

# Instalación

## 1. Instalar Ollama

https://ollama.com/download

## 2. Descargar un modelo

```bash
ollama pull gpt-oss:20b
```

o

```bash
ollama pull qwen3.6
```

Verificar:

```bash
ollama list
```

## 3. Instalar el paquete

Copiar la carpeta:

```
Packages/com.kinolab07.ai
```

dentro del proyecto Unity.

## 4. Abrir Unity

Unity recompilará automáticamente.

## 5. Abrir el plugin

```
KinoLab07 AI
    → Open
```

---

# Primera prueba

Selecciona un GameObject y escribe:

```
¿Qué componentes tiene seleccionado?
```

o selecciona un script y escribe:

```
¿Dónde se usa este script?
```

---

# Arquitectura

```
com.kinolab07.ai
│
├── Editor
│   ├── Agents
│   │   └── Programmer
│   │       └── LocalTools
│   ├── Commands
│   │   └── GameObjects
│   ├── Controllers
│   │   └── Reference
│   ├── Models
│   ├── Services
│   │   └── Reference
│   ├── Unity
│   ├── Utilities
│   └── Windows
├── CHANGELOG.md
├── README.md
└── package.json
```

> Nota: `Documentation/`, `Runtime/`, `Tests/` y `Editor/Context/` existían
> como carpetas vacías (solo `.meta` huérfanos, sin contenido) y se
> retiraron en v0.7 para mantener el repo limpio. Se recrearán cuando
> tengan contenido real.

---

# Arquitectura de ejecución

```
Usuario
        │
        ▼
ProgrammerAgent
        │
        ├───────────────┐
        │               │
        ▼               ▼
LocalToolExecutor     Ollama
        │                │
        ▼                ▼
   Controllers   AIExecutionService
        │                │
        ▼                ▼
   Unity API     AICommandParser
                          │
                          ▼
                  AICommandExecutor
                          │
                          ▼
                      Unity API
```

Las herramientas locales se ejecutan directamente sobre Unity sin utilizar
el modelo de lenguaje (solo con frases mínimas, ej. "crea un cubo").

Cuando la petición requiere razonamiento o parámetros (nombre, posición,
color, componentes...), el modelo responde con un bloque ` ```kinolab ` que
contiene un lote de comandos JSON. `AIExecutionService` extrae y parsea ese
bloque, y `AICommandExecutor` lo ejecuta contra la API de Unity, agrupado
en una sola operación de Undo.

---

# Referencia rápida: comandos kinolab

El modelo puede responder con:

```
```kinolab
{
  "version": "1.0",
  "commands": [ ... ]
}
```
```

Tipos de comando soportados (`AICommandType`) y campos relevantes:

| Comando              | Campos usados                                   |
|----------------------|--------------------------------------------------|
| CreateGameObject     | `component` (Cube/Sphere/Capsule/Plane/Empty), `name`, `x,y,z`, `rx,ry,rz`, `sx,sy,sz` |
| DeleteGameObject     | `target`                                          |
| RenameGameObject     | `target`, `name`                                  |
| DuplicateGameObject  | `target`, `name`                                  |
| AddComponent / RemoveComponent | `target`, `component` (nombre exacto del tipo de Unity) |
| SetTransform         | `target`, `x,y,z`, `rx,ry,rz`, `sx,sy,sz`         |
| SetParent            | `target` (hijo), `name` (padre)                   |
| CreateMaterial       | `name`, `color` (nombre en inglés o hex `#RRGGBB`) |
| AssignMaterial       | `target`, `name` (material existente)             |
| CreatePrefab         | `target`, `name`                                  |
| CreateScript         | *(pendiente — usar el botón "Crear Script")*      |

El schema completo se documenta en tiempo de ejecución al modelo desde
`ProgrammerPromptBuilder`.

---

# Roadmap

## v0.6 (completado)

- ReferenceSearch para Prefabs.
- ReferenceSearch para Escenas.
- ~~ReferenceSearch para Métodos~~ / ~~ReferenceSearch para Clases~~:
  se habían escrito pero nunca se conectaron a ningún flujo (dead code).
  Se retiraron en v0.7 y quedan pendientes para cuando se implementen
  de verdad (ver v0.8).

## v0.7 (en progreso)

- [x] Motor de comandos kinolab (`AICommandExecutor` + `AIExecutionService`).
- [x] Creación automática de GameObjects vía IA (con nombre/posición/rotación/escala).
- [x] Creación/eliminación de Componentes vía IA.
- [x] Modificación de transform y jerarquía (SetTransform, SetParent) vía IA.
- [x] Creación de materiales con color y asignación a GameObjects.
- [x] Creación de Prefabs vía IA.
- [ ] CreateScript vía kinolab (por ahora usa el flujo existente del botón).
- [ ] Preview/confirmación antes de ejecutar un batch (actualmente se ejecuta directo).
- [ ] Resolución de GameObjects por path/ID en vez de solo `GameObject.Find` por nombre.

## v0.8

- ReferenceSearch para Clases, Métodos y Variables (implementación real,
  conectada a LocalToolExecutor — hasta v0.7 solo existía el parseo del
  prompt, sin ejecución).
- SceneAgent.
- XRHelperAgent.
- DebugAgent.
- OptimizationAgent.

## v1.0

- Instalación mediante UPM.
- Publicación oficial.
- Documentación completa.
- API pública para herramientas.

---

# Licencia

MIT License.
