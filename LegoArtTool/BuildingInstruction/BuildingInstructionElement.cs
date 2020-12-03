using System;
using System.Drawing;

namespace LegoArtTool.BuildingInstruction
{
    public class BuildingInstructionElement
    { 
        public Color Color { get; private set; }
        public int Number { get; private set; }

        public const int PixelSize = 50;
        private const int NumberSize = 25;

        public BuildingInstructionElement(Color color, int number)
        {
            Color = color;
            Number = number;
        }

        public Bitmap GetBitmap(bool useDots)
        {
            var bitmap = new Bitmap(PixelSize, PixelSize);

            if (useDots)
            {
                DrawDot(bitmap, Color);
                DrawNumber(bitmap, Number, Color);
            }
            else
            {
                DrawPixelBlock(bitmap, Color);
                DrawNumberInCircle(bitmap, Number);
            }

            return bitmap;
        }

        private void DrawDot(Bitmap bitmap, Color color)
        {
            using (var g = Graphics.FromImage(bitmap))
            {
                g.FillCircle(new SolidBrush(color), PixelSize / 2, PixelSize / 2, PixelSize / 2);
                g.Flush();
            }
        }

        private void DrawPixelBlock(Bitmap bitmap, Color color)
        {
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    bitmap.SetPixel(x, y, color);
                }
            }
        }

        private void DrawNumber(Bitmap bitmap, int number, Color color)
        {
            using (var g = Graphics.FromImage(bitmap))
            {
                var font = g.GetIdealFontSize(PixelSize, "15", new Font("Arial", 8));
                var fontColor = IdealTextColor(color);
                g.DrawText(new SolidBrush(fontColor), font, PixelSize, StringAlignment.Center, number.ToString());
                g.Flush();
            }
        }

        private void DrawNumberInCircle(Bitmap bitmap, int number)
        {
            using (var g = Graphics.FromImage(bitmap))
            {
                g.FillCircle(Brushes.White, PixelSize / 2, PixelSize / 2, NumberSize / 2);
                var font = g.GetIdealFontSize(NumberSize, "15", new Font("Arial", 8));
                g.DrawText(Brushes.Black, font, PixelSize, StringAlignment.Center, number.ToString());
                g.Flush();
            }
        }

        public Color IdealTextColor(Color bg)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) + (bg.B * 0.114));

            var foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return foreColor;
        }
    }
}
