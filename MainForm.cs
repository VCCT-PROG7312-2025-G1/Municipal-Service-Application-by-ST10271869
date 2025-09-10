using System;
using System.Drawing;
using System.Windows.Forms;

namespace Municipal_Service_Application
{
    public partial class MainForm : Form
    {
        private IssueLinkedList reportedIssues;
        private int nextIssueId = 1;

        private DataGridView dgvIssues;
        private Panel contentPanel;

        public MainForm()
        {
            InitializeComponent();
            reportedIssues = new IssueLinkedList();
            nextIssueId = 1;
            SetupMainMenu();
        }

        private void SetupMainMenu()
        {
            // Form
            this.Size = new Size(900, 600);
            this.Text = "Municipal Service Application - RSA Municipal Portal";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9);
            this.BackColor = Color.White;

            // Header 
            // Header panel using TableLayoutPanel
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90, // tall enough for 2-line title
                BackColor = Color.FromArgb(0, 120, 71)
            };

            // TableLayoutPanel to organize label and logo
            var headerTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            headerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // label fills available space
            headerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F)); // fixed logo width
            headerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Title label
            var lblTitle = new Label
            {
                Text = "Municipal Portal\nSouth African Services",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                AutoSize = false,
                AutoEllipsis = true
            };

            // Logo picture
            var logo = new PictureBox
            {
                Image = Properties.Resources.coat_of_arms,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill
            };

            // Add controls to TableLayoutPanel
            headerTable.Controls.Add(lblTitle, 0, 0);
            headerTable.Controls.Add(logo, 1, 0);

            // Add TableLayoutPanel to header panel
            header.Controls.Add(headerTable);

            // Add header panel to form
            this.Controls.Add(header);


            // Left navigation 
            var nav = new Panel { Width = 260, Dock = DockStyle.Left, BackColor = Color.WhiteSmoke };

            var btnReportIssues = new Button
            {
                Text = "Report Issue",
                Size = new Size(220, 50),
                Location = new Point(20, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(255, 184, 28), // SA Gold
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btnReportIssues.FlatAppearance.BorderSize = 0;
            btnReportIssues.Click += BtnReportIssues_Click;

            var btnEvents = new Button
            {
                Text = "Local Events (coming soon)",
                Size = new Size(220, 44),
                Location = new Point(20, 110),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Enabled = false
            };

            var btnStatus = new Button
            {
                Text = "Service Status (coming soon)",
                Size = new Size(220, 44),
                Location = new Point(20, 170),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Enabled = false
            };

            nav.Controls.Add(btnReportIssues);
            nav.Controls.Add(btnEvents);
            nav.Controls.Add(btnStatus);
            this.Controls.Add(nav);

            // Content panel 
            contentPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            dgvIssues = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };

            // columns
            dgvIssues.Columns.Add("colId", "ID");
            dgvIssues.Columns.Add("colLocation", "Location");
            dgvIssues.Columns.Add("colCategory", "Category");
            dgvIssues.Columns.Add("colDate", "Date Reported");
            dgvIssues.Columns.Add("colAttachments", "Attachments");

            // SA-styled headers
            dgvIssues.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvIssues.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvIssues.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvIssues.EnableHeadersVisualStyles = false;

            dgvIssues.CellDoubleClick += DgvIssues_CellDoubleClick;

            contentPanel.Controls.Add(dgvIssues);
            this.Controls.Add(contentPanel);

            // initial refresh
            RefreshIssuesGrid();
        }

        private void BtnReportIssues_Click(object sender, EventArgs e)
        {
            using (var reportForm = new ReportIssueForm())
            {
                if (reportForm.ShowDialog() == DialogResult.OK)
                {
                    var issue = new Issue
                    {
                        Id = nextIssueId++,
                        Location = reportForm.IssueLocation,
                        Category = reportForm.IssueCategory,
                        Description = reportForm.IssueDescription,
                        DateReported = DateTime.Now,
                        AttachedFiles = reportForm.AttachedFiles.Clone() // deep copy
                    };

                    reportedIssues.Add(issue);
                    RefreshIssuesGrid();

                    MessageBox.Show($"Issue reported successfully!\nIssue ID: {issue.Id}\nThank you for your report.",
                        "Report Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void RefreshIssuesGrid()
        {
            dgvIssues.Rows.Clear();
            foreach (var issue in reportedIssues)
            {
                // show number of attachments 
                int attachCount = issue.AttachedFiles?.Count ?? 0;
                dgvIssues.Rows.Add(issue.Id, issue.Location, issue.Category, issue.DateReported.ToString("g"), attachCount);
            }
        }

        private void DgvIssues_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var idCell = dgvIssues.Rows[e.RowIndex].Cells[0].Value;
            if (idCell == null) return;
            int id = Convert.ToInt32(idCell);

            // find the issue by iterating the linked list
            foreach (var issue in reportedIssues)
            {
                if (issue.Id == id)
                {
                    ShowIssueDetails(issue);
                    break;
                }
            }
        }

        private void ShowIssueDetails(Issue issue)
        {
            // build a readable message (do NOT convert to List)
            var msg = $"ID: {issue.Id}\nLocation: {issue.Location}\nCategory: {issue.Category}\nDate: {issue.DateReported:g}\n\nDescription:\n{issue.Description}\n\nAttachments:\n";
            foreach (var path in issue.AttachedFiles)
            {
                msg += $"{System.IO.Path.GetFileName(path)}\n";
            }

            MessageBox.Show(msg, "Issue Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

//____________________________________________END OF FILE____________________________________________________//