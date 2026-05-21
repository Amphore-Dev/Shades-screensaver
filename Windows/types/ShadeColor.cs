using System.Drawing;

namespace Shades.Types
{
    public struct ShadeColor
    {
        public float R, G, B;

        public ShadeColor(float r, float g, float b) { R = r; G = g; B = b; }

        public string Hex => string.Format("#{0:X2}{1:X2}{2:X2}", (int)R, (int)G, (int)B);

        public Color WithAlpha(float alpha) =>
            Color.FromArgb((int)(alpha * 255f), (int)R, (int)G, (int)B);
    }
}
