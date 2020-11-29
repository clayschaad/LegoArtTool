using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LegoArt
{
    public class ImageHelperService
    {
        public BitmapImage LoadToImage(System.Drawing.Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                var bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                return bitmapimage;
            }
        }

        public BitmapImage LoadToImage(string path)
        {
            var bitmapimage = new BitmapImage();
            bitmapimage.BeginInit();
            bitmapimage.UriSource = new Uri(path);
            bitmapimage.EndInit();
            return bitmapimage;
        }

        public Color ConvertDrawingToWindowsMediaColor(System.Drawing.Color inputColor)
        {
            return Color.FromArgb(inputColor.A, inputColor.R, inputColor.G, inputColor.B);
        }
    }
}
