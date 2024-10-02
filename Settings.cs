using System;
using System.Windows.Forms;
using System.Management;
using Microsoft.Azure.Amqp.Framing;

namespace FlatFieldCorrectionApp
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // Load saved settings
            comboBoxMode.SelectedItem = FlatFieldCorrection.Properties.Settings.Default.SelectedMode;
            checkBoxDarkMode.Checked = FlatFieldCorrection.Properties.Settings.Default.DarkMode;
            checkBoxEnableGPU.Checked = FlatFieldCorrection.Properties.Settings.Default.EnableGPU;

            // Enable or disable GPU selection based on the checkbox
            comboBoxGPU.Enabled = checkBoxEnableGPU.Checked;

            // Populate GPUs
            PopulateGPUs();

            // Select the saved GPU
            string selectedGPU = FlatFieldCorrection.Properties.Settings.Default.SelectedGPU;
            if (!string.IsNullOrEmpty(selectedGPU))
            {
                int index = comboBoxGPU.Items.IndexOf(selectedGPU);
                if (index >= 0)
                {
                    comboBoxGPU.SelectedIndex = index;
                }
                else if (comboBoxGPU.Items.Count > 0)
                {
                    comboBoxGPU.SelectedIndex = 0;
                }
            }
            else if (comboBoxGPU.Items.Count > 0)
            {
                comboBoxGPU.SelectedIndex = 0;
            }
        }

        private void PopulateGPUs()
        {
            try
            {
                comboBoxGPU.Items.Clear();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_VideoController");
                foreach (ManagementObject mo in searcher.Get())
                {
                    comboBoxGPU.Items.Add(mo["Name"].ToString());
                }

                if (comboBoxGPU.Items.Count == 0)
                {
                    // No GPUs found
                    checkBoxEnableGPU.Enabled = false;
                    comboBoxGPU.Enabled = false;
                    MessageBox.Show("No GPUs detected on this system.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving GPUs: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save settings
            FlatFieldCorrection.Properties.Settings.Default.SelectedMode = comboBoxMode.SelectedItem.ToString();
            FlatFieldCorrection.Properties.Settings.Default.DarkMode = checkBoxDarkMode.Checked;
            FlatFieldCorrection.Properties.Settings.Default.EnableGPU = checkBoxEnableGPU.Checked;

            if (comboBoxGPU.SelectedItem != null)
            {
                FlatFieldCorrection.Properties.Settings.Default.SelectedGPU = comboBoxGPU.SelectedItem.ToString();
            }

            FlatFieldCorrection.Properties.Settings.Default.Save();

            // Apply theme changes
            ApplyTheme();

            this.Close();
        }

        private void checkBoxEnableGPU_CheckedChanged(object sender, EventArgs e)
        {
            // Enable or disable the GPU combo box based on the checkbox state
            comboBoxGPU.Enabled = checkBoxEnableGPU.Checked;
        }

        private void ApplyTheme()
        {
            // Get the main form
            Form1 mainForm = (Form1)Application.OpenForms["Form1"];
            if (mainForm != null)
            {
                mainForm.ApplyTheme();
            }
        }
    }
}
