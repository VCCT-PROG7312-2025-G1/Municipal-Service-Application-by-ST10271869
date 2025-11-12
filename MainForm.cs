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

        // Event management system
        private EventManager eventManager;

        // Service request tracking system with advanced data structures
        private ServiceRequestManager serviceRequestManager;

        public MainForm()
        {
            InitializeComponent();
            reportedIssues = new IssueLinkedList();
            nextIssueId = 1;

            // Initialize managers
            eventManager = new EventManager();
            serviceRequestManager = new ServiceRequestManager();

            // Load sample data
            initializeSampleEvents();
            InitializeSampleServiceRequests();

            // Setup UI 
            SetupMainMenu();
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
        }

        // Initializes sample service requests to demonstrate data structures
        // Creates requests with varying priorities and statuses
        private void InitializeSampleServiceRequests()
        {
            // Create sample requests with different priorities
            var request1 = new ServiceRequest
            {
                Id = 1,
                Location = "Main Road Medowridge",
                Category = "Pothole",
                Description = "Large pothole causing traffic hazard",
                DateReported = DateTime.Now.AddDays(-5),
                Priority = Priority.High,
                Status = ServiceRequestStatus.InProgress
            };
            request1.UpdateStatus(ServiceRequestStatus.Pending, "Request received");
            request1.UpdateStatus(ServiceRequestStatus.InProgress, "Crew assigned");
            serviceRequestManager.AddRequest(request1);

            var request2 = new ServiceRequest
            {
                Id = 2,
                Location = "Bunker Road Lakeside",
                Category = "Broken Streetlight",
                Description = "Streetlight not functioning, safet hazard at night",
                DateReported = DateTime.Now.AddDays(-10),
                Priority = Priority.Medium,
                Status = ServiceRequestStatus.Resolved
            };
            request2.UpdateStatus(ServiceRequestStatus.Pending, "Request received");
            request2.UpdateStatus(ServiceRequestStatus.InProgress, "Electrician dispatched");
            request2.UpdateStatus(ServiceRequestStatus.Resolved, "Light repaired and tested");
            serviceRequestManager.AddRequest(request2);

            var request3 = new ServiceRequest
            {
                Id = 3,
                Location = "Waterfront",
                Category = "Water Leak",
                Description = "Major water main break flooding street",
                DateReported = DateTime.Now.AddDays(-1),
                Priority = Priority.Critical,
                Status = ServiceRequestStatus.InProgress
            };
            request3.UpdateStatus(ServiceRequestStatus.Pending, "Emergency request logged");
            request3.UpdateStatus(ServiceRequestStatus.InProgress, "Emergency crew en route");
            serviceRequestManager.AddRequest(request3);

            var request4 = new ServiceRequest
            {
                Id = 4,
                Location = "Pinelands",
                Category = "Fallen Tree",
                Description = "Tree blocking roadway after strong winds",
                DateReported = DateTime.Now.AddDays(-3),
                Priority = Priority.High,
                Status = ServiceRequestStatus.Pending
            };
            request4.UpdateStatus(ServiceRequestStatus.Pending, "Waiting for tree removal team");
            serviceRequestManager.AddRequest(request4);

            var request5 = new ServiceRequest
            {
                Id = 5,
                Location = "M5 First Bridge",
                Category = "Graffiti",
                Description = "Graffiti on exterior wall needs removal",
                DateReported = DateTime.Now.AddDays(-7),
                Priority = Priority.Low,
                Status = ServiceRequestStatus.Pending
            };
            request5.UpdateStatus(ServiceRequestStatus.Pending, "Scheduled for cleanup crew");
            serviceRequestManager.AddRequest(request5);

            var request6 = new ServiceRequest
            {
                Id = 6,
                Location = "Khayelitsha",
                Category = "Streetlight Instalation",
                Description = "New streetlight needed for safety",
                DateReported = DateTime.Now.AddDays(-2),
                Priority = Priority.Medium,
                Status = ServiceRequestStatus.Pending
            };
            request6.UpdateStatus(ServiceRequestStatus.Pending, "Awaiting completion of electrical infrastructure");
            serviceRequestManager.AddRequest(request6);

        // Request 6 depends on Request 3 
            serviceRequestManager.AddDependency(6, 3);
            serviceRequestManager.AddDependency(1, 3);
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
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.FromArgb(0, 120, 71)
            };

            var headerTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            headerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            headerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            headerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

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

            var logo = new PictureBox
            {
                Image = Properties.Resources.coat_of_arms,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill
            };

            headerTable.Controls.Add(lblTitle, 0, 0);
            headerTable.Controls.Add(logo, 1, 0);
            header.Controls.Add(headerTable);
            this.Controls.Add(header);

            // Left navigation 
            var nav = new Panel { Width = 260, Dock = DockStyle.Left, BackColor = Color.WhiteSmoke };

            var btnReportIssues = new Button
            {
                Text = "Report Issue",
                Size = new Size(220, 50),
                Location = new Point(20, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(255, 184, 28),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btnReportIssues.FlatAppearance.BorderSize = 0;
            btnReportIssues.Click += BtnReportIssues_Click;

            var btnEvents = new Button
            {
                Text = "Local Events & Announcements ",
                Size = new Size(220, 50),
                Location = new Point(20, 110),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(255, 184, 28),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Enabled = true,
            };
            btnEvents.FlatAppearance.BorderSize = 0;
            btnEvents.Click += BtnEvents_Click;

            // Service Request Status button 
            var btnStatus = new Button
            {
                Text = "Service Request Status",
                Size = new Size(220, 50),
                Location = new Point(20, 180),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(255, 184, 28),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Enabled = true  // NOW ENABLED!
            };
            btnStatus.FlatAppearance.BorderSize = 0;
            btnStatus.Click += BtnStatus_Click;  

            nav.Controls.Add(btnReportIssues);
            nav.Controls.Add(btnEvents);
            nav.Controls.Add(btnStatus);
            this.Controls.Add(nav);

            var heroPanel = new Panel
            {
                Location = new Point(260, 90),  
                Size = new Size(640, 510),      
                BackColor = Color.White
            };

            var heroPicture = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,

     
                Image = Properties.Resources.Menu_Image

            };

            var welcomeLabel = new Label
            {
                Text = "Welcome to RSA Municipal Services\n\nSelect an option from the menu to get started",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 71),
                Dock = DockStyle.Bottom,
                Height = 80,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(248, 249, 250),
                AutoSize = false
            };

            heroPanel.Controls.Add(heroPicture);
            heroPanel.Controls.Add(welcomeLabel);
        

            // Content panel 
            contentPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            // Add hero panel to content panel
            contentPanel.Controls.Add(heroPanel);

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

            dgvIssues.Columns.Add("colId", "ID");
            dgvIssues.Columns.Add("colLocation", "Location");
            dgvIssues.Columns.Add("colCategory", "Category");
            dgvIssues.Columns.Add("colDate", "Date Reported");
            dgvIssues.Columns.Add("colAttachments", "Attachments");

            dgvIssues.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvIssues.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvIssues.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvIssues.EnableHeadersVisualStyles = false;

            dgvIssues.CellDoubleClick += DgvIssues_CellDoubleClick;

            contentPanel.Controls.Add(dgvIssues);
            this.Controls.Add(contentPanel);

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

                    // ALSO add to service request manager with random priority for demo
                    var priorities = new[] { Priority.Low, Priority.Medium, Priority.High };
                    var random = new Random();
                    serviceRequestManager.AddFromIssue(issue, priorities[random.Next(priorities.Length)]);

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

   
        // Opens the Service Request Status form
        private void BtnStatus_Click(object sender, EventArgs e)
        {
            ServiceRequestStatusForm statusForm = new ServiceRequestStatusForm(serviceRequestManager);
            statusForm.ShowDialog();
        }

        private void RefreshIssuesGrid()
        {
            dgvIssues.Rows.Clear();
            foreach (var issue in reportedIssues)
            {
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