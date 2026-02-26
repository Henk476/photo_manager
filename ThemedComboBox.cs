using System.Drawing;
using System.Windows.Forms;

namespace CanutePhotoOrg
{
    internal class ThemedComboBox : ComboBox
    {
        private const int WmPaint = 0x000F;
        private const int WmNcPaint = 0x0085;

        public Color BorderColor { get; set; } = Color.Gray;
        public Color ArrowColor { get; set; } = Color.Black;
        public Color ArrowBackColor { get; set; } = Color.WhiteSmoke;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WmPaint || m.Msg == WmNcPaint)
            {
                using (var graphics = Graphics.FromHwnd(Handle))
                using (var pen = new Pen(BorderColor))
                {
                    int buttonWidth = SystemInformation.VerticalScrollBarWidth;
                    var buttonRect = new Rectangle(Width - buttonWidth - 1, 1, buttonWidth, Height - 2);
                    using (var backgroundBrush = new SolidBrush(ArrowBackColor))
                    {
                        graphics.FillRectangle(backgroundBrush, buttonRect);
                    }

                    int centerX = buttonRect.Left + (buttonRect.Width / 2);
                    int centerY = buttonRect.Top + (buttonRect.Height / 2) + 1;
                    using (var arrowPen = new Pen(ArrowColor, 1.6f))
                    {
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        graphics.DrawLines(arrowPen, new[]
                        {
                            new Point(centerX - 4, centerY - 2),
                            new Point(centerX, centerY + 2),
                            new Point(centerX + 4, centerY - 2)
                        });
                    }

                    var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                    graphics.DrawRectangle(pen, rect);
                }
            }
        }
    }
}
