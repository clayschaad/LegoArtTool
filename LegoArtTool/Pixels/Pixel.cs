using System.Drawing;

namespace LegoArtTool.Pixels
{
    public class Pixel
    { 
        public Color Color { get; private set; }
        public int Number { get; private set; }

        public const int PixelSize = 50;
        private const int NumberSize = 20;

        public Pixel(Color color, int number)
        {
            Color = color;
            Number = number;
        }

        public Bitmap GetBitmap()
        {
            var bitmap = new Bitmap(PixelSize, PixelSize);
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    bitmap.SetPixel(x, y, Color);
                }
            }

            DrawNumber(bitmap, Number);

            return bitmap;
        }

        private void DrawNumber(Bitmap bitmap, int number)
        {

            using (var g = Graphics.FromImage(bitmap))
            {
                g.FillCircle(Brushes.White, PixelSize / 2, PixelSize / 2, NumberSize / 2);
                var font = g.GetIdealFontSize(NumberSize, "15", new Font("Arial", 8));
                g.DrawText(Brushes.Black, font, PixelSize, StringAlignment.Center, number.ToString());
                g.Flush();
            }
        }
    }
}
