using System.Collections.Generic;

namespace LegoArtTool
{
    public class Pixel
    { 
        public int X { get; private set; }
        public int Y { get; private set; }
        public List<LegoArtColorOffset> LegoArtColorOffsets { get; private set; }
        
        public bool IsAssigned { get; set; }
        
        public Pixel(int x, int y, List<LegoArtColorOffset> legoArtColorOffsets)
        {
            X = x;
            Y = y;
            LegoArtColorOffsets = legoArtColorOffsets;
            IsAssigned = false;
        }

    }
}