using Microsoft.Win32;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LegoArt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnImageChooser_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                tbImagePath.Text = openFileDialog.FileName;
                LoadImage(openFileDialog.FileName);
            }
        }

        private void LoadImage(string path)
        {
            var legoColorService = new LegoColorService();

            var bitmap = new System.Drawing.Bitmap(path);
            var width = bitmap.Width;
            var height = bitmap.Height;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    var clr = bitmap.GetPixel(h, w);
                    var legoColor = ConvertDrawingToWindowsMediaColor(clr);
                    var legoColorInfo = legoColorService.GetColorInfo(legoColor);
                    legoColorInfo.NeedCount++;
                }
            }

            parentStackPanel.Children.Clear();

            AddLine(Colors.White, "Have", "Needed", "RGB", "#", "Difference", Colors.White);

            var sortedColors = legoColorService.LegoColors.OrderBy(c => c.LegoNumber).ToList();
            for (int i = 0; i < sortedColors.Count; i++)
            {
                var color = sortedColors[i].Color;
                var textRgb = $"{color.R};{color.G};{color.B}";
                var difference = sortedColors[i].HaveCount - sortedColors[i].NeedCount;
                var statusColor = difference < 0 ? Colors.Red : Colors.Green;
                AddLine(color, sortedColors[i].HaveCount.ToString(), sortedColors[i].NeedCount.ToString(), textRgb, sortedColors[i].LegoNumber.ToString(), difference.ToString(), statusColor);
            }

            var have = legoColorService.LegoColors.Sum(l => l.HaveCount);
            var needed = legoColorService.LegoColors.Sum(l => l.NeedCount);
            AddLine(Colors.White, have.ToString(), needed.ToString(), "Total:", "", "", Colors.White);
        }

        private void AddLine(Color color, string textHave, string textNeed, string textRgb, string textNumber, string textDifference, Color statusColor)
        {
            var rect = new Rectangle();
            rect.Width = 20;
            rect.Height = 20;
            rect.Fill = new SolidColorBrush(color);

            var textBlockHave = new TextBlock();
            textBlockHave.Width = 40;
            textBlockHave.Height = 20;
            textBlockHave.TextAlignment = TextAlignment.Center;
            textBlockHave.Text = textHave;

            var textBlockNeed = new TextBlock();
            textBlockNeed.Width = 50;
            textBlockNeed.Height = 20;
            textBlockNeed.TextAlignment = TextAlignment.Center;
            textBlockNeed.Text = textNeed;

            var textBlockRgb = new TextBlock();
            textBlockRgb.Width = 100;
            textBlockRgb.Height = 20;
            textBlockRgb.TextAlignment = TextAlignment.Left;
            textBlockRgb.Text = textRgb;

            var legoNumber = new TextBlock();
            legoNumber.Width = 20;
            legoNumber.Height = 20;
            legoNumber.TextAlignment = TextAlignment.Center;
            legoNumber.Text = textNumber;

            var differenceTextBlock = new TextBlock();
            differenceTextBlock.Width = 55;
            differenceTextBlock.Height = 20;
            differenceTextBlock.TextAlignment = TextAlignment.Center;
            differenceTextBlock.Text = textDifference;

            var status = new Rectangle();
            status.Width = 20;
            status.Height = 20;
            status.Fill = new SolidColorBrush(statusColor);

            var stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Margin = new Thickness(2, 2, 2, 2);
            stackPanel.Children.Add(legoNumber);
            stackPanel.Children.Add(rect);
            stackPanel.Children.Add(textBlockRgb);       
            stackPanel.Children.Add(textBlockHave);
            stackPanel.Children.Add(textBlockNeed);
            stackPanel.Children.Add(differenceTextBlock);
            stackPanel.Children.Add(status);
            parentStackPanel.Children.Add(stackPanel);
        }

        private Color ConvertDrawingToWindowsMediaColor(System.Drawing.Color inputColor)
        {
            return Color.FromArgb(inputColor.A, inputColor.R, inputColor.G, inputColor.B);
        }
    }
}
