using System.Linq;

namespace GameEngine.Utils {
    public static class StringCache {
        private static readonly string[] _numbers = Enumerable.Range(0, 256).Select(i => i.ToString()).ToArray();
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string Get(int value) => (uint)value < (uint)_numbers.Length ? _numbers[value] : value.ToString();
    }
}