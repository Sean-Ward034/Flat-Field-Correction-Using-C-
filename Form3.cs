using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlatFieldCorrectionApp  // Ensure the namespace matches Form3.Designer.cs
{
    public partial class Form3 : Form
    {
        private Bitmap enhancedImage;  // Store the enhanced image

        public Form3(Bitmap image)
        {
            InitializeComponent();
            if (image != null)
            {
                enhancedImage = image;  // Set the enhanced image
                pictureBoxEnhanced.Image = enhancedImage;  // Display the image in the PictureBox
            }
            else
            {
                MessageBox.Show("Error: Image cannot be null!", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveEnhancedImage_Click(object sender, EventArgs e)
        {
            if (enhancedImage == null)
            {
                MessageBox.Show("No image to save!", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                saveFileDialog.Title = "Save the Enhanced Image";
                saveFileDialog.FileName = "enhanced_image.png";  // Default file name

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        enhancedImage.Save(saveFileDialog.FileName);  // Save the image
                        MessageBox.Show("Image saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to save the image: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
