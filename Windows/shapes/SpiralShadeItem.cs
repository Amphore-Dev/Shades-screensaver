using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Shades.Types;

namespace Shades.Shapes
{
    public class SpiralShadeItem : ShadeItem
    {
        public SpiralShadeItem(float x, float y, ShadeColor color,
            bool filled = false, bool rotation = true, ShadeLineCap lineCap = ShadeLineCap.Butt)
            : base(x, y, color, filled, rotation, lineCap) { }

        public override void Draw(Graphics g, ShadeConfig config, ShadePoint offset)
        {
            for (int i = 0; i < config.NbrShades; i++)
            {
                float factor = 1f - (config.GradRatio * i) / 100f;
                float cx = config.Center.X + (Position.X - config.TotalWidth / 2f + config.Width / 2f) + offset.X * factor;
                float cy = config.Center.Y + (Position.Y - config.TotalHeight / 2f + config.Width / 2f) + offset.Y * factor;
                float r = config.Width / 2f;

                // startAngle and endAngle from Swift (radians) → GDI+ degrees (clockwise, Y-down)
                float startRad = Angle + (config.NbrShades - i) * 8f * (float)Math.PI / 180f;
                float startDeg = -(startRad * 180f / (float)Math.PI); // negate for Y-down
                float sweepDeg = -180f;

                var c = GetColor(config, i);
                var lc = ToGdiCap(LineCap);
                using var pen = new Pen(c, config.Thickness) { StartCap = lc, EndCap = lc };
                g.DrawArc(pen, cx - r, cy - r, 2 * r, 2 * r, startDeg, sweepDeg);

                if (Rotation)
                    Angle += 2f / config.NbrShades * (float)Math.PI / 180f;
            }
        }

        private static System.Drawing.Drawing2D.LineCap ToGdiCap(ShadeLineCap cap)
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
