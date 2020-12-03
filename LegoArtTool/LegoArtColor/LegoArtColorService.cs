using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace LegoArtTool.LegoArtColor
{
    public class LegoArtColorService
    {
        public List<LegoArtColorInfo> GetLegoColors()
        {
            var legoColors = new List<LegoArtColorInfo>();

            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(0, 0, 0), 1, 698));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(102, 101, 96), 2, 141));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(167, 166, 162), 3, 51));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(246, 246, 248), 4, 149));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(0, 53, 94), 5, 121));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(101, 135, 162), 6, 52));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(121, 196, 236), 7, 57));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(253, 104, 0), 8, 74));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(255, 170, 0), 9, 65));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(229, 205, 135), 10, 283));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(163, 129, 93), 11, 137));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(196, 115, 54), 12, 29));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(206, 81, 0), 13, 85));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(119, 40, 0), 14, 250));
            legoColors.Add(new LegoArtColorInfo(Color.FromArgb(66, 23, 3), 15, 554));

            return legoColors;
        }

        public List<LegoArtColorInfo> ParseImage(Bitmap bitmap)
        {
            var parsedLegoColors = GetLegoColors();

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
