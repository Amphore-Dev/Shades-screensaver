using System;
using System.Globalization;
using Microsoft.Win32;

namespace Shades
{
    internal static class Settings
    {
        private const string RegKey = @"Software\Shades";

        public static double SceneDuration
        {
            get => GetDouble("SceneDuration", 15.0);
            set => SetDouble("SceneDuration", value);
        }

        public static double FadeDuration
        {
            get => GetDouble("FadeDuration", 0.5);
            set => SetDouble("FadeDuration", value);
        }

        private static double GetDouble(string name, double def)
        {
            using var key = Registry.CurrentUser.OpenSubKey(RegKey);
            if (key?.GetValue(name) is string s &&
                double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out double v))
                return v;
            return def;
        }

        private static void SetDouble(string name, double value)
        {
            using var key = Registry.CurrentUser.CreateSubKey(RegKey);
            key?.SetValue(name, value.ToString(CultureInfo.InvariantCulture));
        }
    }
}
