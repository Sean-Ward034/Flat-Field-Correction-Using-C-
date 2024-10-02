namespace FlatFieldCorrectionApp
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.Label lblDarkMode;
        private System.Windows.Forms.CheckBox checkBoxDarkMode;
        private System.Windows.Forms.CheckBox checkBoxEnableGPU;
        private System.Windows.Forms.Label lblGPU;
        private System.Windows.Forms.ComboBox comboBoxGPU;
        private System.Windows.Forms.Button btnSave;

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
            this.lblMode = new System.Windows.Forms.Label();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.lblDarkMode = new System.Windows.Forms.Label();
            this.checkBoxDarkMode = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableGPU = new System.Windows.Forms.CheckBox();
            this.lblGPU = new System.Windows.Forms.Label();
            this.comboBoxGPU = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(20, 20);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(96, 17);
            this.lblMode.TabIndex = 0;
            this.lblMode.Text = "Select Mode:";
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Items.AddRange(new object[] {
            "Color",
            "Infrared",
            "X-Ray"});
            this.comboBoxMode.Location = new System.Drawing.Point(130, 17);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(150, 24);
            this.comboBoxMode.TabIndex = 1;
            // 
            // lblDarkMode
            // 
            this.lblDarkMode.AutoSize = true;
            this.lblDarkMode.Location = new System.Drawing.Point(20, 60);
            this.lblDarkMode.Name = "lblDarkMode";
            this.lblDarkMode.Size = new System.Drawing.Size(84, 17);
            this.lblDarkMode.TabIndex = 2;
            this.lblDarkMode.Text = "Dark Mode:";
            // 
            // checkBoxDarkMode
            // 
            this.checkBoxDarkMode.AutoSize = true;
            this.checkBoxDarkMode.Location = new System.Drawing.Point(130, 60);
            this.checkBoxDarkMode.Name = "checkBoxDarkMode";
            this.checkBoxDarkMode.Size = new System.Drawing.Size(18, 17);
            this.checkBoxDarkMode.TabIndex = 3;
            this.checkBoxDarkMode.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableGPU
            // 
            this.checkBoxEnableGPU.AutoSize = true;
            this.checkBoxEnableGPU.Location = new System.Drawing.Point(130, 130);
            this.checkBoxEnableGPU.Name = "checkBoxEnableGPU";
            this.checkBoxEnableGPU.Size = new System.Drawing.Size(18, 17);
            this.checkBoxEnableGPU.TabIndex = 7;
            this.checkBoxEnableGPU.UseVisualStyleBackColor = true;
            this.checkBoxEnableGPU.CheckedChanged += new System.EventHandler(this.checkBoxEnableGPU_CheckedChanged);
            // 
            // lblGPU
            // 
            this.lblGPU.AutoSize = true;
            this.lblGPU.Location = new System.Drawing.Point(20, 100);
            this.lblGPU.Name = "lblGPU";
            this.lblGPU.Size = new System.Drawing.Size(103, 17);
            this.lblGPU.TabIndex = 4;
            this.lblGPU.Text = "Available GPUs:";
            // 
            // comboBoxGPU
            // 
            this.comboBoxGPU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGPU.FormattingEnabled = true;
            this.comboBoxGPU.Location = new System.Drawing.Point(130, 97);
            this.comboBoxGPU.Name = "comboBoxGPU";
            this.comboBoxGPU.Size = new System.Drawing.Size(250, 24);
            this.comboBoxGPU.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(305, 140);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 190);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.comboBoxMode);
            this.Controls.Add(this.lblDarkMode);
            this.Controls.Add(this.checkBoxDarkMode);
            this.Controls.Add(this.checkBoxEnableGPU);
            this.Controls.Add(this.lblGPU);
            this.Controls.Add(this.comboBoxGPU);
            this.Controls.Add(this.btnSave);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
