using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace LegoArtTool
{
    public class LegoArtColorService
    {
        public static readonly List<LegoArtColorInfo> LegoColors;

        static LegoArtColorService()
        {
            LegoColors = new List<LegoArtColorInfo>();

            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(0, 0, 0), 1, 698));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(102, 101, 96), 2, 141));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(167, 166, 162), 3, 51));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(246, 246, 248), 4, 149));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(0, 53, 94), 5, 121));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(101, 135, 162), 6, 52));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(121, 196, 236), 7, 57));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(253, 104, 0), 8, 74));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(255, 170, 0), 9, 65));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(229, 205, 135), 10, 283));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(163, 129, 93), 11, 137));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(196, 115, 54), 12, 29));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(206, 81, 0), 13, 85));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(119, 40, 0), 14, 250));
            LegoColors.Add(new LegoArtColorInfo(Color.FromArgb(66, 23, 3), 15, 554));
        }

        public static List<LegoArtColorInfo> ParseImage(Bitmap bitmap)
        {
            var parsedLegoColors = new List<LegoArtColorInfo>(LegoColors);

            var width = bitmap.Width;
            var height = bitmap.Height;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    var clr = bitmap.GetPixel(h, w);
                    var legoColorInfo = parsedLegoColors.SingleOrDefault(l => l.Color == clr);
                    if (legoColorInfo != null)
                    {
                        legoColorInfo.NeedCount++;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return parsedLegoColors;
        }
    }
}
