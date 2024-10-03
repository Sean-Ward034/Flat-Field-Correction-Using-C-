using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Azure.Amqp.Framing;
using Google.Api.Ads.AdWords.v201809;
using FlatFieldCorrection;

namespace FlatFieldCorrectionApp
{
    public partial class Form1 : Form
    {
        private Bitmap OriginalImage;
        private Bitmap ProcessedImage;

        private Bitmap objectImage;
        private Bitmap darkFrameImage;
        private Bitmap brightFrameImage;
        private Bitmap correctedImage;  // The original corrected image
        private Bitmap editedImage;     // The image that is being edited

        private float zoomFactorObject = 1.0f;
        private float zoomFactorCorrected = 1.0f;

        // Variables for panning
        private bool isPanningObject = false;
        private Point panStartPointObject = new Point();

        private bool isPanningCorrected = false;
        private Point panStartPointCorrected = new Point();

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load; // Attach Form Load event

            // Enable mouse wheel events for zooming
            this.panelObjectImage.MouseWheel += PanelObjectImage_MouseWheel;
            this.panelCorrectedImage.MouseWheel += PanelCorrectedImage_MouseWheel;

            // Ensure the panels have focus to receive mouse wheel events
            this.panelObjectImage.MouseEnter += (s, e) => panelObjectImage.Focus();
            this.panelCorrectedImage.MouseEnter += (s, e) => panelCorrectedImage.Focus();

            // Handle form resizing for dynamic GUI sizing
            this.Resize += Form1_Resize;

            // Attach mouse events for panning
            pictureBoxObject.MouseDown += PictureBoxObject_MouseDown;
            pictureBoxObject.MouseMove += PictureBoxObject_MouseMove;
            pictureBoxObject.MouseUp += PictureBoxObject_MouseUp;

            pictureBoxCorrected.MouseDown += PictureBoxCorrected_MouseDown;
            pictureBoxCorrected.MouseMove += PictureBoxCorrected_MouseMove;
            pictureBoxCorrected.MouseUp += PictureBoxCorrected_MouseUp;

            btnOpenForm2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyTheme(); // Apply the theme on form load
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Adjust panel sizes when the form is resized
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            int padding = 24; // Adjust as needed
            int halfWidth = (this.ClientSize.Width - padding) / 2;
            int panelHeight = this.ClientSize.Height - panelControls.Height - padding;

            panelObjectImage.Location = new Point(12, 30);
            panelObjectImage.Size = new Size(halfWidth, panelHeight);

            panelCorrectedImage.Location = new Point(panelObjectImage.Right + 12, 30);
            panelCorrectedImage.Size = new Size(halfWidth, panelHeight);
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

                    // Assign OriginalImage properly for use in Form2
                    OriginalImage = (Bitmap)objectImage.Clone();

                    correctedImage = null;  // Clear previous corrected image
                    pictureBoxCorrected.Image = null;  // Clear previous display

                    // Reset zoom factor and apply zoom
                    zoomFactorObject = 1.0f;
                    ApplyZoom(pictureBoxObject, zoomFactorObject);

                    GenerateCalibrationImages(objectImage);

                    btnOpenForm2.Enabled = true;  // Enable Pre-Processing button after upload

                    MessageBox.Show("Image Uploaded and Smoothed Successfully");
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

            // Reset zoom factor and apply zoom
            zoomFactorCorrected = 1.0f;
            ApplyZoom(pictureBoxCorrected, zoomFactorCorrected);

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

        private void btnOpenForm2_Click(object sender, EventArgs e)
        {
            if (OriginalImage != null)
            {
                Form2 form2 = new Form2(OriginalImage);
                form2.FormClosed += (s, args) => {
                    if (form2.ProcessedImage != null)
                    {
                        correctedImage = form2.ProcessedImage;  // Store the corrected image
                        pictureBoxCorrected.Image = correctedImage;  // Update the corrected image display
                         MessageBox.Show("Enhancements applied successfully!");
                    }
                };
                form2.ShowDialog();  // Show Form2 as a modal dialog
            }
        }

        // Event handler for the new Save button
        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            Bitmap imageToSave = correctedImage;

            if (imageToSave != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png";
                    saveFileDialog.Title = "Save Image As";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            imageToSave.Save(saveFileDialog.FileName);
                            MessageBox.Show($"Image saved successfully to {saveFileDialog.FileName}!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error saving image: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void sliderSharpening_ValueChanged(object sender, EventArgs e)
        {
            if (correctedImage != null)
            {
                int sharpenStrength = trackBarSharpening.Value;
                pictureBoxCorrected.Image = ApplySharpening(correctedImage, sharpenStrength);
            }
        }

        private Bitmap ApplySharpening(Bitmap image, int strength)
        {
            float sharpenAmount = strength / 100.0f;
            Bitmap blurredImage = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(blurredImage))
            {
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(1, 1, image.Width - 2, image.Height - 2), GraphicsUnit.Pixel);
            }

            Bitmap sharpenedImage = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color original = image.GetPixel(x, y);
                    Color blurred = blurredImage.GetPixel(x, y);

                    int r = ClampSharp(original.R + (int)((original.R - blurred.R) * sharpenAmount));
                    int g = ClampSharp(original.G + (int)((original.G - blurred.G) * sharpenAmount));
                    int b = ClampSharp(original.B + (int)((original.B - blurred.B) * sharpenAmount));

                    sharpenedImage.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return sharpenedImage;
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

        private void btnAIEnhance_Click(object sender, EventArgs e)
        {
            try
            {
                if (correctedImage == null)
                {
                    MessageBox.Show("Please apply flat-field correction before AI enhancement.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Step 1: Open a file dialog for the user to choose the ONNX model file
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Select AI Model for Enhancement";
                    openFileDialog.Filter = "ONNX Files (*.onnx)|*.onnx";
                    openFileDialog.InitialDirectory = Application.StartupPath + @"\Models";  // Set default path if applicable

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Step 2: Display message indicating the start of AI Enhancement
                        MessageBox.Show("Starting AI Enhancement. This may take a few moments.", "AI Enhancement", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Step 3: Use the selected model path for AI processing
                        string modelPath = openFileDialog.FileName;

                        // Call the Real-ESRGAN processor to enhance the corrected image using the selected model
                        var upscaledImage = RealESRGANProcessor.UpscaleImage(correctedImage, modelPath);

                        // Step 4: Open the new Form3 to display the enhanced image
                        Form3 enhancedForm = new Form3(upscaledImage);
                        enhancedForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Model selection was cancelled. AI Enhancement not started.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"AI Enhancement failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            return AdjustBrightness(objectImage, 2.5f);
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
            string selectedMode = FlatFieldCorrection.Properties.Settings.Default.SelectedMode;
            bool enableGPU = FlatFieldCorrection.Properties.Settings.Default.EnableGPU;
            string selectedGPU = FlatFieldCorrection.Properties.Settings.Default.SelectedGPU;

            if (enableGPU && !string.IsNullOrEmpty(selectedGPU))
            {
                // Attempt to use GPU acceleration
                try
                {
                    // Implement your GPU-accelerated flat field correction here
                    // Placeholder: Show a message indicating GPU usage
                    //MessageBox.Show("GPU acceleration is enabled, but GPU processing is not implemented yet.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during GPU processing: " + ex.Message);
                    // Fallback to CPU processing
                    return ApplyFlatFieldCorrectionCPU(rawImage, darkFrame, brightFrame, selectedMode);
                }
            }

            // Use CPU processing
            return ApplyFlatFieldCorrectionCPU(rawImage, darkFrame, brightFrame, selectedMode);
        }

        private Bitmap ApplyFlatFieldCorrectionCPU(Bitmap rawImage, Bitmap darkFrame, Bitmap brightFrame, string selectedMode)
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

                    int correctedR = 0, correctedG = 0, correctedB = 0;

                    switch (selectedMode)
                    {
                        case "Color":
                            // Handle color images
                            correctedR = (int)(((rawPixel.R - darkPixel.R) / gain) * 255);
                            correctedG = (int)(((rawPixel.G - darkPixel.G) / gain) * 255);
                            correctedB = (int)(((rawPixel.B - darkPixel.B) / gain) * 255);
                            break;

                        case "Greyscale":

                        default:
                            // Process as grayscale
                            int rawIntensity = (rawPixel.R + rawPixel.G + rawPixel.B) / 3;
                            int darkIntensity = (darkPixel.R + darkPixel.G + darkPixel.B) / 3;
                            int correctedIntensity = (int)(((rawIntensity - darkIntensity) / gain) * 255);
                            correctedR = correctedG = correctedB = correctedIntensity;
                            break;
                    }

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

        private int ClampSharp(int value, int min = 0, int max = 255)
        {
            return value < min ? min : (value > max ? max : value);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        public void ApplyTheme()
        {
            bool darkMode = FlatFieldCorrection.Properties.Settings.Default.DarkMode;
            Color backColor = darkMode ? Color.FromArgb(30, 30, 30) : SystemColors.Control;
            Color foreColor = darkMode ? Color.White : SystemColors.ControlText;

            this.BackColor = backColor;
            this.ForeColor = foreColor;

            foreach (Control control in this.Controls)
            {
                ApplyThemeToControl(control, backColor, foreColor);
            }
        }

        private void ApplyThemeToControl(Control control, Color backColor, Color foreColor)
        {
            control.BackColor = backColor;
            control.ForeColor = foreColor;

            // Apply recursively to child controls
            foreach (Control child in control.Controls)
            {
                ApplyThemeToControl(child, backColor, foreColor);
            }
        }

        // Zooming functionality
        private void PanelObjectImage_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                // Zoom in or out
                if (e.Delta > 0)
                {
                    zoomFactorObject *= 1.1f;
                }
                else if (e.Delta < 0)
                {
                    zoomFactorObject /= 1.1f;
                }
                ApplyZoom(pictureBoxObject, zoomFactorObject);
            }
            else
            {
                // Scroll the panel vertically
                panelObjectImage.VerticalScroll.Value = Math.Max(0, Math.Min(panelObjectImage.VerticalScroll.Maximum, panelObjectImage.VerticalScroll.Value - e.Delta));
                panelObjectImage.PerformLayout();
            }
        }

        private void PanelCorrectedImage_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                // Zoom in or out
                if (e.Delta > 0)
                {
                    zoomFactorCorrected *= 1.1f;
                }
                else if (e.Delta < 0)
                {
                    zoomFactorCorrected /= 1.1f;
                }
                ApplyZoom(pictureBoxCorrected, zoomFactorCorrected);
            }
            else
            {
                // Scroll the panel vertically
                panelCorrectedImage.VerticalScroll.Value = Math.Max(0, Math.Min(panelCorrectedImage.VerticalScroll.Maximum, panelCorrectedImage.VerticalScroll.Value - e.Delta));
                panelCorrectedImage.PerformLayout();
            }
        }

        private void ApplyZoom(PictureBox pictureBox, float zoomFactor)
        {
            if (pictureBox.Image == null) return;

            pictureBox.SuspendLayout();

            pictureBox.Width = (int)(pictureBox.Image.Width * zoomFactor);
            pictureBox.Height = (int)(pictureBox.Image.Height * zoomFactor);

            pictureBox.ResumeLayout();
        }

        // Panning functionality
        private void PictureBoxObject_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPanningObject = true;
                panStartPointObject = e.Location;
                Cursor = Cursors.Hand;
            }
        }

        private void PictureBoxObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPanningObject)
            {
                Point delta = new Point(e.Location.X - panStartPointObject.X, e.Location.Y - panStartPointObject.Y);
                panelObjectImage.AutoScrollPosition = new Point(-panelObjectImage.AutoScrollPosition.X - delta.X, -panelObjectImage.AutoScrollPosition.Y - delta.Y);
            }
        }

        private void PictureBoxObject_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPanningObject = false;
                Cursor = Cursors.Default;
            }
        }

        private void PictureBoxCorrected_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPanningCorrected = true;
                panStartPointCorrected = e.Location;
                Cursor = Cursors.Hand;
            }
        }

        private void PictureBoxCorrected_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPanningCorrected)
            {
                Point delta = new Point(e.Location.X - panStartPointCorrected.X, e.Location.Y - panStartPointCorrected.Y);
                panelCorrectedImage.AutoScrollPosition = new Point(-panelCorrectedImage.AutoScrollPosition.X - delta.X, -panelCorrectedImage.AutoScrollPosition.Y - delta.Y);
            }
        }

        private void PictureBoxCorrected_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPanningCorrected = false;
                Cursor = Cursors.Default;
            }
        }
    }
}
