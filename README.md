# KinoLab07 AI

**Asistente de Inteligencia Artificial para Unity 6 usando Ollama y
modelos LLM locales.**

KinoLab07 AI integra modelos locales de IA ejecutándose con Ollama
directamente dentro del Editor de Unity para generar código, modificar
scripts, analizar errores de la consola y asistir el desarrollo de
videojuegos, aplicaciones XR y experiencias de Realidad Virtual y Mixta.

------------------------------------------------------------------------

# Características actuales (v0.1.0)

-   Chat con modelos locales de Ollama.
-   Selector automático de modelos instalados.
-   Lectura del GameObject seleccionado.
-   Lectura automática del script seleccionado.
-   Generación automática de scripts C#.
-   Modificación de scripts existentes.
-   Análisis básico de la consola de Unity.

------------------------------------------------------------------------

# Requisitos

-   Unity 6.x
-   Ollama
-   Git (opcional, recomendado)
-   macOS (probado)
-   Windows (pendiente de pruebas)

------------------------------------------------------------------------

# Instalación

## 1. Instalar Ollama

https://ollama.com/download

## 2. Descargar un modelo

``` bash
ollama pull gpt-oss:20b
```

o

``` bash
ollama pull qwen3.6
```

Verificar:

``` bash
ollama list
```

## 3. Instalar el paquete

Copiar la carpeta:

    Packages/com.kinolab07.ai

dentro del proyecto Unity.

## 4. Abrir Unity

Unity recompilará automáticamente.

## 5. Abrir el plugin

    KinoLab07 AI
        → Open

------------------------------------------------------------------------

# Primera prueba

Escribir:

    Crea un script que haga girar un cubo.

Pulsar:

    Crear Script con IA

------------------------------------------------------------------------

# Estructura

    com.kinolab07.ai
    │
    ├── Documentation
    ├── Editor
    │   ├── Controllers
    │   ├── Models
    │   ├── Services
    │   ├── Unity
    │   ├── Utilities
    │   └── Windows
    ├── Runtime
    └── Tests

------------------------------------------------------------------------

# Roadmap

## v0.2

-   Vista previa antes de aplicar cambios.
-   Historial de conversación.
-   Memoria del proyecto.

## v0.3

-   Corrección automática de errores.
-   Agente de consola.

## v0.4

-   Agente de escena.
-   Agente de prefabs.

## v1.0

-   Instalación mediante UPM.
-   Documentación completa.
-   Publicación oficial.

------------------------------------------------------------------------

# Licencia

MIT License.
