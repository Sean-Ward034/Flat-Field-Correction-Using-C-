using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlatFieldCorrectionApp
{
    public partial class Form1 : Form
    {
        private Bitmap objectImage;
        private Bitmap darkFrameImage;
        private Bitmap brightFrameImage;
        private Bitmap correctedImage;  // The original corrected image
        private Bitmap editedImage;     // The image that is being edited

        public Form1()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    objectImage = new Bitmap(openFileDialog.FileName);
                    pictureBoxObject.Image = objectImage;

                    GenerateCalibrationImages(objectImage);

                    MessageBox.Show("Dark and Bright Field Images generated and smoothed automatically.");
                }
            }
        }

        private void btnApplyCorrection_Click(object sender, EventArgs e)
        {
            if (objectImage == null)
            {
                MessageBox.Show("Please upload an object image first.");
                return;
            }

            // Apply flat-field correction
            correctedImage = ApplyFlatFieldCorrection(objectImage, darkFrameImage, brightFrameImage);
            pictureBoxCorrected.Image = correctedImage;

            // Enable the "Edit Correction" button
            btnEditCorrection.Enabled = true;
        }

        private void btnEditCorrection_Click(object sender, EventArgs e)
        {
            if (correctedImage == null) return;

            // Make a copy of the corrected image to start editing
            editedImage = new Bitmap(correctedImage);

            // Enable the trackbars for editing
            trackBarBrightness.Enabled = true;
            trackBarContrast.Enabled = true;
            trackBarExposure.Enabled = true;

            // Enable "Save Changes" and "Discard Changes" buttons
            btnSaveChanges.Enabled = true;
            btnDiscardChanges.Enabled = true;
        }

        private void trackBarBrightness_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageAdjustments();
        }

        private void trackBarContrast_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageAdjustments();
        }

        private void trackBarExposure_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageAdjustments();
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (editedImage != null)
            {
                // Save changes by setting correctedImage to editedImage
                correctedImage = new Bitmap(editedImage);
                pictureBoxCorrected.Image = correctedImage;

                // Reset UI controls after saving
                ResetEditingControls();
            }
        }

        private void btnDiscardChanges_Click(object sender, EventArgs e)
        {
            if (correctedImage != null)
            {
                // Discard changes by restoring the original corrected image
                pictureBoxCorrected.Image = correctedImage;

                // Reset UI controls after discarding
                ResetEditingControls();
            }
        }

        private void ResetEditingControls()
        {
            // Disable the editing controls
            trackBarBrightness.Enabled = false;
            trackBarContrast.Enabled = false;
            trackBarExposure.Enabled = false;

            // Disable the save/discard buttons
            btnSaveChanges.Enabled = false;
            btnDiscardChanges.Enabled = false;

            // Reset trackbars to default values
            trackBarBrightness.Value = 0;
            trackBarContrast.Value = 0;
            trackBarExposure.Value = 0;
        }

        private void UpdateImageAdjustments()
        {
            if (editedImage == null) return;

            // Get current slider values
            float brightness = trackBarBrightness.Value / 100.0f;
            float contrast = trackBarContrast.Value / 100.0f;
            float exposure = trackBarExposure.Value / 100.0f;

            // Apply adjustments to the edited image
            Bitmap adjustedImage = AdjustImage(correctedImage, brightness, contrast, exposure);
            editedImage = adjustedImage;

            // Show the adjusted image in the picture box
            pictureBoxCorrected.Image = adjustedImage;
        }

        private Bitmap AdjustImage(Bitmap image, float brightness, float contrast, float exposure)
        {
            Bitmap adjustedImage = new Bitmap(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);

                    // Apply brightness, contrast, and exposure adjustments
                    int r = Clamp((int)(pixel.R * (1 + brightness) * (1 + contrast + exposure)), 0, 255);
                    int g = Clamp((int)(pixel.G * (1 + brightness) * (1 + contrast + exposure)), 0, 255);
                    int b = Clamp((int)(pixel.B * (1 + brightness) * (1 + contrast + exposure)), 0, 255);

                    adjustedImage.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return adjustedImage;
        }

        private void GenerateCalibrationImages(Bitmap objectImage)
        {
            darkFrameImage = CreateDarkField(objectImage);
            brightFrameImage = CreateBrightField(objectImage);

            // Apply Gaussian Smoothing
            darkFrameImage = ApplyGaussianSmoothing(darkFrameImage);
            brightFrameImage = ApplyGaussianSmoothing(brightFrameImage);
        }

        private Bitmap CreateDarkField(Bitmap objectImage)
        {
            return AdjustBrightness(objectImage, 0.2f); // Reduce brightness by 80%
        }

        private Bitmap CreateBrightField(Bitmap objectImage)
        {
            return AdjustBrightness(objectImage, 1.5f); // Increase brightness by 50%
        }

        private Bitmap AdjustBrightness(Bitmap image, float factor)
        {
            Bitmap adjustedImage = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    int adjustedR = Clamp((int)(pixel.R * factor), 0, 255);
                    int adjustedG = Clamp((int)(pixel.G * factor), 0, 255);
                    int adjustedB = Clamp((int)(pixel.B * factor), 0, 255);
                    adjustedImage.SetPixel(x, y, Color.FromArgb(adjustedR, adjustedG, adjustedB));
                }
            }
            return adjustedImage;
        }

        private Bitmap ApplyFlatFieldCorrection(Bitmap rawImage, Bitmap darkFrame, Bitmap brightFrame)
        {
            int width = rawImage.Width;
            int height = rawImage.Height;
            Bitmap correctedImage = new Bitmap(width, height);

            float darkMean = CalculateMeanIntensity(darkFrame);
            float brightMean = CalculateMeanIntensity(brightFrame);
            float gain = Math.Max(brightMean - darkMean, 1); // Prevent division by zero

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color rawPixel = rawImage.GetPixel(x, y);
                    Color darkPixel = darkFrame.GetPixel(x, y);
                    Color brightPixel = brightFrame.GetPixel(x, y);

                    int correctedR = (int)(((rawPixel.R - darkPixel.R) / gain) * 255);
                    int correctedG = (int)(((rawPixel.G - darkPixel.G) / gain) * 255);
                    int correctedB = (int)(((rawPixel.B - darkPixel.B) / gain) * 255);

                    correctedImage.SetPixel(x, y, Color.FromArgb(
                        Clamp(correctedR, 0, 255),
                        Clamp(correctedG, 0, 255),
                        Clamp(correctedB, 0, 255)));
                }
            }

            return correctedImage;
        }

        private Bitmap ApplyGaussianSmoothing(Bitmap image)
        {
            Bitmap smoothed = new Bitmap(image.Width, image.Height);

            int[,] kernel = {
                { 1, 2, 1 },
                { 2, 4, 2 },
                { 1, 2, 1 }
            };
            int kernelSum = 16;

            for (int y = 1; y < image.Height - 1; y++)
            {
                for (int x = 1; x < image.Width - 1; x++)
                {
                    int[] sums = new int[3]; // For R, G, B channels

                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            Color neighbor = image.GetPixel(x + kx, y + ky);
                            int kernelValue = kernel[ky + 1, kx + 1];
                            sums[0] += neighbor.R * kernelValue;
                            sums[1] += neighbor.G * kernelValue;
                            sums[2] += neighbor.B * kernelValue;
                        }
                    }

                    smoothed.SetPixel(x, y, Color.FromArgb(
                        Clamp(sums[0] / kernelSum, 0, 255),
                        Clamp(sums[1] / kernelSum, 0, 255),
                        Clamp(sums[2] / kernelSum, 0, 255)));
                }
            }

            return smoothed;
        }

        private float CalculateMeanIntensity(Bitmap image)
        {
            float sum = 0;
            int pixelCount = image.Width * image.Height;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    sum += (pixel.R + pixel.G + pixel.B) / 3.0f;
                }
            }
            return sum / pixelCount;
        }

        private int Clamp(int value, int min, int max)
        {
            return value < min ? min : value > max ? max : value;
        }
    }
}
