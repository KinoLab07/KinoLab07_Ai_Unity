# KinoLab07 AI

**Asistente de Inteligencia Artificial para Unity 6 utilizando Ollama y modelos LLM ejecutados localmente.**

KinoLab07 AI es un copiloto de desarrollo para Unity que integra modelos locales mediante Ollama directamente en el Editor. Su objetivo es asistir al desarrollador comprendiendo el contexto del proyecto y ejecutando herramientas tanto mediante IA como directamente sobre la API de Unity.

---

# Características actuales (v0.5)

## IA

- Chat con modelos locales mediante Ollama.
- Selector automático de modelos instalados.
- Creación automática de scripts C#.
- Modificación automática de scripts.
- Análisis básico de la consola de Unity.

## Contexto inteligente

- Lectura del GameObject seleccionado.
- Lectura del script seleccionado.
- Lectura de la escena activa.
- Lectura de prefabs.
- Indexación automática de scripts del proyecto.

## Herramientas locales

- Arquitectura LocalToolExecutor.
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
├── Documentation
├── Editor
│   ├── Agents
│   │   ├── Programmer
│   │   └── LocalTools
│   ├── Context
│   ├── Controllers
│   ├── Models
│   ├── Services
│   ├── Settings
│   ├── UI
│   ├── Unity
│   ├── Utilities
│   └── Windows
├── Runtime
└── Tests
```

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
        │
        ▼
Controllers
        │
        ▼
Unity API
```

Las herramientas locales se ejecutan directamente sobre Unity sin utilizar el modelo de lenguaje.

---

# Roadmap

## v0.6

- ReferenceSearch para Prefabs.
- ReferenceSearch para Escenas.
- ReferenceSearch para Métodos.
- ReferenceSearch para Clases.

## v0.7

- Creación automática de GameObjects.
- Creación automática de Componentes.
- Modificación automática de escenas.
- Creación de materiales.

## v0.8

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
