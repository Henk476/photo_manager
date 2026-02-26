using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using CanutePhotoOrg.Properties;

namespace CanutePhotoOrg
{
    internal static class ThemeManager
    {
        private const int DwmaUseImmersiveDarkMode = 20;
        private const int DwmaUseImmersiveDarkModeLegacy = 19;

        private const string SystemPreference = "System";
        private const string LightPreference = "Light";
        private const string DarkPreference = "Dark";

        private sealed class ThemePalette
        {
            public Color FormBackColor { get; set; }
            public Color SurfaceColor { get; set; }
            public Color InputBackColor { get; set; }
            public Color PrimaryTextColor { get; set; }
            public Color InputTextColor { get; set; }
            public Color BorderColor { get; set; }
            public bool IsDark { get; set; }
        }

        public static string NormalizeThemePreference(string preference)
        {
            if (string.Equals(preference, LightPreference, StringComparison.OrdinalIgnoreCase))
            {
                return LightPreference;
            }

            if (string.Equals(preference, DarkPreference, StringComparison.OrdinalIgnoreCase))
            {
                return DarkPreference;
            }

            return SystemPreference;
        }

        public static void ApplyTheme(Form form)
        {
            string preference = NormalizeThemePreference(Settings.Default.ThemePreference);
            bool useDarkTheme = ShouldUseDarkTheme(preference);
            ThemePalette palette = useDarkTheme ? CreateDarkPalette() : CreateLightPalette();

            ApplyTitleBarTheme(form, useDarkTheme);

            form.BackColor = palette.FormBackColor;
            form.ForeColor = palette.PrimaryTextColor;
            ApplyToControls(form.Controls, palette);
        }

        private static void ApplyTitleBarTheme(Form form, bool useDarkTheme)
        {
            if (!form.IsHandleCreated)
            {
                void OnHandleCreated(object sender, EventArgs e)
                {
                    form.HandleCreated -= OnHandleCreated;
                    ApplyTitleBarTheme(form, useDarkTheme);
                }

                form.HandleCreated += OnHandleCreated;
                return;
            }

            try
            {
                int enabled = useDarkTheme ? 1 : 0;
                IntPtr handle = form.Handle;
                int attributeSize = sizeof(int);

                int result = DwmSetWindowAttribute(handle, DwmaUseImmersiveDarkMode, ref enabled, attributeSize);
                if (result != 0)
                {
                    DwmSetWindowAttribute(handle, DwmaUseImmersiveDarkModeLegacy, ref enabled, attributeSize);
                }
            }
            catch
            {
                // Ignore title bar theming errors to avoid affecting app startup.
            }
        }

        private static bool ShouldUseDarkTheme(string preference)
        {
            if (string.Equals(preference, DarkPreference, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(preference, LightPreference, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return IsWindowsDarkThemeEnabled();
        }

        private static bool IsWindowsDarkThemeEnabled()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    object value = key?.GetValue("AppsUseLightTheme");
                    if (value is int intValue)
                    {
                        return intValue == 0;
                    }

                    if (value is long longValue)
                    {
                        return longValue == 0;
                    }
                }
            }
            catch
            {
                // Ignore registry read errors and fall back to light theme.
            }

            return false;
        }

        private static void ApplyToControls(Control.ControlCollection controls, ThemePalette palette)
        {
            foreach (Control control in controls)
            {
                if (control is Label label)
                {
                    label.ForeColor = palette.PrimaryTextColor;
                    label.BackColor = Color.Transparent;
                }
                else if (control is TextBox textBox)
                {
                    textBox.BackColor = palette.InputBackColor;
                    textBox.ForeColor = palette.InputTextColor;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is ThemedComboBox themedComboBox)
                {
                    themedComboBox.BackColor = palette.InputBackColor;
                    themedComboBox.ForeColor = palette.InputTextColor;
                    themedComboBox.FlatStyle = FlatStyle.Flat;
                    themedComboBox.BorderColor = palette.BorderColor;
                    themedComboBox.ArrowColor = palette.InputTextColor;
                    themedComboBox.ArrowBackColor = palette.SurfaceColor;
                    themedComboBox.Invalidate();
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.BackColor = palette.InputBackColor;
                    comboBox.ForeColor = palette.InputTextColor;
                    comboBox.FlatStyle = FlatStyle.Flat;
                }
                else if (control is Button button)
                {
                    button.BackColor = palette.SurfaceColor;
                    button.ForeColor = palette.PrimaryTextColor;
                    button.UseVisualStyleBackColor = false;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 1;
                    button.FlatAppearance.BorderColor = palette.BorderColor;
                    button.FlatAppearance.MouseOverBackColor = palette.IsDark
                        ? Color.FromArgb(64, 64, 64)
                        : Color.FromArgb(216, 216, 216);
                    button.FlatAppearance.MouseDownBackColor = palette.IsDark
                        ? Color.FromArgb(74, 74, 74)
                        : Color.FromArgb(206, 206, 206);
                }
                else if (control is ProgressBar progressBar)
                {
                    progressBar.BackColor = palette.InputBackColor;
                }
                else
                {
                    control.BackColor = palette.FormBackColor;
                    control.ForeColor = palette.PrimaryTextColor;
                }

                if (control.HasChildren)
                {
                    ApplyToControls(control.Controls, palette);
                }
            }
        }

        private static ThemePalette CreateLightPalette()
        {
            return new ThemePalette
            {
                FormBackColor = Color.FromArgb(240, 240, 240),
                SurfaceColor = Color.FromArgb(230, 230, 230),
                InputBackColor = Color.White,
                PrimaryTextColor = Color.Black,
                InputTextColor = Color.Black,
                BorderColor = Color.FromArgb(160, 160, 160),
                IsDark = false
            };
        }

        private static ThemePalette CreateDarkPalette()
        {
            return new ThemePalette
            {
                FormBackColor = Color.FromArgb(32, 32, 32),
                SurfaceColor = Color.FromArgb(52, 52, 52),
                InputBackColor = Color.FromArgb(42, 42, 42),
                PrimaryTextColor = Color.Gainsboro,
                InputTextColor = Color.WhiteSmoke,
                BorderColor = Color.FromArgb(90, 90, 90),
                IsDark = true
            };
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);
    }
}
