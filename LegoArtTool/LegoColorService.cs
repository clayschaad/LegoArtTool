using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace LegoArtTool
{
    public class LegoColorService
    {
        public List<LegoColorInfo> LegoColors { get; private set; }

        public LegoColorService()
        {
            LegoColors = new List<LegoColorInfo>();

            LegoColors.Add(new LegoColorInfo(Color.FromArgb(0, 0, 0), 1, 698));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(102, 101, 96), 2, 141));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(167, 166, 162), 3, 51));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(246, 246, 248), 4, 149));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(0, 53, 94), 5, 121));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(101, 135, 162), 6, 52));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(121, 196, 236), 7, 57));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(253, 104, 0), 8, 74));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(255, 170, 0), 9, 65));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(229, 205, 135), 10, 283));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(163, 129, 93), 11, 137));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(196, 115, 54), 12, 29));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(206, 81, 0), 13, 85));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(119, 40, 0), 14, 250));
            LegoColors.Add(new LegoColorInfo(Color.FromArgb(66, 23, 3), 15, 554));
        }

        public LegoColorInfo GetColorInfo(Color color)
        {
            return LegoColors.Single(l => l.Color == color);
        }
    }
}
