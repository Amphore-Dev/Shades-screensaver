using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Shades.Types;

namespace Shades.Shapes
{
    public class TriangleShadeItem : ShadeItem
    {
        public TriangleShadeItem(float x, float y, ShadeColor color,
            bool filled = false, bool rotation = false, ShadeLineCap lineCap = ShadeLineCap.Butt)
            : base(x, y, color, filled, rotation, lineCap) { }

        public override void Draw(Graphics g, ShadeConfig config, ShadePoint offset)
        {
            for (int i = 0; i < config.NbrShades; i++)
            {
                float factor = 1f - (config.GradRatio * i) / 100f;
                float cx = config.Center.X + (Position.X - config.TotalWidth / 2f + config.Width / 2f) + offset.X * factor;
                float cy = config.Center.Y + (Position.Y - config.TotalHeight / 2f + config.Width / 2f) + offset.Y * factor;
                float w = config.Width;

                var pts = new[]
                {
                    new PointF(cx + (float)Math.Cos(Angle) * w,                     cy + (float)Math.Sin(Angle) * w),
                    new PointF(cx + (float)Math.Cos(Angle + 2 * Math.PI / 3) * w,  cy + (float)Math.Sin(Angle + 2 * Math.PI / 3) * w),
                    new PointF(cx + (float)Math.Cos(Angle + 4 * Math.PI / 3) * w,  cy + (float)Math.Sin(Angle + 4 * Math.PI / 3) * w),
                };

                using var path = new GraphicsPath();
                path.AddPolygon(pts);
                DrawPath(g, path, config, i);

                if (Rotation)
                    Angle += 0.3f / config.NbrShades * (float)Math.PI / 180f;
            }
        }
    }
}
