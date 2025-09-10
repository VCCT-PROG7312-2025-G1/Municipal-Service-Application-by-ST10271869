using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Municipal_Service_Application
{
    public partial class ReportIssueForm : Form
    {
        // Public properties to return data
        public string IssueLocation { get; private set; }
        public string IssueCategory { get; private set; }
        public string IssueDescription { get; private set; }
        public List<string> AttachedFiles { get; private set; }

        // Private controls
        private TextBox txtLocation;
        private ComboBox cmbCategory;
        private TextBox txtDescription;
        private ListBox lstAttachedFiles;
        private ProgressBar progressBar;

        public ReportIssueForm()
        {
            InitializeComponent();
            AttachedFiles = new List<string>();
            SetupReportForm();
        }

        private void SetupReportForm()
        {
            // Form configuration
            this.Size = new Size(500, 600);
            this.Text = "Report an Issue";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.LightGray;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title
            Label titleLabel = new Label
            {
                Text = "Report a Municipal Issue",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Size = new Size(400, 30),
                Location = new Point(50, 20),
                ForeColor = Color.DarkBlue
            };

            // Location controls
            Label lblLocation = new Label
            {
                Text = "Location:",
                Size = new Size(100, 20),
                Location = new Point(30, 70)
            };

            txtLocation = new TextBox
            {
                Size = new Size(300, 25),
                Location = new Point(130, 68)
            };

            // Category controls
            Label lblCategory = new Label
            {
                Text = "Category:",
                Size = new Size(100, 20),
                Location = new Point(30, 110)
            };

            cmbCategory = new ComboBox
            {
                Size = new Size(200, 25),
                Location = new Point(130, 108),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategory.Items.AddRange(new string[] {
                "Road Maintenance",
                "Water & Sewage",
                "Electricity",
                "Waste Management",
                "Parks & Recreation",
                "Street Lighting",
                "Traffic Signals",
                "Other"
            });

            // Description controls
            Label lblDescription = new Label
            {
                Text = "Description:",
                Size = new Size(100, 20),
                Location = new Point(30, 150)
            };

            txtDescription = new TextBox
            {
                Size = new Size(400, 120),
                Location = new Point(30, 175),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // File attachment controls
            Label lblFiles = new Label
            {
                Text = "Attachments:",
                Size = new Size(100, 20),
                Location = new Point(30, 310)
            };

            Button btnAddFile = new Button
            {
                Text = "Add File/Image",
                Size = new Size(120, 30),
                Location = new Point(130, 308),
                BackColor = Color.LightBlue
            };
            btnAddFile.Click += BtnAddFile_Click;

            lstAttachedFiles = new ListBox
            {
                Size = new Size(400, 80),
                Location = new Point(30, 345)
            };

            // Progress bar for user engagement
            Label lblProgress = new Label
            {
                Text = "Report Progress:",
                Size = new Size(120, 20),
                Location = new Point(30, 440)
            };

            progressBar = new ProgressBar
            {
                Size = new Size(400, 20),
                Location = new Point(30, 465),
                Style = ProgressBarStyle.Continuous,
                Value = 0
            };

            // Action buttons
            Button btnSubmit = new Button
            {
                Text = "Submit Report",
                Size = new Size(100, 35),
                Location = new Point(250, 500),
                BackColor = Color.Green,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnSubmit.Click += BtnSubmit_Click;

            Button btnCancel = new Button
            {
                Text = "Cancel",
                Size = new Size(100, 35),
                Location = new Point(360, 500),
                BackColor = Color.Red,
                ForeColor = Color.White
            };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            // Add all controls to the form
            this.Controls.AddRange(new Control[] {
                titleLabel, lblLocation, txtLocation, lblCategory, cmbCategory,
                lblDescription, txtDescription, lblFiles, btnAddFile, lstAttachedFiles,
                lblProgress, progressBar, btnSubmit, btnCancel
            });

            // Set up event handlers for progress tracking
            txtLocation.TextChanged += UpdateProgress;
            cmbCategory.SelectedIndexChanged += UpdateProgress;
            txtDescription.TextChanged += UpdateProgress;
        }

        private void UpdateProgress(object sender, EventArgs e)
        {
            int progress = 0;

            if (!string.IsNullOrEmpty(txtLocation.Text)) progress += 25;
            if (cmbCategory.SelectedIndex >= 0) progress += 25;
            if (!string.IsNullOrEmpty(txtDescription.Text)) progress += 25;
            if (lstAttachedFiles.Items.Count > 0) progress += 25;

            progressBar.Value = progress;
        }

        private void BtnAddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Document files (*.pdf, *.txt, *.doc, *.docx)|*.pdf;*.txt;*.doc;*.docx|All files (*.*)|*.*",
                Title = "Select file to attach"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.GetFileName(openFileDialog.FileName);
                AttachedFiles.Add(openFileDialog.FileName);
                lstAttachedFiles.Items.Add(fileName);
                UpdateProgress(sender, e);

                // User engagement message
                if (AttachedFiles.Count == 1)
                {
                    MessageBox.Show("Great! Adding images or documents helps us resolve issues faster.",
                        "Thank you!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter the location of the issue.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a category for the issue.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please provide a description of the issue.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Set properties to return to calling form
            IssueLocation = txtLocation.Text.Trim();
            IssueCategory = cmbCategory.SelectedItem.ToString();
            IssueDescription = txtDescription.Text.Trim();

            // Success message
            MessageBox.Show("Thank you for taking the time to report this issue! Your civic participation makes our community better.",
                "Submission Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
