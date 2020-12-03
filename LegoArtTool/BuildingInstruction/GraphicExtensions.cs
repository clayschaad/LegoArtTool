using System.Drawing;
using System.Drawing.Drawing2D;

namespace LegoArtTool.BuildingInstruction
{
    public static class GraphicsExtensions
    {
        public static void DrawCircle(this Graphics g, Pen pen, float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius, radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush, float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius, radius + radius, radius + radius);
        }

        public static void DrawText(this Graphics g, Brush brush, Font font, int size, StringAlignment alignment, string text)
        {
            var format = new StringFormat();
            format.LineAlignment = alignment;
            format.Alignment = alignment;

            var rectf = new RectangleF(0, 0, size, size);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString(text, font, brush, rectf, format);
        }

        public static Font GetIdealFontSize(this Graphics g, int size, string referenceText, Font preferedFont)
        {
            var room = new Size(size, size);
            var realSize = g.MeasureString(referenceText, preferedFont);
            var heightScaleRatio = room.Height / realSize.Height;
            var widthScaleRatio = room.Width / realSize.Width;
            var scaleRatio = (heightScaleRatio < widthScaleRatio) ? heightScaleRatio : widthScaleRatio;
            var scaleFontSize = preferedFont.Size * scaleRatio;
            return new Font(preferedFont.FontFamily, scaleFontSize);
        }
    }
}
