namespace LegoArtTool
{
    public class LegoArtColorOffset
    {
        public LegoArtColorInfo LegoArtColorInfo { get; private set; }
        public decimal ColorOffset { get; private set; }
        public bool IsAvailable { get; set; }
        
        public LegoArtColorOffset( decimal colorOffset, LegoArtColorInfo legoArtColorInfo)
        {
            ColorOffset = colorOffset;
            LegoArtColorInfo = legoArtColorInfo;
            IsAvailable = true;
        }


    }
}