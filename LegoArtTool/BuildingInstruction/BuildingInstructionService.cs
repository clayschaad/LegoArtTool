using LegoArtTool.LegoArtColor;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace LegoArtTool.BuildingInstruction
{
    public class BuildingInstructionService
    {
        public Bitmap CreateBuildingInstructionImage(Bitmap bitmap, List<LegoArtColorInfo> legoArtColors)
        {
            var pixelMatrix = new BuildingInstructionMatrix(bitmap, legoArtColors);
            return pixelMatrix.GetBitmap();
        }

        public string PersistBuildingInstructions(Bitmap bitmap, string path)
        {
            var segmentSize = bitmap.Width / 3;
            var outPutDirectory = CreateOutPutDirectory(path);

            var segmentNumber = 1;
            for (var h = 0; h < 3; h++)
            {
                for (var w = 0; w < 3; w++)
                {
                    var rectangle = new Rectangle(w * segmentSize, h * segmentSize, segmentSize, segmentSize);
                    var segment = bitmap.Clone(rectangle, bitmap.PixelFormat);
                    segment.Save($"{outPutDirectory}/{segmentNumber++}.png", ImageFormat.Png);
                }
            }

            return outPutDirectory;
        }

        private string CreateOutPutDirectory(string path)
        {
            var increment = 0;
            
            while (Directory.Exists(GetOutputDirectoryPath(increment, path)))
            {
                increment++;
            }

            var outputDirectoryPath = GetOutputDirectoryPath(increment, path);
            Directory.CreateDirectory(outputDirectoryPath);
            return outputDirectoryPath;
        }

        private string GetOutputDirectoryPath(int increment, string path)
        {
            var parent = Directory.GetParent(path);
            var filename = Path.GetFileNameWithoutExtension(path);
            return increment == 0 ? $"{parent}/legoArtInstructions" : $"{parent}/legoArtInstructions_{filename} ({increment})";
        }
    }
}