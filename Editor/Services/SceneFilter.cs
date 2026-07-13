using UnityEngine;

namespace KinoLab07.AI.Services
{
    public static class SceneFilter
    {
        public static bool Ignore(GameObject go)
        {
            string name = go.name;

            if (name.StartsWith("OVR"))
                return true;

            if (name.StartsWith("Meta"))
                return true;

            if (name.StartsWith("SteamVR"))
                return true;

            if (name.StartsWith("XR"))
                return true;

            if (name.StartsWith("Left"))
                return true;

            if (name.StartsWith("Right"))
                return true;

            if (name.StartsWith("Hand"))
                return true;

            if (name.StartsWith("Controller"))
                return true;

            return false;
        }
    }
}