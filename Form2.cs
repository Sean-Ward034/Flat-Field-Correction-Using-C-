using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlatFieldCorrectionApp
{
    public partial class Form2 : Form
    {
        public Bitmap OriginalImage { get; set; }
        public Bitmap ProcessedImage { get; set; }

        public Form2(Bitmap inputImage)
        {
            InitializeComponent();
            OriginalImage = (Bitmap)inputImage.Clone();
            ProcessedImage = OriginalImage;

            UpdatePreview();  // Display the original image in preview
        }

        private void btnDenoise_Click(object sender, EventArgs e)
        {
            ProcessedImage = ApplyDenoising(ProcessedImage);
            UpdatePreview();  // Update the preview after de-noising
        }

        private void btnSaveEnhancements_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;  // Set dialog result as OK
            this.Close();  // Close Form2
        }

        // Apply a simple de-noising filter (Gaussian blur for now)
        private Bitmap ApplyDenoising(Bitmap image)
        {
            Bitmap denoised = new Bitmap(image.Width, image.Height);

            for (int y = 1; y < image.Height - 1; y++)
            {
                for (int x = 1; x < image.Width - 1; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    Color prevX = image.GetPixel(x - 1, y);
                    Color nextX = image.GetPixel(x + 1, y);
                    Color prevY = image.GetPixel(x, y - 1);
                    Color nextY = image.GetPixel(x, y + 1);

                    int avgR = (pixel.R + prevX.R + nextX.R + prevY.R + nextY.R) / 5;
                    int avgG = (pixel.G + prevX.G + nextX.G + prevY.G + nextY.G) / 5;
                    int avgB = (pixel.B + prevX.B + nextX.B + prevY.B + nextY.B) / 5;

                    denoised.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }
            return denoised;
        }

        // Update the PictureBox preview
        private void UpdatePreview()
        {
            pictureBoxPreview.Image = ProcessedImage;  // Show the enhanced image in the PictureBox
        }

        private void btnApplyExposureFusion_Click(object sender, EventArgs e)
        {
            ProcessedImage = ApplyExposureFusion(OriginalImage);
            MessageBox.Show("Exposure Fusion applied successfully!");
        }

        private void btnIncreaseResolution_Click(object sender, EventArgs e)
        {
            ProcessedImage = IncreaseResolution(OriginalImage, 2.0f);  // Increase resolution by a factor of 2
            MessageBox.Show("Resolution Increased successfully!");
        }

        // Method to apply Exposure Fusion
        private Bitmap ApplyExposureFusion(Bitmap image)
        {
            Bitmap brighter = AdjustBrightness(image, 50);
            Bitmap darker = AdjustBrightness(image, -50);

            Bitmap blendedImage = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color original = image.GetPixel(x, y);
                    Color bright = brighter.GetPixel(x, y);
                    Color dark = darker.GetPixel(x, y);

                    int r = (original.R + bright.R + dark.R) / 3;
                    int g = (original.G + bright.G + dark.G) / 3;
                    int b = (original.B + bright.B + dark.B) / 3;

                    blendedImage.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return blendedImage;
        }

        // Increase Resolution Method
        private Bitmap IncreaseResolution(Bitmap image, float scaleFactor)
        {
            int newWidth = (int)(image.Width * scaleFactor);
            int newHeight = (int)(image.Height * scaleFactor);

            Bitmap highResImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(highResImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));
            }
            return highResImage;
        }

        // Simple brightness adjustment for demonstration purposes
        private Bitmap AdjustBrightness(Bitmap image, int adjustment)
        {
            Bitmap adjustedImage = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    int r = Clamp(pixel.R + adjustment);
                    int g = Clamp(pixel.G + adjustment);
                    int b = Clamp(pixel.B + adjustment);
                    adjustedImage.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return adjustedImage;
        }

        // Helper method to clamp values
        private int Clamp(int value)
        {
            return value < 0 ? 0 : (value > 255 ? 255 : value);
        }
    }
}
