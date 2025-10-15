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

        private EventManager eventManager;

        public MainForm()
        {
            InitializeComponent();
            reportedIssues = new IssueLinkedList();
            nextIssueId = 1;
            SetupMainMenu();

            eventManager = new EventManager();

            initializeSampleEvents();

        }

        private void initializeSampleEvents()
        {
            eventManager.AddEvent(new Event(
                "Community Clean-Up Day",
                "Join us for a day of cleaning and making our neighbourhoods look great.",
                new DateTime(2025, 10, 20),
                "Community"
                ));

            eventManager.AddEvent(new Event(
                "Municipal council meeting",
                "Financial, urban waste management, and Planning and Resilience meeting.",
                new DateTime(2025, 12, 2),
                "Goverment"
                ));

            eventManager.AddEvent(new Event(
                "Road Maintenance notice",
                "N1 outbound lane construction to begin.",
                new DateTime(2026, 02, 27),
                "Infrastructure"
                ));

            eventManager.AddEvent(new Event(
                "Cape Town Cycle Tour",
                "Road closures to be expected from 04:00am to 13:00pm for the Cape Town Cycle Tour.",
                new DateTime(2026, 03, 8),
                "Community"
                ));

            eventManager.AddEvent(new Event(
                "Annual Fun run",
                "Join us at our annual 5km or 10km Fun run to raise awareness for cancer.",
                new DateTime(2025, 12, 2),
                "Community"
                ));

            eventManager.AddEvent(new Event(
                "Eskom Load Shedding",
                "Loadshedding to start up again in the next coming months.",
                new DateTime(2026, 05, 2),
                "Infrastructure"
                ));

            eventManager.AddEvent(new Event(
                "Blood Drive",
                "Join us at Western Cape blood service center, as we strive to supply the Cape with safe blood products.",
                new DateTime(2026, 01, 29),
                "Health"
                ));

            eventManager.AddEvent(new Event(
                "Budget Meeting",
                "Public consoltation on the 2026 municipal budget allocations.",
                new DateTime(2026, 01, 25),
                "Government"
                ));

            eventManager.AddEvent(new Event(
                "Wine Market",
                "Join us on chruch street for our 7th Wine Market.",
                new DateTime(2026, 06, 10),
                "Community"
                ));

            eventManager.AddEvent(new Event(
                "Water pipe interruption",
                "Scheduled maintenance on water pipes in plumstead for the August 2026.",
                new DateTime(2026, 08, 08),
                "Utilities"
                ));

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
                Height = 90, // Height tall enough for 2-line title
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

            // Logo 
            var logo = new PictureBox
            {
                Image = Properties.Resources.coat_of_arms,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill
            };

            // controls to TableLayoutPanel
            headerTable.Controls.Add(lblTitle, 0, 0);
            headerTable.Controls.Add(logo, 1, 0);

            // TableLayoutPanel to header panel
            header.Controls.Add(headerTable);

            // header panel to form
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
                Text = "Local Events & Announcements ",
                Size = new Size(220, 44),
                Location = new Point(20, 110),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(255, 184, 28), // SA Gold,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Enabled = true,

            };

            btnEvents.FlatAppearance.BorderSize = 0;
            btnEvents.Click += BtnEvents_Click;

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
                        AttachedFiles = reportForm.AttachedFiles.Clone() 
                    };

                    reportedIssues.Add(issue);
                    RefreshIssuesGrid();

                    MessageBox.Show($"Issue reported successfully!\nIssue ID: {issue.Id}\nThank you for your report.",
                        "Report Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnEvents_Click(object sender, EventArgs e)
        {
            LocalEventsForm eventsForm = new LocalEventsForm(eventManager);

            eventsForm.ShowDialog();

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