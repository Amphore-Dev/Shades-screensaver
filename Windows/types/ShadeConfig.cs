using System;

namespace Shades.Types
{
    public class ShadeConfig
    {
        public float Width, Height, Spacing, Thickness;
        public int NbrShades, NbrItemsX, NbrItemsY;
        public ShadePoint Center;
        public float TotalWidth, TotalHeight, GradRatio;
        public ShadeType Type;
        public ShadeColor[] Colors;
        public ShadeColor MainColor;
        public ShadeLineCap LineCap;
        public Func<ShadeColor, ShadeColor, bool> FillFilter;
        public Func<ShadeColor, ShadeColor, bool> RotationFilter;
    }
}
