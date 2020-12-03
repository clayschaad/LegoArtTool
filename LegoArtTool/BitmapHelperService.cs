using System.Drawing;

namespace LegoArtTool
{
    public class BitmapHelperService
    {
        public Bitmap Scale(Bitmap bitmap, int scaleFactor)
        {
            var scaledBitmap = new Bitmap(bitmap.Width * scaleFactor, bitmap.Height * scaleFactor);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var sourcePixel = bitmap.GetPixel(x, y);
                    var sourceColor = Color.FromArgb(sourcePixel.ToArgb());

                    for (int scaleX = x * scaleFactor; scaleX < scaledBitmap.Width; scaleX++)
                    {
                        for (int scaleY = y * scaleFactor; scaleY < scaledBitmap.Height; scaleY++)
                        {
                            scaledBitmap.SetPixel(scaleX, scaleY, sourceColor);
                        }
                    }
                }
            }

            return scaledBitmap;
        }
    }
}
