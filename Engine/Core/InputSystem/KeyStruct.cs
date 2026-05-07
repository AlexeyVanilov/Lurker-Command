using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Core.InputSystem {
    public readonly struct KeyStruct {
        public readonly Keys Key;
        public readonly KeyType KeyType;
        public readonly Action KeyAction;

        public KeyStruct(Keys key, KeyType keyType, Action keyAction) {
            Key = key;
            KeyType = keyType;
            KeyAction = keyAction;
        }
    }

    public enum KeyType {
        Pressed,
        Released,
        Held,
    };
}