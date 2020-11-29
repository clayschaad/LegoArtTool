using System.Drawing;

namespace LegoArtTool
{
    public class LegoColorInfo
    {
        public Color Color { get; private set; }
        public int LegoNumber { get; private set; }
        public int HaveCount { get; private set; }
        public int NeedCount { get; set; }
        
        public LegoColorInfo(Color color, int number, int haveCount)
        {
            Color = color;
            LegoNumber = number;
            HaveCount = haveCount;
        }
    }
}
