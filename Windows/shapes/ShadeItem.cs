using System.Drawing;
using System.Drawing.Drawing2D;
using Shades.Types;

namespace Shades.Shapes
{
    public abstract class ShadeItem
    {
        public ShadePoint Position;
        public float Angle = 0;
        public bool Rotation;
        public ShadeColor Color;
        public bool Filled;
        public ShadeLineCap LineCap;

        protected ShadeItem(float x, float y, ShadeColor color,
            bool filled = false, bool rotation = false, ShadeLineCap lineCap = ShadeLineCap.Butt)
        {
            Position = new ShadePoint(x, y);
            Color = color;
            Filled = filled;
            Rotation = rotation;
            LineCap = lineCap;
        }

        public abstract void Draw(Graphics g, ShadeConfig config, ShadePoint offset);

        protected Color GetColor(ShadeConfig config, int i)
        {
            float alpha = System.Math.Max(0f, 1f - (config.GradRatio * i) / 100f);
            return Color.WithAlpha(alpha);
        }

        protected void DrawPath(Graphics g, GraphicsPath path, ShadeConfig config, int i)
        {
            var c = GetColor(config, i);
            if (Filled)
            {
                using var brush = new SolidBrush(c);
                g.FillPath(brush, path);
            }
            else
            {
                var lc = ToGdiLineCap(LineCap);
                using var pen = new Pen(c, config.Thickness) { StartCap = lc, EndCap = lc };
                g.DrawPath(pen, path);
            }
        }

        protected void DrawLine(Graphics g, float x1, float y1, float x2, float y2, ShadeConfig config, int i)
        {
            var c = GetColor(config, i);
            var lc = ToGdiLineCap(LineCap);
            using var pen = new Pen(c, config.Thickness) { StartCap = lc, EndCap = lc };
            g.DrawLine(pen, x1, y1, x2, y2);
        }

        private static System.Drawing.Drawing2D.LineCap ToGdiLineCap(ShadeLineCap cap)
        {
            switch (cap)
            {
                case ShadeLineCap.Round:  return System.Drawing.Drawing2D.LineCap.Round;
                case ShadeLineCap.Square: return System.Drawing.Drawing2D.LineCap.Square;
                default:                  return System.Drawing.Drawing2D.LineCap.Flat;
            }
        }
    }
}
