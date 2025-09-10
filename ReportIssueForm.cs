using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Municipal_Service_Application
{
    public partial class ReportIssueForm : Form
    {
        // Public properties to return data
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
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Font = new Font("Segoe UI", 9);

            // Title
            var titleLabel = new Label
            {
                Text = "Report a Municipal Issue",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(460, 30),
                Location = new Point(30, 10),
                ForeColor = Color.FromArgb(30, 80, 130),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Location
            var lblLocation = new Label { Text = "Location:", Location = new Point(30, 50), AutoSize = true };
            txtLocation = new TextBox { Location = new Point(120, 48), Size = new Size(360, 25) };

            // Category
            var lblCategory = new Label { Text = "Category:", Location = new Point(30, 90), AutoSize = true };
            cmbCategory = new ComboBox { Location = new Point(120, 88), Size = new Size(250, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCategory.Items.AddRange(new string[] {
                "Road Maintenance", "Water & Sewage", "Electricity",
                "Waste Management", "Parks & Recreation", "Street Lighting",
                "Traffic Signals", "Other"
            });

            // Description
            var lblDescription = new Label { Text = "Description:", Location = new Point(30, 130), AutoSize = true };
            txtDescription = new TextBox
            {
                Location = new Point(30, 155),
                Size = new Size(450, 120),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // Files
            var lblFiles = new Label { Text = "Attachments:", Location = new Point(30, 285), AutoSize = true };
            var btnAddFile = new Button
            {
                Text = "Add File/Image",
                Location = new Point(120, 280),
                Size = new Size(120, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White
            };
            btnAddFile.FlatAppearance.BorderSize = 0;
            btnAddFile.Click += BtnAddFile_Click;

            lstAttachedFiles = new ListBox { Location = new Point(30, 320), Size = new Size(450, 80) };

            // Progress bar
            var lblProgress = new Label { Text = "Report Progress:", Location = new Point(30, 410), AutoSize = true };
            progressBar = new ProgressBar { Location = new Point(30, 435), Size = new Size(450, 20), Value = 0 };

            // Buttons
            var btnSubmit = new Button
            {
                Text = "✔ Submit Report",
                Location = new Point(300, 470),
                Size = new Size(120, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(34, 139, 34),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += BtnSubmit_Click;

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(430, 470),
                Size = new Size(60, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(200, 30, 30),
                ForeColor = Color.White
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            // Add controls
            this.Controls.AddRange(new Control[] {
                titleLabel, lblLocation, txtLocation, lblCategory, cmbCategory,
                lblDescription, txtDescription, lblFiles, btnAddFile, lstAttachedFiles,
                lblProgress, progressBar, btnSubmit, btnCancel
            });

            // Progress attachments
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
                        MessageBox.Show("Great — adding attachments helps us resolve issues faster.", "Thanks!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            MessageBox.Show("Thank you — your report has been received.", "Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
