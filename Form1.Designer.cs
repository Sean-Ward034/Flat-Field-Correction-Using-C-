namespace FlatFieldCorrectionApp
{
    partial class Form1
    {
        private System.Windows.Forms.PictureBox pictureBoxObject;
        private System.Windows.Forms.PictureBox pictureBoxCorrected;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnApplyCorrection;
        private System.Windows.Forms.Button btnEditCorrection;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnDiscardChanges;
        private System.Windows.Forms.TrackBar trackBarBrightness;
        private System.Windows.Forms.TrackBar trackBarContrast;
        private System.Windows.Forms.TrackBar trackBarExposure;
        private System.Windows.Forms.Label lblBrightness;
        private System.Windows.Forms.Label lblContrast;
        private System.Windows.Forms.Label lblExposure;

        private void InitializeComponent()
        {
            this.pictureBoxObject = new System.Windows.Forms.PictureBox();
            this.pictureBoxCorrected = new System.Windows.Forms.PictureBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnApplyCorrection = new System.Windows.Forms.Button();
            this.btnEditCorrection = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnDiscardChanges = new System.Windows.Forms.Button();
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.trackBarContrast = new System.Windows.Forms.TrackBar();
            this.trackBarExposure = new System.Windows.Forms.TrackBar();
            this.lblBrightness = new System.Windows.Forms.Label();
            this.lblContrast = new System.Windows.Forms.Label();
            this.lblExposure = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCorrected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarExposure)).BeginInit();

            this.SuspendLayout();

            // PictureBox for the Object Image
            this.pictureBoxObject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxObject.Location = new System.Drawing.Point(30, 30);
            this.pictureBoxObject.Size = new System.Drawing.Size(400, 400);
            this.pictureBoxObject.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // PictureBox for the Corrected Image
            this.pictureBoxCorrected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCorrected.Location = new System.Drawing.Point(500, 30);
            this.pictureBoxCorrected.Size = new System.Drawing.Size(400, 400);
            this.pictureBoxCorrected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // Button for Upload
            this.btnUpload.Location = new System.Drawing.Point(30, 460);
            this.btnUpload.Size = new System.Drawing.Size(150, 40);
            this.btnUpload.Text = "Upload Image";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);

            // Button for Apply Correction
            this.btnApplyCorrection.Location = new System.Drawing.Point(210, 460);
            this.btnApplyCorrection.Size = new System.Drawing.Size(150, 40);
            this.btnApplyCorrection.Text = "Apply Correction";
            this.btnApplyCorrection.Click += new System.EventHandler(this.btnApplyCorrection_Click);

            // Button for Edit Correction
            this.btnEditCorrection.Location = new System.Drawing.Point(390, 460);
            this.btnEditCorrection.Size = new System.Drawing.Size(150, 40);
            this.btnEditCorrection.Text = "Edit Correction";
            this.btnEditCorrection.Enabled = false; // Initially disabled
            this.btnEditCorrection.Click += new System.EventHandler(this.btnEditCorrection_Click);

            // Button for Save Changes
            this.btnSaveChanges.Location = new System.Drawing.Point(570, 460);
            this.btnSaveChanges.Size = new System.Drawing.Size(150, 40);
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.Enabled = false; // Initially disabled
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);

            // Button for Discard Changes
            this.btnDiscardChanges.Location = new System.Drawing.Point(750, 460);
            this.btnDiscardChanges.Size = new System.Drawing.Size(150, 40);
            this.btnDiscardChanges.Text = "Discard Changes";
            this.btnDiscardChanges.Enabled = false; // Initially disabled
            this.btnDiscardChanges.Click += new System.EventHandler(this.btnDiscardChanges_Click);

            // TrackBar for Brightness Adjustment
            this.trackBarBrightness.Location = new System.Drawing.Point(30, 520);
            this.trackBarBrightness.Minimum = -100;
            this.trackBarBrightness.Maximum = 100;
            this.trackBarBrightness.TickFrequency = 10;
            this.trackBarBrightness.Enabled = false;
            this.trackBarBrightness.ValueChanged += new System.EventHandler(this.trackBarBrightness_ValueChanged);

            // Label for Brightness
            this.lblBrightness.Location = new System.Drawing.Point(30, 500);
            this.lblBrightness.Text = "Brightness";

            // TrackBar for Contrast Adjustment
            this.trackBarContrast.Location = new System.Drawing.Point(210, 520);
            this.trackBarContrast.Minimum = -100;
            this.trackBarContrast.Maximum = 100;
            this.trackBarContrast.TickFrequency = 10;
            this.trackBarContrast.Enabled = false;
            this.trackBarContrast.ValueChanged += new System.EventHandler(this.trackBarContrast_ValueChanged);

            // Label for Contrast
            this.lblContrast.Location = new System.Drawing.Point(210, 500);
            this.lblContrast.Text = "Contrast";

            // TrackBar for Exposure Adjustment
            this.trackBarExposure.Location = new System.Drawing.Point(390, 520);
            this.trackBarExposure.Minimum = -100;
            this.trackBarExposure.Maximum = 100;
            this.trackBarExposure.TickFrequency = 10;
            this.trackBarExposure.Enabled = false;
            this.trackBarExposure.ValueChanged += new System.EventHandler(this.trackBarExposure_ValueChanged);

            // Label for Exposure
            this.lblExposure.Location = new System.Drawing.Point(390, 500);
            this.lblExposure.Text = "Exposure";

            // Form Settings
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.pictureBoxObject);
            this.Controls.Add(this.pictureBoxCorrected);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnApplyCorrection);
            this.Controls.Add(this.btnEditCorrection);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnDiscardChanges);
            this.Controls.Add(this.trackBarBrightness);
            this.Controls.Add(this.trackBarContrast);
            this.Controls.Add(this.trackBarExposure);
            this.Controls.Add(this.lblBrightness);
            this.Controls.Add(this.lblContrast);
            this.Controls.Add(this.lblExposure);
            this.Name = "Form1";
            this.Text = "Advanced Flat Field Correction App";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCorrected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarExposure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
