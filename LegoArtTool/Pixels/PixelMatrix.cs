using System;
using System.Drawing;

namespace LegoArtTool.Pixels
{
    public class PixelMatrix
    {
        public Pixel[,] Pixels { get; private set; }
        public int MatrixSize { get; private set; }
        public int PixelSize { get; private set; }

        private Random rnd = new Random();

        public PixelMatrix(int pixelSize, Bitmap sourceBitmap)
        {
            MatrixSize = sourceBitmap.Width;
            PixelSize = pixelSize;
            Pixels = new Pixel[MatrixSize, MatrixSize];

            for (int x = 0; x < MatrixSize; x++)
            {
                for (int y = 0; y < MatrixSize; y++)
                {
                    var pixel = sourceBitmap.GetPixel(x, y);
                    var sourceColor = Color.FromArgb(pixel.ToArgb());
                    Pixels[x, y] = new Pixel(sourceColor, PixelSize, 9);
                }
            }
        }

        public Bitmap ConvertBitmap()
        {
            var bitmapSize = MatrixSize * PixelSize;
            var bitmap = new Bitmap(bitmapSize, bitmapSize);
            using (var g = Graphics.FromImage(bitmap))
            {
                for (int x = 0; x < MatrixSize; x++)
                {
                    for (int y = 0; y < MatrixSize; y++)
                    {
                        var pixelBitmap = Pixels[x, y].GetBitmap();
                        g.DrawImage(pixelBitmap, x * pixelBitmap.Width, y * pixelBitmap.Height);
                    }
                }
            }

            return bitmap;
        }

        private Color GetRandomColor()
        {
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            return randomColor;
        }
    }
}
