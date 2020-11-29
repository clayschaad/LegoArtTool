using System.Drawing;

namespace LegoArtTool.Pixels
{
    public class Pixel
    { 
        public Color Color { get; private set; }
        public int Number { get; private set; }
        public int Size { get; private set; }

        public Pixel(Color color, int size, int number)
        {
            Color = color;
            Size = size;
            Number = number;
        }

        public Bitmap GetBitmap()
        {
            var bitmap = new Bitmap(Size, Size);
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    bitmap.SetPixel(x, y, Color);
                }
            }

            return bitmap;
        }
    }
}
