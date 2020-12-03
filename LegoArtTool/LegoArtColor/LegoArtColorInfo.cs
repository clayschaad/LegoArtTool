using System.Drawing;

namespace LegoArtTool.LegoArtColor
{
    public class LegoArtColorInfo
    {
        public Color Color { get; private set; }
        public int LegoNumber { get; private set; }
        public int HaveCount { get; private set; }
        public int NeedCount { get; set; }
        
        public LegoArtColorInfo(Color color, int number, int haveCount)
        {
            Color = color;
            LegoNumber = number;
            HaveCount = haveCount;
        }
    }
}
