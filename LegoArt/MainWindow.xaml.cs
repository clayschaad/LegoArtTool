using System.Drawing;
using LegoArtTool;
using Microsoft.Win32;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace LegoArt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LegoArtColorService legoArtColorService;
        private readonly BitmapHelperService bitmapHelperService;
        private readonly ImageHelperService imageHelperService;
        private readonly LegoArtImageGenerationService legoArtImageGenerationService;

        public MainWindow()
        {
            InitializeComponent();

            legoArtColorService = new LegoArtColorService();
            bitmapHelperService = new BitmapHelperService();
            imageHelperService = new ImageHelperService();
            legoArtImageGenerationService = new LegoArtImageGenerationService();

            btnInstructionsPersister.Visibility = Visibility.Hidden;
        }

        private void btnImageChooser_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                tbImagePath.Text = openFileDialog.FileName;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                });

                LoadImage(openFileDialog.FileName);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = null;
                });
            }
        }

        private void btnInstructionsPersister_Click(object sender, RoutedEventArgs e)
        {
            var d = new DataObject(DataFormats.Bitmap, scaledImage.Source, true);
            var bitmap = d.GetData(typeof(Bitmap)) as Bitmap;
            var outputPath = BuildingInstructionService.PersistBuildingInstructions(bitmap, tbImagePath.Text);
            MessageBox.Show($"Instructions saved to {outputPath}", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void LoadImage(string path)
        {
            var bitmap = new System.Drawing.Bitmap(path);
            var reducedBitMap = legoArtImageGenerationService.GenerateLegoArtImageFromFullColorImage(bitmap);

            var legoArtColors = legoArtColorService.ParseImage(reducedBitMap);
            if (legoArtColors != null)
            {
                var sourceBitmap = bitmapHelperService.Scale(reducedBitMap, 1);
                sourceImage.Source = imageHelperService.LoadToImage(sourceBitmap);

                var convertedBitmap = bitmapHelperService.ConvertToPixelMatrix(sourceBitmap, legoArtColorService.GetLegoColors());
                scaledImage.Source = imageHelperService.LoadToImage(convertedBitmap);

                btnInstructionsPersister.Visibility = Visibility.Visible;
                
                parentStackPanel.Children.Clear();

                AddLine(System.Drawing.Color.White, "Have", "Needed", "RGB", "#", "Difference", System.Drawing.Color.White);

                var sortedColors = legoArtColors.OrderBy(c => c.LegoNumber).ToList();
                for (int i = 0; i < sortedColors.Count; i++)
                {
                    var color = sortedColors[i].Color;
                    var textRgb = $"{color.R};{color.G};{color.B}";
                    var difference = sortedColors[i].HaveCount - sortedColors[i].NeedCount;
                    var statusColor = difference < 0 ? System.Drawing.Color.Red : System.Drawing.Color.Green;
                    AddLine(color, sortedColors[i].HaveCount.ToString(), sortedColors[i].NeedCount.ToString(), textRgb, sortedColors[i].LegoNumber.ToString(), difference.ToString(), statusColor);
                }

                var have = legoArtColors.Sum(l => l.HaveCount);
                var needed = legoArtColors.Sum(l => l.NeedCount);
                AddLine(System.Drawing.Color.White, have.ToString(), needed.ToString(), "Total:", "", "", System.Drawing.Color.White);
            }
            else
            {
                MessageBox.Show("Image includes wrong colors", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddLine(System.Drawing.Color color, string textHave, string textNeed, string textRgb, string textNumber, string textDifference, System.Drawing.Color statusColor)
        {
            var displayColor = imageHelperService.ConvertDrawingToWindowsMediaColor(color);
            var rect = new Rectangle();
            rect.Width = 20;
            rect.Height = 20;
            rect.Fill = new SolidColorBrush(displayColor);

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

            var displayStatusColor = imageHelperService.ConvertDrawingToWindowsMediaColor(statusColor);
            var status = new Rectangle();
            status.Width = 20;
            status.Height = 20;
            status.Fill = new SolidColorBrush(displayStatusColor);

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
    }
}
