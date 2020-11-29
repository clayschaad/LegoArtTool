using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LegoArtTool.Pixels
{
    public class PixelMatrix
    {
        public Pixel[,] Pixels { get; private set; }
        public int MatrixSize { get; private set; }

        private Random rnd = new Random();

        public PixelMatrix(Bitmap sourceBitmap, List<LegoArtColorInfo> legoArtColors)
        {
            MatrixSize = sourceBitmap.Width;
            Pixels = new Pixel[MatrixSize, MatrixSize];

            for (int x = 0; x < MatrixSize; x++)
            {
                for (int y = 0; y < MatrixSize; y++)
                {
                    var pixel = sourceBitmap.GetPixel(x, y);
                    var sourceColor = Color.FromArgb(pixel.ToArgb());
                    var legoNumber = legoArtColors.Single(l => l.Color == sourceColor).LegoNumber;
                    Pixels[x, y] = new Pixel(sourceColor, legoNumber);
                }
            }
        }

        public Bitmap ConvertBitmap()
        {
            var bitmapSize = MatrixSize * Pixel.PixelSize;
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
