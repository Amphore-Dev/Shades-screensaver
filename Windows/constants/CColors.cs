using Shades.Types;

namespace Shades.Constants
{
    public static class CColors
    {
        public static readonly ShadeColor[] HOME_COLORS =
        {
            new ShadeColor(80, 178, 192),
            new ShadeColor(241, 162, 8),
            new ShadeColor(214, 40, 57),
            new ShadeColor(0, 255, 255),
            new ShadeColor(252, 163, 17),
            new ShadeColor(237, 37, 78),
            new ShadeColor(209, 52, 91),
        };

        public static readonly ShadeColor[] LIGHT_COLORS =
        {
            new ShadeColor(255, 255, 255),
            new ShadeColor(0, 255, 0),
            new ShadeColor(255, 0, 255),
            new ShadeColor(255, 255, 0),
            new ShadeColor(255, 105, 120),
            new ShadeColor(7, 190, 184),
            new ShadeColor(186, 50, 79),
            new ShadeColor(23, 86, 118),
            new ShadeColor(75, 163, 195),
            new ShadeColor(255, 200, 87),
            new ShadeColor(250, 120, 30),
            new ShadeColor(223, 41, 53),
            new ShadeColor(129, 195, 215),
            new ShadeColor(49, 203, 0),
            new ShadeColor(17, 152, 34),
            new ShadeColor(155, 197, 91),
        };

        public static readonly ShadeColor[] DARK_COLORS =
        {
            new ShadeColor(0, 0, 0),
            new ShadeColor(0, 0, 0),
            new ShadeColor(0, 0, 0),
            new ShadeColor(0, 0, 0),
        };

        public static readonly ShadeColor[] ALL_COLORS;

        static CColors()
        {
            ALL_COLORS = new ShadeColor[HOME_COLORS.Length + LIGHT_COLORS.Length + DARK_COLORS.Length];
            HOME_COLORS.CopyTo(ALL_COLORS, 0);
            LIGHT_COLORS.CopyTo(ALL_COLORS, HOME_COLORS.Length);
            DARK_COLORS.CopyTo(ALL_COLORS, HOME_COLORS.Length + LIGHT_COLORS.Length);
        }
    }
}
