using System.Drawing;
using System.Drawing.Drawing2D;
using Shades.Types;

namespace Shades.Shapes
{
    public class CircleShadeItem : ShadeItem
    {
        public CircleShadeItem(float x, float y, ShadeColor color,
            bool filled = false, ShadeLineCap lineCap = ShadeLineCap.Butt)
            : base(x, y, color, filled, false, lineCap) { }

        public override void Draw(Graphics g, ShadeConfig config, ShadePoint offset)
        {
            for (int i = 0; i < config.NbrShades; i++)
            {
                float factor = 1f - (config.GradRatio * i) / 100f;
                float cx = config.Center.X + (Position.X - config.TotalWidth / 2f + config.Width / 2f) + offset.X * factor;
                float cy = config.Center.Y + (Position.Y - config.TotalHeight / 2f + config.Width / 2f) + offset.Y * factor;
                float r = config.Width / 2f;

                using var path = new GraphicsPath();
                path.AddEllipse(cx - r, cy - r, 2 * r, 2 * r);
                DrawPath(g, path, config, i);
            }
        }
    }
}
