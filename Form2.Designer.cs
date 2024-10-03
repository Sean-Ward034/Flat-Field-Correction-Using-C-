namespace FlatFieldCorrectionApp
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Button btnDenoise;
        private System.Windows.Forms.Button btnSaveEnhancements;
        private System.Windows.Forms.Button btnApplyExposureFusion;
        private System.Windows.Forms.Button btnIncreaseResolution;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnApplyExposureFusion = new System.Windows.Forms.Button();
            this.btnIncreaseResolution = new System.Windows.Forms.Button();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.btnDenoise = new System.Windows.Forms.Button();
            this.btnSaveEnhancements = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApplyExposureFusion
            // 
            this.btnApplyExposureFusion.Location = new System.Drawing.Point(30, 30);
            this.btnApplyExposureFusion.Name = "btnApplyExposureFusion";
            this.btnApplyExposureFusion.Size = new System.Drawing.Size(150, 30);
            this.btnApplyExposureFusion.TabIndex = 3;
            this.btnApplyExposureFusion.Text = "Apply Exposure Fusion";
            this.btnApplyExposureFusion.Click += new System.EventHandler(this.btnApplyExposureFusion_Click);
            // 
            // btnIncreaseResolution
            // 
            this.btnIncreaseResolution.Location = new System.Drawing.Point(30, 80);
            this.btnIncreaseResolution.Name = "btnIncreaseResolution";
            this.btnIncreaseResolution.Size = new System.Drawing.Size(150, 30);
            this.btnIncreaseResolution.TabIndex = 4;
            this.btnIncreaseResolution.Text = "Increase Resolution";
            this.btnIncreaseResolution.Click += new System.EventHandler(this.btnIncreaseResolution_Click);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPreview.Location = new System.Drawing.Point(200, 30);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(544, 449);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 0;
            this.pictureBoxPreview.TabStop = false;
            // 
            // btnDenoise
            // 
            this.btnDenoise.Location = new System.Drawing.Point(30, 135);
            this.btnDenoise.Name = "btnDenoise";
            this.btnDenoise.Size = new System.Drawing.Size(150, 30);
            this.btnDenoise.TabIndex = 1;
            this.btnDenoise.Text = "Apply De-noising";
            this.btnDenoise.Click += new System.EventHandler(this.btnDenoise_Click);
            // 
            // btnSaveEnhancements
            // 
            this.btnSaveEnhancements.Location = new System.Drawing.Point(594, 485);
            this.btnSaveEnhancements.Name = "btnSaveEnhancements";
            this.btnSaveEnhancements.Size = new System.Drawing.Size(150, 30);
            this.btnSaveEnhancements.TabIndex = 2;
            this.btnSaveEnhancements.Text = "Save Enhancements";
            this.btnSaveEnhancements.Click += new System.EventHandler(this.btnSaveEnhancements_Click);
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(774, 652);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.btnDenoise);
            this.Controls.Add(this.btnSaveEnhancements);
            this.Controls.Add(this.btnApplyExposureFusion);
            this.Controls.Add(this.btnIncreaseResolution);
            this.Name = "Form2";
            this.Text = "Pre-Processing Features";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
