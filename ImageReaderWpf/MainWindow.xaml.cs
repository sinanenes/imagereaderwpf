using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Tesseract;

namespace ImageReaderWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoCaptureDevice videoSource;
        private Bitmap? capturedImage;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenCameraButton_Click(object sender, RoutedEventArgs e)
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("No camera devices found.");
                return;
            }

            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {

            //BitmapImage bitmapImage = ConvertToBitmapImage((Bitmap)eventArgs.Frame.Clone());

            BitmapImage bitmapImage = null;

            // Create BitmapImage on UI thread
            Dispatcher.Invoke(() =>
            {
                bitmapImage = ConvertToBitmapImage((Bitmap)eventArgs.Frame.Clone());
            });

            Dispatcher.Invoke(() =>
            {
                CameraFeed.Source = bitmapImage;
            });
        }

        private void CaptureImageButton_Click(object sender, RoutedEventArgs e)
        {
            videoSource.SignalToStop();

            capturedImage = new Bitmap(GetBitmapFromImageControl(CameraFeed));

            BitmapImage bitmapImage = ConvertToBitmapImage(capturedImage);

            CapturedImage.Source = bitmapImage;
        }

        private Bitmap? GetBitmapFromImageControl(System.Windows.Controls.Image imageControl)
        {

            // Check if the Image control has an image source
            if (imageControl.Source is BitmapSource bitmapSource)
            {
                // Convert BitmapSource to Bitmap
                Bitmap bitmap = ConvertBitmapSourceToBitmap(bitmapSource);

                // Use the bitmap as needed
                // For example, you can save it to a file
                //bitmap.Save("image.bmp");

                return bitmap;

            }

            return null;
        }

        private Bitmap ConvertBitmapSourceToBitmap(BitmapSource bitmapSource)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(memory);

                return new Bitmap(memory);
            }
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            if (capturedImage != null)
            {
                // Process the captured image (e.g., save to file, further analysis)
                // For demonstration, I'll just display the image path
                MessageBox.Show("Approved! Image captured.");
            }
            else
            {
                MessageBox.Show("No image captured yet.");
            }
        }

        private void DenyButton_Click(object sender, RoutedEventArgs e)
        {
            CapturedImage.Source = null;
            capturedImage = null;
            MessageBox.Show("Image capture denied.");
        }

        private BitmapImage ConvertToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }


        private static string ExtractText(Bitmap image)
        {
            // Perform OCR on the captured image using Tesseract
            using (var engine = new TesseractEngine(@"C:\OCR\Tesseract\tessdata\", "tur", EngineMode.Default))
            {
                // using (var img = Pix.LoadFromFile(image).LoadFromFile("path_to_your_image.jpg")) // Load image directly into Pix
                // {
                ImageConverter converter = new ImageConverter();
                var imgByteArray = (byte[])converter.ConvertTo(image, typeof(byte[]));
                var img = Pix.LoadFromMemory(imgByteArray);


                using (var page = engine.Process(img))
                {

                    return page.GetText().Trim() != string.Empty ? page.GetText() : "Yazı Okunamadı";
                }
                // }
            }
        }

        private void OcrRead_Click(object sender, RoutedEventArgs e)
        {
            string readOcr = "Yazı Bulunamadı";
            if (capturedImage != null)
            {
                readOcr = ExtractText(capturedImage);
            }

            OcrText.Text = readOcr;
        }
    }
}