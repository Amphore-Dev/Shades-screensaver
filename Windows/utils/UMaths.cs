using System;

namespace Shades.Utils
{
    public static class UMaths
    {
        private static readonly Random _rng = new Random();

        public static int RandInt(int min, int max) =>
            max > min ? _rng.Next(min, max + 1) : min;

        public static float RandFloat(float min, float max) =>
            (float)(_rng.NextDouble() * (max - min) + min);
    }
}
