using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Municipal_Service_Application
{
    public partial class ReportIssueForm : Form
    {
        public string IssueLocation { get; private set; }
        public string IssueCategory { get; private set; }
        public string IssueDescription { get; private set; }
        public FileLinkedList AttachedFiles { get; private set; } // custom list

        // Controls
        private TextBox txtLocation;
        private ComboBox cmbCategory;
        private TextBox txtDescription;
        private ListBox lstAttachedFiles;
        private ProgressBar progressBar;

        public ReportIssueForm()
        {
            InitializeComponent();
            AttachedFiles = new FileLinkedList();
            SetupReportForm();
        }

        private void SetupReportForm()
        {
            // Form basics
            this.Size = new Size(520, 620);
            this.Text = "Report an Issue";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White; // clean, modern base
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Font = new Font("Segoe UI", 9);

            // Title
            var titleLabel = new Label
            {
                Text = "Report a Municipal Issue",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(400, 40),
                Location = new Point(30, 10),
                ForeColor = Color.FromArgb(0, 122, 51), // SA green
                TextAlign = ContentAlignment.MiddleLeft
            };

            
            var logo = new PictureBox
            {
                Image = Properties.Resources.coat_of_arms,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(430, 10),
                Size = new Size(60, 40)
            };

            // Location
            var lblLocation = new Label { Text = "Location:", Location = new Point(30, 60), AutoSize = true };
            txtLocation = new TextBox { Location = new Point(120, 58), Size = new Size(360, 25) };

            // Category
            var lblCategory = new Label { Text = "Category:", Location = new Point(30, 100), AutoSize = true };
            cmbCategory = new ComboBox
            {
                Location = new Point(120, 98),
                Size = new Size(250, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategory.Items.AddRange(new string[] {
                "Road Maintenance", "Water & Sewage", "Electricity",
                "Waste Management", "Parks & Recreation", "Street LightS",
                "Robot Outages", "Other"
            });

            // Description
            var lblDescription = new Label { Text = "Description:", Location = new Point(30, 140), AutoSize = true };
            txtDescription = new TextBox
            {
                Location = new Point(30, 165),
                Size = new Size(450, 120),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // Files
            var lblFiles = new Label { Text = "Attachments:", Location = new Point(30, 295), AutoSize = true };
            var btnAddFile = new Button
            {
                Text = "Add File/Image",
                Location = new Point(120, 290),
                Size = new Size(140, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Goldenrod, //  gold
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnAddFile.FlatAppearance.BorderSize = 0;
            btnAddFile.Click += BtnAddFile_Click;

            lstAttachedFiles = new ListBox { Location = new Point(30, 330), Size = new Size(450, 80) };

            // Progress bar
            var lblProgress = new Label { Text = "Report Progress:", Location = new Point(30, 420), AutoSize = true };
            progressBar = new ProgressBar
            {
                Location = new Point(30, 445),
                Size = new Size(450, 20),
                Value = 0,
                ForeColor = Color.FromArgb(0, 122, 51) // green
            };

            // Buttons
            var btnSubmit = new Button
            {
                Text = "Submit Report",
                Location = new Point(280, 490),
                Size = new Size(140, 38),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 122, 51), // green
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += BtnSubmit_Click;

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(430, 490),
                Size = new Size(70, 38),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Firebrick,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            // Add controls
            this.Controls.AddRange(new Control[] {
                titleLabel, logo, lblLocation, txtLocation, lblCategory, cmbCategory,
                lblDescription, txtDescription, lblFiles, btnAddFile, lstAttachedFiles,
                lblProgress, progressBar, btnSubmit, btnCancel
            });

            // Progress events
            txtLocation.TextChanged += UpdateProgress;
            cmbCategory.SelectedIndexChanged += UpdateProgress;
            txtDescription.TextChanged += UpdateProgress;
        }

        private void UpdateProgress(object sender, EventArgs e)
        {
            int progress = 0;
            if (!string.IsNullOrWhiteSpace(txtLocation.Text)) progress += 25;
            if (cmbCategory.SelectedIndex >= 0) progress += 25;
            if (!string.IsNullOrWhiteSpace(txtDescription.Text)) progress += 25;
            if (lstAttachedFiles.Items.Count > 0) progress += 25;
            progressBar.Value = progress;
        }

        private void BtnAddFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Images & Docs|*.jpg;*.jpeg;*.png;*.bmp;*.pdf;*.doc;*.docx;*.txt|All files|*.*";
                ofd.Title = "Select attachment";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    AttachedFiles.Add(ofd.FileName);
                    lstAttachedFiles.Items.Add(Path.GetFileName(ofd.FileName));
                    UpdateProgress(sender, e);

                    if (AttachedFiles.Count == 1)
                        MessageBox.Show("Great — adding attachments helps us resolve issues faster.",
                            "Thanks!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter the location.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please provide a description.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IssueLocation = txtLocation.Text.Trim();
            IssueCategory = cmbCategory.SelectedItem.ToString();
            IssueDescription = txtDescription.Text.Trim();

            // Close with OK so the caller picks up properties and AttachedFiles
            MessageBox.Show("Thank you — your report has been received.",
                "Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

//__________________________________________________________END OF FILE________________________________________________________________\\