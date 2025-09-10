using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Municipal_Service_Application
{
    public partial class MainForm : Form
    {
        private List<Issue> reportedIssues;
        private int nextIssueId = 1;

        public MainForm()
        {
            InitializeComponent();
            reportedIssues = new List<Issue>();
            SetupMainMenu();
        }

        private void SetupMainMenu()
        {
            // Configure the main form
            this.Size = new Size(600, 400);
            this.Text = "Municipal Service Application";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightBlue;

            // Main Menu Panel
            Panel mainMenuPanel = new Panel
            {
                Size = new Size(500, 300),
                Location = new Point(50, 50),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Title Label
            Label titleLabel = new Label
            {
                Text = "Municipal Service Center",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Size = new Size(400, 40),
                Location = new Point(50, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DarkBlue
            };

            // Report Issues Button
            Button btnReportIssues = new Button
            {
                Text = "Report Issues",
                Size = new Size(200, 50),
                Location = new Point(150, 80),
                BackColor = Color.Green,
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            btnReportIssues.Click += BtnReportIssues_Click;

            // Local Events Button (Disabled for now)
            Button btnLocalEvents = new Button
            {
                Text = "Local Events and Announcements",
                Size = new Size(200, 50),
                Location = new Point(150, 140),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Font = new Font("Arial", 10),
                Enabled = false
            };

            // Service Request Status Button (Disabled for now)
            Button btnServiceStatus = new Button
            {
                Text = "Service Request Status",
                Size = new Size(200, 50),
                Location = new Point(150, 200),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Font = new Font("Arial", 10),
                Enabled = false
            };

            // Add controls to panels and form
            mainMenuPanel.Controls.Add(titleLabel);
            mainMenuPanel.Controls.Add(btnReportIssues);
            mainMenuPanel.Controls.Add(btnLocalEvents);
            mainMenuPanel.Controls.Add(btnServiceStatus);

            this.Controls.Add(mainMenuPanel);
        }

        private void BtnReportIssues_Click(object sender, EventArgs e)
        {
            ReportIssueForm reportForm = new ReportIssueForm();
            if (reportForm.ShowDialog() == DialogResult.OK)
            {
                // Create new issue from the form data
                Issue newIssue = new Issue
                {
                    Id = nextIssueId++,
                    Location = reportForm.IssueLocation,
                    Category = reportForm.IssueCategory,
                    Description = reportForm.IssueDescription,
                    AttachedFiles = reportForm.AttachedFiles
                };

                reportedIssues.Add(newIssue);

                MessageBox.Show($"Issue reported successfully!\nIssue ID: {newIssue.Id}\nThank you for your report.",
                    "Report Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}