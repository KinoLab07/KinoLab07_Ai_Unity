using System;

namespace KinoLab07.AI.Commands
{
    [Serializable]
    public class AICommand
    {
        // IMPORTANTE: JsonUtility deserializa enums por valor numérico,
        // no por nombre. El LLM escribe el nombre como string
        // ("CreateGameObject"), así que lo recibimos como string y lo
        // convertimos a AICommandType manualmente en AICommandExecutor
        // con Enum.TryParse.
        public string type;

        public string name;
        public string target;
        public string component;
        public string color;
        public float x,y,z;
        public float rx,ry,rz;
        public float sx=1,sy=1,sz=1;
    }
}
