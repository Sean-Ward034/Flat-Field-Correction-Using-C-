namespace FlatFieldCorrectionApp
{
    partial class Form3
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxEnhanced;
        private System.Windows.Forms.Button btnSaveEnhancedImage;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxEnhanced = new System.Windows.Forms.PictureBox();
            this.btnSaveEnhancedImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnhanced)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxEnhanced
            // 
            this.pictureBoxEnhanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxEnhanced.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxEnhanced.Name = "pictureBoxEnhanced";
            this.pictureBoxEnhanced.Size = new System.Drawing.Size(800, 550);
            this.pictureBoxEnhanced.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxEnhanced.TabIndex = 0;
            this.pictureBoxEnhanced.TabStop = false;
            // 
            // btnSaveEnhancedImage
            // 
            this.btnSaveEnhancedImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSaveEnhancedImage.Location = new System.Drawing.Point(0, 550);
            this.btnSaveEnhancedImage.Name = "btnSaveEnhancedImage";
            this.btnSaveEnhancedImage.Size = new System.Drawing.Size(800, 50);
            this.btnSaveEnhancedImage.TabIndex = 1;
            this.btnSaveEnhancedImage.Text = "Save Enhanced Image";
            this.btnSaveEnhancedImage.UseVisualStyleBackColor = true;
            this.btnSaveEnhancedImage.Click += new System.EventHandler(this.btnSaveEnhancedImage_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pictureBoxEnhanced);
            this.Controls.Add(this.btnSaveEnhancedImage);
            this.Name = "Form3";
            this.Text = "Enhanced Image";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnhanced)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
