using System.Drawing;
using System.Drawing.Drawing2D;
using Shades.Types;

namespace Shades.Shapes
{
    public class SquaredShadeItem : ShadeItem
    {
        public SquaredShadeItem(float x, float y, ShadeColor color,
            bool filled = false, ShadeLineCap lineCap = ShadeLineCap.Butt)
            : base(x, y, color, filled, false, lineCap) { }

        public override void Draw(Graphics g, ShadeConfig config, ShadePoint offset)
        {
            for (int i = 0; i < config.NbrShades; i++)
            {
                float factor = 1f - (config.GradRatio * i) / 100f;
                float rx = config.Center.X + (Position.X - config.TotalWidth / 2f) + offset.X * factor;
                float ry = config.Center.Y + (Position.Y - config.TotalHeight / 2f) + offset.Y * factor;

                using var path = new GraphicsPath();
                path.AddRectangle(new RectangleF(rx, ry, config.Width, config.Height));
                DrawPath(g, path, config, i);
            }
        }
    }
}
