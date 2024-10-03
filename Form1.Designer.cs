﻿namespace FlatFieldCorrectionApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelControls; // Panel for controls
        private System.Windows.Forms.Panel panelObjectImage;
        private System.Windows.Forms.Panel panelCorrectedImage;

        private System.Windows.Forms.PictureBox pictureBoxObject;
        private System.Windows.Forms.PictureBox pictureBoxCorrected;
        private System.Windows.Forms.GroupBox groupBoxAdjustments; // Group box for adjustments

        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnApplyCorrection;
        private System.Windows.Forms.Button btnEditCorrection;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnDiscardChanges;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnOpenForm2;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Button btnAIEnhance;

        private System.Windows.Forms.TrackBar trackBarBrightness;
        private System.Windows.Forms.TrackBar trackBarContrast;
        private System.Windows.Forms.TrackBar trackBarExposure;
        private System.Windows.Forms.TrackBar trackBarSharpening;

        private System.Windows.Forms.Label lblBrightness;
        private System.Windows.Forms.Label lblContrast;
        private System.Windows.Forms.Label lblExposure;
        private System.Windows.Forms.Label lblSharpening;
        private System.Windows.Forms.Label labelStatus;

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
            this.panelObjectImage = new System.Windows.Forms.Panel();
            this.pictureBoxObject = new System.Windows.Forms.PictureBox();
            this.panelCorrectedImage = new System.Windows.Forms.Panel();
            this.pictureBoxCorrected = new System.Windows.Forms.PictureBox();
            this.panelControls = new System.Windows.Forms.Panel();
            this.btnUpload = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.btnApplyCorrection = new System.Windows.Forms.Button();
            this.btnEditCorrection = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnDiscardChanges = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.groupBoxAdjustments = new System.Windows.Forms.GroupBox();
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.trackBarContrast = new System.Windows.Forms.TrackBar();
            this.trackBarExposure = new System.Windows.Forms.TrackBar();
            this.lblBrightness = new System.Windows.Forms.Label();
            this.lblContrast = new System.Windows.Forms.Label();
            this.lblExposure = new System.Windows.Forms.Label();
            this.trackBarSharpening = new System.Windows.Forms.TrackBar();
            this.lblSharpening = new System.Windows.Forms.Label();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnOpenForm2 = new System.Windows.Forms.Button();
            this.btnAIEnhance = new System.Windows.Forms.Button();
            this.panelObjectImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxObject)).BeginInit();
            this.panelCorrectedImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCorrected)).BeginInit();
            this.panelControls.SuspendLayout();
            this.groupBoxAdjustments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarExposure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSharpening)).BeginInit();
            this.SuspendLayout();
            // 
            // panelObjectImage
            // 
            this.panelObjectImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelObjectImage.AutoScroll = true;
            this.panelObjectImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelObjectImage.Controls.Add(this.pictureBoxObject);
            this.panelObjectImage.Location = new System.Drawing.Point(12, 49);
            this.panelObjectImage.Name = "panelObjectImage";
            this.panelObjectImage.Size = new System.Drawing.Size(700, 480);
            this.panelObjectImage.TabIndex = 0;
            // 
            // pictureBoxObject
            // 
            this.pictureBoxObject.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxObject.Name = "pictureBoxObject";
            this.pictureBoxObject.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxObject.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxObject.TabIndex = 0;
            this.pictureBoxObject.TabStop = false;
            // 
            // panelCorrectedImage
            // 
            this.panelCorrectedImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCorrectedImage.AutoScroll = true;
            this.panelCorrectedImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCorrectedImage.Controls.Add(this.pictureBoxCorrected);
            this.panelCorrectedImage.Location = new System.Drawing.Point(720, 50);
            this.panelCorrectedImage.Name = "panelCorrectedImage";
            this.panelCorrectedImage.Size = new System.Drawing.Size(700, 478);
            this.panelCorrectedImage.TabIndex = 1;
            // 
            // pictureBoxCorrected
            // 
            this.pictureBoxCorrected.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxCorrected.Name = "pictureBoxCorrected";
            this.pictureBoxCorrected.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxCorrected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxCorrected.TabIndex = 0;
            this.pictureBoxCorrected.TabStop = false;
            // 
            // panelControls
            // 
            this.panelControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelControls.Controls.Add(this.btnAIEnhance);
            this.panelControls.Controls.Add(this.btnUpload);
            this.panelControls.Controls.Add(this.labelStatus);
            this.panelControls.Controls.Add(this.btnApplyCorrection);
            this.panelControls.Controls.Add(this.btnEditCorrection);
            this.panelControls.Controls.Add(this.btnSaveChanges);
            this.panelControls.Controls.Add(this.btnDiscardChanges);
            this.panelControls.Controls.Add(this.btnSettings);
            this.panelControls.Controls.Add(this.groupBoxAdjustments);
            this.panelControls.Location = new System.Drawing.Point(12, 534);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(1408, 201);
            this.panelControls.TabIndex = 2;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(3, 14);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(160, 40);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "Upload Image";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(12, 57);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(163, 20);
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "Ready";
            // 
            // btnApplyCorrection
            // 
            this.btnApplyCorrection.Location = new System.Drawing.Point(549, 11);
            this.btnApplyCorrection.Name = "btnApplyCorrection";
            this.btnApplyCorrection.Size = new System.Drawing.Size(160, 40);
            this.btnApplyCorrection.TabIndex = 1;
            this.btnApplyCorrection.Text = "Apply Correction";
            this.btnApplyCorrection.UseVisualStyleBackColor = true;
            this.btnApplyCorrection.Click += new System.EventHandler(this.btnApplyCorrection_Click);
            // 
            // btnEditCorrection
            // 
            this.btnEditCorrection.Enabled = false;
            this.btnEditCorrection.Location = new System.Drawing.Point(549, 57);
            this.btnEditCorrection.Name = "btnEditCorrection";
            this.btnEditCorrection.Size = new System.Drawing.Size(160, 40);
            this.btnEditCorrection.TabIndex = 2;
            this.btnEditCorrection.Text = "Edit Correction";
            this.btnEditCorrection.UseVisualStyleBackColor = true;
            this.btnEditCorrection.Click += new System.EventHandler(this.btnEditCorrection_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Enabled = false;
            this.btnSaveChanges.Location = new System.Drawing.Point(549, 103);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(160, 40);
            this.btnSaveChanges.TabIndex = 3;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnDiscardChanges
            // 
            this.btnDiscardChanges.Enabled = false;
            this.btnDiscardChanges.Location = new System.Drawing.Point(549, 149);
            this.btnDiscardChanges.Name = "btnDiscardChanges";
            this.btnDiscardChanges.Size = new System.Drawing.Size(160, 40);
            this.btnDiscardChanges.TabIndex = 4;
            this.btnDiscardChanges.Text = "Discard Changes";
            this.btnDiscardChanges.UseVisualStyleBackColor = true;
            this.btnDiscardChanges.Click += new System.EventHandler(this.btnDiscardChanges_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(3, 156);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(160, 40);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // groupBoxAdjustments
            // 
            this.groupBoxAdjustments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAdjustments.Controls.Add(this.trackBarBrightness);
            this.groupBoxAdjustments.Controls.Add(this.trackBarContrast);
            this.groupBoxAdjustments.Controls.Add(this.trackBarExposure);
            this.groupBoxAdjustments.Controls.Add(this.lblBrightness);
            this.groupBoxAdjustments.Controls.Add(this.lblContrast);
            this.groupBoxAdjustments.Controls.Add(this.lblExposure);
            this.groupBoxAdjustments.Controls.Add(this.trackBarSharpening);
            this.groupBoxAdjustments.Controls.Add(this.lblSharpening);
            this.groupBoxAdjustments.Location = new System.Drawing.Point(708, 14);
            this.groupBoxAdjustments.Name = "groupBoxAdjustments";
            this.groupBoxAdjustments.Size = new System.Drawing.Size(695, 175);
            this.groupBoxAdjustments.TabIndex = 6;
            this.groupBoxAdjustments.TabStop = false;
            this.groupBoxAdjustments.Text = "Adjustments";
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.Enabled = false;
            this.trackBarBrightness.Location = new System.Drawing.Point(90, 120);
            this.trackBarBrightness.Maximum = 100;
            this.trackBarBrightness.Minimum = -100;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(220, 56);
            this.trackBarBrightness.TabIndex = 2;
            this.trackBarBrightness.TickFrequency = 10;
            this.trackBarBrightness.ValueChanged += new System.EventHandler(this.trackBarBrightness_ValueChanged);
            // 
            // trackBarContrast
            // 
            this.trackBarContrast.Enabled = false;
            this.trackBarContrast.Location = new System.Drawing.Point(90, 70);
            this.trackBarContrast.Maximum = 100;
            this.trackBarContrast.Minimum = -100;
            this.trackBarContrast.Name = "trackBarContrast";
            this.trackBarContrast.Size = new System.Drawing.Size(220, 56);
            this.trackBarContrast.TabIndex = 1;
            this.trackBarContrast.TickFrequency = 10;
            this.trackBarContrast.ValueChanged += new System.EventHandler(this.trackBarContrast_ValueChanged);
            // 
            // trackBarExposure
            // 
            this.trackBarExposure.Enabled = false;
            this.trackBarExposure.Location = new System.Drawing.Point(90, 20);
            this.trackBarExposure.Maximum = 100;
            this.trackBarExposure.Minimum = -100;
            this.trackBarExposure.Name = "trackBarExposure";
            this.trackBarExposure.Size = new System.Drawing.Size(220, 56);
            this.trackBarExposure.TabIndex = 3;
            this.trackBarExposure.TickFrequency = 10;
            this.trackBarExposure.ValueChanged += new System.EventHandler(this.trackBarExposure_ValueChanged);
            // 
            // lblBrightness
            // 
            this.lblBrightness.AutoSize = true;
            this.lblBrightness.Location = new System.Drawing.Point(10, 130);
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.Size = new System.Drawing.Size(73, 16);
            this.lblBrightness.TabIndex = 5;
            this.lblBrightness.Text = "Brightness:";
            // 
            // lblContrast
            // 
            this.lblContrast.AutoSize = true;
            this.lblContrast.Location = new System.Drawing.Point(10, 80);
            this.lblContrast.Name = "lblContrast";
            this.lblContrast.Size = new System.Drawing.Size(59, 16);
            this.lblContrast.TabIndex = 4;
            this.lblContrast.Text = "Contrast:";
            // 
            // lblExposure
            // 
            this.lblExposure.AutoSize = true;
            this.lblExposure.Location = new System.Drawing.Point(10, 30);
            this.lblExposure.Name = "lblExposure";
            this.lblExposure.Size = new System.Drawing.Size(67, 16);
            this.lblExposure.TabIndex = 3;
            this.lblExposure.Text = "Exposure:";
            // 
            // trackBarSharpening
            // 
            this.trackBarSharpening.Enabled = false;
            this.trackBarSharpening.Location = new System.Drawing.Point(435, 20);
            this.trackBarSharpening.Maximum = 100;
            this.trackBarSharpening.Minimum = -100;
            this.trackBarSharpening.Name = "trackBarSharpening";
            this.trackBarSharpening.Size = new System.Drawing.Size(220, 56);
            this.trackBarSharpening.TabIndex = 4;
            this.trackBarSharpening.TickFrequency = 10;
            this.trackBarSharpening.ValueChanged += new System.EventHandler(this.sliderSharpening_ValueChanged);
            // 
            // lblSharpening
            // 
            this.lblSharpening.Location = new System.Drawing.Point(347, 26);
            this.lblSharpening.Name = "lblSharpening";
            this.lblSharpening.Size = new System.Drawing.Size(100, 20);
            this.lblSharpening.TabIndex = 6;
            this.lblSharpening.Text = "Sharpening";
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(1270, 14);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(150, 30);
            this.btnSaveImage.TabIndex = 0;
            this.btnSaveImage.Text = "Save Corrected Image";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnOpenForm2
            // 
            this.btnOpenForm2.Location = new System.Drawing.Point(12, 12);
            this.btnOpenForm2.Name = "btnOpenForm2";
            this.btnOpenForm2.Size = new System.Drawing.Size(112, 30);
            this.btnOpenForm2.TabIndex = 3;
            this.btnOpenForm2.Text = "Pre-Processing";
            this.btnOpenForm2.UseVisualStyleBackColor = true;
            this.btnOpenForm2.Click += new System.EventHandler(this.btnOpenForm2_Click);
            // 
            // btnAIEnhance
            // 
            this.btnAIEnhance.Location = new System.Drawing.Point(383, 11);
            this.btnAIEnhance.Name = "btnAIEnhance";
            this.btnAIEnhance.Size = new System.Drawing.Size(160, 41);
            this.btnAIEnhance.TabIndex = 5;
            this.btnAIEnhance.Text = "AI Enhance";
            this.btnAIEnhance.UseVisualStyleBackColor = true;
            this.btnAIEnhance.Click += new System.EventHandler(this.btnAIEnhance_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1432, 747);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.panelObjectImage);
            this.Controls.Add(this.panelCorrectedImage);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.btnOpenForm2);
            this.MinimumSize = new System.Drawing.Size(1450, 780);
            this.Name = "Form1";
            this.Text = "Advanced Flat Field Correction App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelObjectImage.ResumeLayout(false);
            this.panelObjectImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxObject)).EndInit();
            this.panelCorrectedImage.ResumeLayout(false);
            this.panelCorrectedImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCorrected)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.groupBoxAdjustments.ResumeLayout(false);
            this.groupBoxAdjustments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarExposure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSharpening)).EndInit();
            this.ResumeLayout(false);

        }
    }
}