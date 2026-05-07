using System;
using System.Runtime.CompilerServices;

namespace GameEngine.Utils {
    public static class IntToStringCache
    {
        [ThreadStatic]
        private static int _lastValue;

        [ThreadStatic]
        private static string? _lastString;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetString(int value)
        {
            var currentCachedString = _lastString;

            if (currentCachedString != null && value == _lastValue)
            {
                return currentCachedString;
            }

            string newValue = value.ToString();
            _lastValue = value;
            _lastString = newValue;

            return newValue;
        }

        public static void Clear()
        {
            _lastString = null;
            _lastValue = 0;
        }
    }

    public static class FloatToStringCache
    {
        [ThreadStatic]
        private static float _lastValue;

        [ThreadStatic]
        private static string? _lastString;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetString(float value)
        {
            var currentCachedString = _lastString;

            if (currentCachedString != null && value == _lastValue)
            {
                return currentCachedString;
            }

            string newValue = value.ToString();
            _lastValue = value;
            _lastString = newValue;

            return newValue;
        }

        public static void Clear()
        {
            _lastString = null;
            _lastValue = 0;
        }
    }
}