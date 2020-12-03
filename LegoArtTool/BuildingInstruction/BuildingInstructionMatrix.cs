using LegoArtTool.LegoArtColor;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LegoArtTool.BuildingInstruction
{
    public class BuildingInstructionMatrix
    {
        public BuildingInstructionElement[,] Pixels { get; private set; }
        public int MatrixSize { get; private set; }

        public BuildingInstructionMatrix(Bitmap sourceBitmap, List<LegoArtColorInfo> legoArtColors)
        {
            MatrixSize = sourceBitmap.Width;
            Pixels = new BuildingInstructionElement[MatrixSize, MatrixSize];

            for (int x = 0; x < MatrixSize; x++)
            {
                for (int y = 0; y < MatrixSize; y++)
                {
                    var pixel = sourceBitmap.GetPixel(x, y);
                    var sourceColor = Color.FromArgb(pixel.ToArgb());
                    var legoNumber = legoArtColors.Single(l => l.Color == sourceColor).LegoNumber;
                    Pixels[x, y] = new BuildingInstructionElement(sourceColor, legoNumber);
                }
            }
        }

        public Bitmap GetBitmap(bool useDots)
        {
            var bitmapSize = MatrixSize * BuildingInstructionElement.PixelSize;
            var bitmap = new Bitmap(bitmapSize, bitmapSize);
            using (var g = Graphics.FromImage(bitmap))
            {
                for (int x = 0; x < MatrixSize; x++)
                {
                    for (int y = 0; y < MatrixSize; y++)
                    {
                        var pixelBitmap = Pixels[x, y].GetBitmap(useDots);
                        g.DrawImage(pixelBitmap, x * pixelBitmap.Width, y * pixelBitmap.Height);
                    }
                }
            }

            return bitmap;
        }
    }
}
