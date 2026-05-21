using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shades
{
    public class SettingsForm : Form
    {
        private readonly TrackBar _sceneTrack;
        private readonly TrackBar _fadeTrack;
        private readonly Label _sceneLabel;
        private readonly Label _fadeLabel;

        public SettingsForm()
        {
            Text = "Shades — Settings";
            ClientSize = new Size(400, 240);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            _sceneLabel = MakeLabel("", 20, 20);
            _sceneTrack = MakeTrack(20, 44, 5, 120, (int)Settings.SceneDuration);
            _sceneTrack.ValueChanged += (s, e) => UpdateLabels();

            _fadeLabel = MakeLabel("", 20, 110);
            _fadeTrack = MakeTrack(20, 134, 1, 30, (int)Math.Round(Settings.FadeDuration * 10));
            _fadeTrack.ValueChanged += (s, e) => UpdateLabels();

            var ok     = new Button { Text = "OK",     DialogResult = DialogResult.OK,     Size = new Size(80, 28), Location = new Point(220, 200) };
            var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Size = new Size(80, 28), Location = new Point(310, 200) };

            ok.Click     += (s, e) => { Settings.SceneDuration = _sceneTrack.Value; Settings.FadeDuration = _fadeTrack.Value / 10.0; Close(); };
            cancel.Click += (s, e) => Close();

            AcceptButton = ok;
            CancelButton = cancel;

            Controls.AddRange(new Control[] { _sceneLabel, _sceneTrack, _fadeLabel, _fadeTrack, ok, cancel });
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            _sceneLabel.Text = $"Scene duration: {_sceneTrack.Value} s";
            _fadeLabel.Text  = $"Fade duration: {_fadeTrack.Value / 10.0:0.0} s";
        }

        private static Label MakeLabel(string text, int x, int y) => new Label
        {
            Text = text, Location = new Point(x, y), AutoSize = true
        };

        private static TrackBar MakeTrack(int x, int y, int min, int max, int value) => new TrackBar
        {
            Location = new Point(x, y), Size = new Size(360, 45),
            Minimum = min, Maximum = max,
            Value = Math.Max(min, Math.Min(max, value)),
            TickFrequency = (max - min) / 10
        };
    }
}
