using LegoArtTool.LegoArtColor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LegoArtTool
{
    public class LegoArtImageGenerationService
    {
        private readonly List<LegoArtColorInfo> legoArtColorInfos;

        public LegoArtImageGenerationService()
        {
            var legoArtColorService = new LegoArtColorService();
            legoArtColorInfos = legoArtColorService.GetLegoColors();
        }
        
        public Bitmap GenerateLegoArtImageFromFullColorImage(Bitmap bitmap)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;
            
            var pixels = new List<Pixel>();
            
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    var color = bitmap.GetPixel(h, w);
                    var pixel = MapPixel(color, h ,w);
                    pixels.Add(pixel);
                }
            }
            
            var reducedBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            
            var remainingColors = legoArtColorInfos.ToDictionary(k => k.LegoNumber, v => v.HaveCount);
            
            while (pixels.Count(p => !p.IsAssigned) > 0)
            {
                var minOffset = pixels.Where(p => !p.IsAssigned).SelectMany(p => p.LegoArtColorOffsets.Where(o => o.IsAvailable)
                        .Select(o => o.ColorOffset))
                        .Min(x => x);

                var pixel = pixels.Where(p => !p.IsAssigned).First(p => p.LegoArtColorOffsets.Where(o => o.IsAvailable)
                    .Any(o => o.ColorOffset == minOffset));
                var legoArtColorInfo = pixel.LegoArtColorOffsets.First(o => o.ColorOffset == minOffset).LegoArtColorInfo;
               
                reducedBitmap.SetPixel(pixel.X, pixel.Y, legoArtColorInfo.Color);

                remainingColors[legoArtColorInfo.LegoNumber]--;
                pixel.IsAssigned = true;

                if (remainingColors[legoArtColorInfo.LegoNumber] < 1)
                {
                    var legoArtColorOffsets = pixels.SelectMany(p => p.LegoArtColorOffsets
                            .Where(o => o.LegoArtColorInfo.LegoNumber == legoArtColorInfo.LegoNumber)).ToList();
                    legoArtColorOffsets.ForEach(o => o.IsAvailable = false);
                }
            }

            return reducedBitmap;
        }

        private Pixel MapPixel(Color inputColor, int xPosition, int yPosition)
        {
            var legoArtColorOffsets = legoArtColorInfos.Select(i => ColorDistance(inputColor, i)).ToList();
            return new Pixel(xPosition, yPosition, legoArtColorOffsets);
        }
        
        private static LegoArtColorOffset ColorDistance(Color inputColor, LegoArtColorInfo colorInfo)
        {
            var targetColor = colorInfo.Color;
            var rmean = (inputColor.R + (long)targetColor.R) / 2;
            var r = inputColor.R - (long)targetColor.R;
            var g = inputColor.G - (long)targetColor.G;
            var b = inputColor.B - (long)targetColor.B;
            var offset = (decimal) Math.Sqrt((((512 + rmean) * r * r) >> 8) + 4 * g * g + (((767 - rmean) * b * b) >> 8));
            return new LegoArtColorOffset((decimal) offset, colorInfo);
        }
    }
}