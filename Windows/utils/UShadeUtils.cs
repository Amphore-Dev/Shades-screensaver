using System;
using Shades.Constants;
using Shades.Types;

namespace Shades.Utils
{
    public static class UShadeUtils
    {
        public static ShadeColor[] GetRandColors()
        {
            var colors = new[]
            {
                CColors.ALL_COLORS[UMaths.RandInt(0, CColors.ALL_COLORS.Length - 1)],
                CColors.ALL_COLORS[UMaths.RandInt(0, CColors.ALL_COLORS.Length - 1)],
            };
            int tries = 0;
            while (colors[0].Hex == colors[1].Hex && tries++ < 10)
                colors[1] = CColors.ALL_COLORS[UMaths.RandInt(0, CColors.ALL_COLORS.Length - 1)];
            return colors;
        }

        public static Func<ShadeColor, ShadeColor, bool> GetRandFilter() =>
            CFilters.RANDOM_FILTERS[UMaths.RandInt(0, CFilters.RANDOM_FILTERS.Length - 1)];

        public static ShadeLineCap GetRandLineCap() =>
            CFilters.LINE_CAP_TYPES[UMaths.RandInt(0, CFilters.LINE_CAP_TYPES.Length - 1)];
    }
}
