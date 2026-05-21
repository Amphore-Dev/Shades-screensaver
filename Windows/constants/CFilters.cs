using System;
using Shades.Types;
using Shades.Utils;

namespace Shades.Constants
{
    public static class CFilters
    {
        public static readonly ShadeLineCap[] LINE_CAP_TYPES =
            { ShadeLineCap.Butt, ShadeLineCap.Round, ShadeLineCap.Square };

        public static bool SameColorFilter(ShadeColor main, ShadeColor item) => item.Hex == main.Hex;
        public static bool NoFilter(ShadeColor main, ShadeColor item) => true;
        public static bool RandomFilter(ShadeColor main, ShadeColor item) => false;
        public static bool Random50Filter(ShadeColor main, ShadeColor item) => UMaths.RandInt(0, 1) == 1;

        public static readonly Func<ShadeColor, ShadeColor, bool>[] RANDOM_FILTERS =
        {
            SameColorFilter, NoFilter, RandomFilter, Random50Filter,
        };
    }
}
