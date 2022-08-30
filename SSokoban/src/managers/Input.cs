using System.Collections.Generic;
using SFML.Window;

namespace SSokoban.Core
{
    public enum KeyState
    {
        Pressed,
        Released
    }

    public static class Input
    {
        public static Dictionary<Keyboard.Key, KeyState> Keys { get; set; } = new Dictionary<Keyboard.Key, KeyState>();

        public static bool GetKeyPressed(Keyboard.Key key)
        {
            if (Keys.ContainsKey(key))
                if (Keys[key] == KeyState.Pressed)
                    return true;
            return false;
        }

        public static bool GetKeyReleased(Keyboard.Key key)
        {
            if (Keys.ContainsKey(key))
                if (Keys[key] == KeyState.Released)
                    return true;
            return false;
        }

        public static bool GetKeyDown(Keyboard.Key key)
        {
            return Keyboard.IsKeyPressed(key);
        }
    }
}