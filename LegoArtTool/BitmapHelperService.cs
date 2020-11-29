using System.Drawing;

namespace LegoArtTool
{
    public class BitmapHelperService
    {
        public Bitmap ScaleImage(string path, int scaleFactor)
        {
            var inputBitmap = new Bitmap(path);
            var scaledBitmap = new Bitmap(inputBitmap.Width * scaleFactor, inputBitmap.Height * scaleFactor);

            for (int x = 0; x < inputBitmap.Width; x++)
            {
                for (int y = 0; y < inputBitmap.Height; y++)
                {
                    var sourcePixel = inputBitmap.GetPixel(x, y);
                    for (int scaleX = x * scaleFactor; scaleX < scaledBitmap.Width; scaleX++)
                    {
                        for (int scaleY = y * scaleFactor; scaleY < scaledBitmap.Height; scaleY++)
                        {
                            scaledBitmap.SetPixel(scaleX, scaleY, Color.FromArgb(sourcePixel.ToArgb()));
                        }
                    }
                }
            }

            return scaledBitmap;
        }
    }
}
