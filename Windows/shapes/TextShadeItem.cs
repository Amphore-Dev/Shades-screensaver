using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Shades.Types;

namespace Shades.Shapes
{
    public class TextShadeItem : ShadeItem
    {
        private static readonly PrivateFontCollection _pfc = LoadFont();
        private static readonly FontFamily _fontFamily = _pfc.Families.Length > 0
            ? _pfc.Families[0]
            : new FontFamily("Consolas");

        private static PrivateFontCollection LoadFont()
        {
            var pfc = new PrivateFontCollection();
            try
            {
                var asm = Assembly.GetExecutingAssembly();
                using var stream = asm.GetManifestResourceStream("Shades.fonts.Remained.ttf");
                if (stream == null) return pfc;
                var data = new byte[stream.Length];
                stream.ReadExactly(data, 0, data.Length);
                var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                try { pfc.AddMemoryFont(handle.AddrOfPinnedObject(), data.Length); }
                finally { handle.Free(); }
            }
            catch { }
            return pfc;
        }

        private static string CurrentTime => DateTime.Now.ToString("HH:mm");

        public TextShadeItem(float x, float y, ShadeColor color,
            bool filled = true, ShadeLineCap lineCap = ShadeLineCap.Butt)
            : base(x, y, color, filled, false, lineCap) { }

        public override void Draw(Graphics g, ShadeConfig config, ShadePoint offset)
        {
            string text = CurrentTime;

            for (int i = 0; i < config.NbrShades; i++)
            {
                float alpha = Math.Max(0f, 1f - (config.GradRatio * i) / 100f);
                float factor = 1f - (config.GradRatio * i) / 100f;

                using var font = new Font(_fontFamily, config.Width, FontStyle.Regular, GraphicsUnit.Pixel);
                var size = g.MeasureString(text, font);

                float drawX = config.Width / 2f + config.Center.X
                    + (Position.X - config.TotalWidth / 2f) + offset.X * factor - size.Width / 2f;
                float drawY = config.Height / 2f + config.Center.Y
                    + (Position.Y - config.TotalHeight / 2f) + offset.Y * factor - size.Height / 2f;

                using var brush = new SolidBrush(Color.WithAlpha(alpha));
                g.DrawString(text, font, brush, drawX, drawY);
            }
        }
    }
}
