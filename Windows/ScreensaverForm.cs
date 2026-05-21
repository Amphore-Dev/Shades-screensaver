using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Shades.Constants;
using Shades.Shapes;
using Shades.Types;
using Shades.Utils;

namespace Shades
{
    public class ScreensaverForm : Form
    {
        private List<ShadeItem> _items = new List<ShadeItem>();
        private ShadeConfig _config;
        private ShadePoint _offset = new ShadePoint(0, 0);
        private ShadePoint _targetOffset = new ShadePoint(0, 0);
        private int _genCount = 0;
        private double _time = 0;

        // Fade
        private float _fadeOpacity = 0;
        private float _fadeTarget = 1;
        private float _fadeStartOpacity = 0;
        private double _fadeStartTime = 0;
        private bool _isFading = true;
        private bool _pendingRegeneration = false;

        // Regen
        private double _regenTimer = 0;

        private readonly Timer _timer;
        private Point _firstMousePos = Point.Empty;
        protected readonly bool IsPreviewMode;

        public ScreensaverForm(Rectangle bounds, bool isPreview = false)
        {
            IsPreviewMode = isPreview;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.Black;
            ShowInTaskbar = false;
            TopMost = !isPreview;

            if (bounds != Rectangle.Empty)
                Bounds = bounds;

            if (!isPreview)
            {
                MouseMove  += OnMouseMove;
                MouseClick += (s, e) => ExitScreensaver();
                KeyDown    += (s, e) => ExitScreensaver();
            }

            _timer = new Timer { Interval = 16 };
            _timer.Tick += OnTick;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPreviewMode) Cursor.Hide();
            _time = _fadeStartTime = _regenTimer = 0;
            _timer.Start();
            GenerateFirstScene();
        }

        private void OnTick(object sender, EventArgs e)
        {
            _time += 0.016;
            UpdateOffset();
            UpdateFade();

            if (_time - _regenTimer > Settings.SceneDuration && !_isFading)
                TriggerRegeneration();

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Black);

            if (_config == null) return;

            var offset = _offset;
            foreach (var item in _items)
                item.Draw(g, _config, offset);

            float overlayAlpha = 1f - _fadeOpacity;
            if (overlayAlpha > 0.001f)
            {
                using var brush = new SolidBrush(Color.FromArgb((int)(overlayAlpha * 255f), 0, 0, 0));
                g.FillRectangle(brush, 0, 0, ClientSize.Width, ClientSize.Height);
            }
        }

        // MARK: - First scene (clock text with HOME color)

        private void GenerateFirstScene()
        {
            var forceColor = CColors.HOME_COLORS[UMaths.RandInt(0, CColors.HOME_COLORS.Length - 1)];
            float size = ClientSize.Width / 6f;

            _config = new ShadeConfig
            {
                Width = size, Height = size, Spacing = 0,
                Thickness = UMaths.RandInt(1, 20),
                NbrShades = 10, NbrItemsX = 1, NbrItemsY = 1,
                Center = new ShadePoint(ClientSize.Width / 2f, ClientSize.Height / 2f),
                TotalWidth = size, TotalHeight = size,
                GradRatio = 100f / 10f,
                Type = ShadeType.Text,
                Colors = new[] { forceColor },
                MainColor = forceColor,
                LineCap = ShadeLineCap.Butt,
                FillFilter = (m, c) => true,
                RotationFilter = (m, c) => false
            };

            _items = new List<ShadeItem> { new TextShadeItem(0, 0, forceColor, true) };
            _genCount++;
            StartFadeIn();
        }

        // MARK: - Random scene

        private void GenerateRandomScene()
        {
            _config = GenerateConfig();
            GenerateItems();
            _genCount++;
            StartFadeIn();
        }

        private void StartFadeIn()
        {
            _fadeStartTime = _time;
            _fadeStartOpacity = 0;
            _fadeOpacity = 0;
            _fadeTarget = 1;
            _isFading = true;
            _regenTimer = _time;
        }

        private void TriggerRegeneration()
        {
            _fadeStartTime = _time;
            _fadeStartOpacity = _fadeOpacity;
            _fadeTarget = 0;
            _isFading = true;
            _pendingRegeneration = true;
        }

        private ShadeConfig GenerateConfig()
        {
            var types = (ShadeType[])Enum.GetValues(typeof(ShadeType));
            var type = types[UMaths.RandInt(0, types.Length - 1)];

            int nbrShades; float size; int nbrItemsX, nbrItemsY; float spacing;

            if (type == ShadeType.Text)
            {
                nbrShades = 10; size = ClientSize.Width / 6f;
                nbrItemsX = 1; nbrItemsY = 1; spacing = 0;
            }
            else
            {
                nbrShades = UMaths.RandInt(3, CShades.MAX_SHADES_NBR);
                size = UMaths.RandFloat(10, 100);
                nbrItemsX = UMaths.RandInt(3, 20);
                nbrItemsY = UMaths.RandInt(3, 20);
                spacing = UMaths.RandFloat(10, 100);
            }

            var colors = UShadeUtils.GetRandColors();
            var mainColor = colors[UMaths.RandInt(0, colors.Length - 1)];
            float totalWidth  = nbrItemsX * size + Math.Max(0, nbrItemsX - 1) * spacing;
            float totalHeight = nbrItemsY * size + Math.Max(0, nbrItemsY - 1) * spacing;

            return new ShadeConfig
            {
                Width = size, Height = size, Spacing = spacing,
                Thickness = UMaths.RandInt(1, 20),
                NbrShades = nbrShades, NbrItemsX = nbrItemsX, NbrItemsY = nbrItemsY,
                Center = new ShadePoint(ClientSize.Width / 2f, ClientSize.Height / 2f),
                TotalWidth = totalWidth, TotalHeight = totalHeight,
                GradRatio = 100f / nbrShades,
                Type = type, Colors = colors, MainColor = mainColor,
                LineCap = UShadeUtils.GetRandLineCap(),
                FillFilter = UShadeUtils.GetRandFilter(),
                RotationFilter = UShadeUtils.GetRandFilter()
            };
        }

        private void GenerateItems()
        {
            _items.Clear();
            var colors = _genCount > 1 ? UShadeUtils.GetRandColors() : _config.Colors;

            for (int x = 0; x < _config.NbrItemsX; x++)
            {
                for (int y = 0; y < _config.NbrItemsY; y++)
                {
                    var itemColor = colors[UMaths.RandInt(0, colors.Length - 1)];
                    bool isFilled  = _config.FillFilter(_config.MainColor, itemColor);
                    bool hasRotation = _config.RotationFilter(_config.MainColor, itemColor);
                    float px = x * _config.Width + x * _config.Spacing;
                    float py = y * _config.Height + y * _config.Spacing;

                    ShadeItem item;
                    switch (_config.Type)
                    {
                        case ShadeType.Square:
                            item = new SquaredShadeItem(px, py, itemColor, isFilled, _config.LineCap); break;
                        case ShadeType.Circle:
                            item = new CircleShadeItem(px, py, itemColor, isFilled, _config.LineCap); break;
                        case ShadeType.Spiral:
                            item = new SpiralShadeItem(px, py, itemColor, isFilled, true, _config.LineCap); break;
                        case ShadeType.Triangle:
                            var tri = new TriangleShadeItem(px, py, itemColor, isFilled, hasRotation, _config.LineCap);
                            tri.Angle = 90f * (float)Math.PI / 180f;
                            item = tri; break;
                        default: // Text
                            item = new TextShadeItem(px, py, itemColor, true, _config.LineCap); break;
                    }
                    _items.Add(item);
                }
            }
        }

        // MARK: - Offset animation (replaces mouse, matches JS sin/cos movement)

        private void UpdateOffset()
        {
            double t = _time;
            float amplitude = Math.Min(ClientSize.Width, ClientSize.Height) * 0.15f;
            _targetOffset = new ShadePoint(
                (float)(Math.Sin(t * 0.08) * amplitude + Math.Cos(t * 0.05) * amplitude * 0.5),
                (float)(Math.Cos(t * 0.06) * amplitude + Math.Sin(t * 0.04) * amplitude * 0.5)
            );
            float step = 0.02f;
            _offset.X += (_targetOffset.X - _offset.X) * step;
            _offset.Y += (_targetOffset.Y - _offset.Y) * step;
        }

        // MARK: - Fade

        private void UpdateFade()
        {
            if (!_isFading) return;
            float progress = Math.Min((float)((_time - _fadeStartTime) / Settings.FadeDuration), 1f);
            _fadeOpacity = _fadeStartOpacity + (_fadeTarget - _fadeStartOpacity) * progress;
            if (progress >= 1f)
            {
                _fadeOpacity = _fadeTarget;
                _isFading = false;
                if (_pendingRegeneration && _fadeTarget == 0f)
                {
                    _pendingRegeneration = false;
                    GenerateRandomScene();
                }
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_firstMousePos == Point.Empty) { _firstMousePos = e.Location; return; }
            if (Math.Abs(e.X - _firstMousePos.X) > 5 || Math.Abs(e.Y - _firstMousePos.Y) > 5)
                ExitScreensaver();
        }

        protected void ExitScreensaver()
        {
            _timer.Stop();
            Cursor.Show();
            Application.Exit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _timer?.Dispose();
            base.Dispose(disposing);
        }
    }
}
