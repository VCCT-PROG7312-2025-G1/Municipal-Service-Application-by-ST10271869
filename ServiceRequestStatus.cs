using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Municipal_Service_Application
{
    public partial class ServiceRequestStatusForm : Form
    {
        private ServiceRequestManager requestManager;

        public ServiceRequestStatusForm(ServiceRequestManager manager)
        {
            InitializeComponent();
            requestManager = manager;
            this.Load += ServiceRequestStatusForm_Load;
        }

        private void ServiceRequestStatusForm_Load(object sender, EventArgs e)
        {
            // Apply SA theme styling
            ApplySATheme();

            // Load status filter options
            LoadStatusFilters();

            // Display all requests initially
            LoadAllRequests();

            // Show statistics
            UpdateStatistics();
        }


        // Applies South African flag colors and professional styling
        // Maintains consistency with other forms in the application

        private void ApplySATheme()
        {
            // Style the DataGridView headers
            dgvRequests.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvRequests.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvRequests.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRequests.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgvRequests.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Style the data rows
            dgvRequests.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvRequests.DefaultCellStyle.BackColor = Color.White;
            dgvRequests.DefaultCellStyle.ForeColor = Color.Black;
            dgvRequests.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 184, 28); // SA Gold
            dgvRequests.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvRequests.DefaultCellStyle.Padding = new Padding(5);

            // Alternating row colors for readability
            dgvRequests.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvRequests.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 184, 28);
            dgvRequests.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
        }


        // Populates the status filter dropdown with all status options

        private void LoadStatusFilters()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add("All Statuses");

            foreach (ServiceRequestStatus status in Enum.GetValues(typeof(ServiceRequestStatus)))
            {
                cmbStatusFilter.Items.Add(status.ToString());
            }

            cmbStatusFilter.SelectedIndex = 0;
        }


        // Loads all service requests into the DataGridView
        // Uses BST GetAllSorted() for O(n) sorted retrieval

        private void LoadAllRequests()
        {
            dgvRequests.Rows.Clear();

            // Get all requests sorted by ID (demonstrates BST in-order traversal)
            var requests = requestManager.GetAllSortedById();

            foreach (var request in requests)
            {
                AddRequestToGrid(request);
            }
        }


        // Adds a single request to the DataGridView with color-coded status

        private void AddRequestToGrid(ServiceRequest request)
        {
            int rowIndex = dgvRequests.Rows.Add(
                request.Id,
                request.Location,
                request.Category,
                request.Status.ToString(),
                request.Priority.ToString(),
                request.DateReported.ToShortDateString(),
                request.EstimatedCompletion?.ToShortDateString() ?? "N/A"
            );

            // Color-code the status cell based on status
            DataGridViewRow row = dgvRequests.Rows[rowIndex];
            Color statusColor = GetStatusColor(request.Status);
            row.Cells[3].Style.BackColor = statusColor;
            row.Cells[3].Style.ForeColor = Color.White;
            row.Cells[3].Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // Color-code priority cell
            if (request.Priority == Priority.Critical || request.Priority == Priority.High)
            {
                row.Cells[4].Style.BackColor = Color.FromArgb(220, 53, 69); // Red
                row.Cells[4].Style.ForeColor = Color.White;
                row.Cells[4].Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }
        }


        // Returns appropriate color for each status
        private Color GetStatusColor(ServiceRequestStatus status)
        {
            return status switch
            {
                ServiceRequestStatus.Pending => Color.FromArgb(255, 193, 7),      // Yellow/Gold
                ServiceRequestStatus.InProgress => Color.FromArgb(0, 123, 255),   // Blue
                ServiceRequestStatus.Resolved => Color.FromArgb(40, 167, 69),     // Green
                ServiceRequestStatus.Rejected => Color.FromArgb(220, 53, 69),     // Red
                _ => Color.Gray
            };
        }


        // Handles search functionality

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            dgvRequests.Rows.Clear();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadAllRequests();
                return;
            }

            // Try to parse as ID for BST search (O(log n))
            if (int.TryParse(searchText, out int id))
            {
                var request = requestManager.SearchById(id);
                if (request != null)
                {
                    AddRequestToGrid(request);
                    return;
                }
            }

            // Otherwise, do text-based search
            var results = requestManager.SearchByText(searchText);
            foreach (var request in results)
            {
                AddRequestToGrid(request);
            }
        }


        // Handles status filter changes

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvRequests.Rows.Clear();

            string selectedStatus = cmbStatusFilter.SelectedItem.ToString();

            if (selectedStatus == "All Statuses")
            {
                LoadAllRequests();
                return;
            }

            // Parse the selected status
            if (Enum.TryParse<ServiceRequestStatus>(selectedStatus, out var status))
            {
                // Get requests by status using hash table (O(1) lookup)
                var requests = requestManager.GetByStatus(status);
                foreach (var request in requests)
                {
                    AddRequestToGrid(request);
                }
            }
        }


        // Shows detailed view of a request including status history

        private void dgvRequests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgvRequests.Rows[e.RowIndex].Cells[0].Value);

            // Use BST search for O(log n) retrieval
            var request = requestManager.SearchById(id);
            if (request == null) return;

            ShowRequestDetails(request);
        }



        // Displays detailed information about a service request
        private void ShowRequestDetails(ServiceRequest request)
        {
            string details = $"\n";
            details += $"SERVICE REQUEST DETAILS\n";
            details += $"\n\n";

            details += $"Request ID: {request.Id}\n";
            details += $"Location: {request.Location}\n";
            details += $"Category: {request.Category}\n";
            details += $"Priority: {request.Priority}\n";
            details += $"Status: {request.Status}\n";
            details += $"Reported: {request.DateReported:g}\n";

            if (request.EstimatedCompletion.HasValue)
                details += $"Estimated Completion: {request.EstimatedCompletion.Value:g}\n";

            details += $"\nDescription:\n{request.Description}\n";

            // Show status history timeline
            if (request.StatusHistory.Count > 0)
            {
                details += $"\n\n";
                details += $"STATUS HISTORY TIMELINE\n";
                details += $"\n\n";

                foreach (var update in request.StatusHistory)
                {
                    details += $"🔹 {update.Timestamp:g}\n";
                    details += $"   Status: {update.Status}\n";
                    if (!string.IsNullOrWhiteSpace(update.Notes))
                        details += $"   Note: {update.Notes}\n";
                    details += "\n";
                }
            }

            // Show dependencies using graph
            var dependencies = requestManager.GetDependentRequests(request.Id);
            if (dependencies.Count > 0)
            {
                details += $"\n\n";
                details += $"DEPENDENT REQUESTS (Graph)\n";
                details += $"\n\n";
                details += "This request must be completed before:\n\n";

                foreach (var dep in dependencies)
                {
                    details += $"  • Request #{dep.Id}: {dep.Location} - {dep.Category}\n";
                }
            }

            MessageBox.Show(details, "Service Request Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        // Updates and displays statistics panel

        private void UpdateStatistics()
        {
            var stats = requestManager.GetStatistics();

            lblTotalRequests.Text = $"Total: {stats.TotalRequests}";
            lblPendingCount.Text = $"Pending: {stats.PendingCount}";
            lblInProgressCount.Text = $"In Progress: {stats.InProgressCount}";
            lblResolvedCount.Text = $"Resolved: {stats.ResolvedCount}";
            lblHighPriority.Text = $"High Priority: {stats.HighPriorityCount}";
            lblAvgResolution.Text = $"Avg Resolution: {stats.AverageResolutionTime:F1} days";
        }


        // Simulates status update 

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a request to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells[0].Value);
            var request = requestManager.SearchById(id);

            if (request == null) return;

            var nextStatus = request.Status switch
            {
                ServiceRequestStatus.Pending => ServiceRequestStatus.InProgress,
                ServiceRequestStatus.InProgress => ServiceRequestStatus.Resolved,
                _ => request.Status
            };

            if (nextStatus != request.Status)
            {
                requestManager.UpdateRequestStatus(id, nextStatus, $"Status updated to {nextStatus}");
                LoadAllRequests();
                UpdateStatistics();

                MessageBox.Show($"Request #{id} updated to {nextStatus}", "Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // Shows the highest priority request from the heap

        private void btnShowNextPriority_Click(object sender, EventArgs e)
        {
            var nextRequest = requestManager.GetNextHighestPriority();

            if (nextRequest == null)
            {
                MessageBox.Show("No pending requests in the priority queue.", "Queue Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string message = $"HIGHEST PRIORITY REQUEST\n\n";
            message += $"ID: {nextRequest.Id}\n";
            message += $"Priority: {nextRequest.Priority}\n";
            message += $"Location: {nextRequest.Location}\n";
            message += $"Category: {nextRequest.Category}\n";
            message += $"Description: {nextRequest.Description}\n\n";
            message += "This request should be addressed first!";

            MessageBox.Show(message, "Next Priority Request (Heap)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    private void btnViewDependencies_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a request to view its dependencies.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedId = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells[0].Value);
            var selectedRequest = requestManager.SearchById(selectedId);

            if (selectedRequest == null) return;

            // Get dependencies 
            var dependencies = requestManager.GetDependencies(selectedId);

            // Get dependent requests 
            var dependents = requestManager.GetDependentRequests(selectedId);

            // Get all related requests using BFS
            var related = requestManager.GetRelatedRequests(selectedId);

            string message = $"DEPENDENCY GRAPH ANALYSIS\n";
            message += $"\n\n";
            message += $"Request #{selectedId}\n";
            message += $"{selectedRequest.Category} - {selectedRequest.Location}\n";
            message += $"Status: {selectedRequest.Status}\n\n";
            message += $"\n\n";

            // Show what this depends on
            message += "THIS REQUEST DEPENDS ON:\n\n";
            if (dependencies.Count > 0)
            {
                foreach (var depId in dependencies)
                {
                    var depReq = requestManager.SearchById(depId);
                    message += $"  → Request #{depId}: {depReq.Category}\n";
                    message += $"     Location: {depReq.Location}\n";
                    message += $"     Status: {depReq.Status}\n\n";
                }
            }
            else
            {
                message += "  None - can proceed independently\n\n";
            }

            message += "\n\n";

            // Show what depends on this
            message += "REQUESTS WAITING ON THIS:\n\n";
            if (dependents.Count > 0)
            {
                foreach (var dep in dependents)
                {
                    message += $"  → Request #{dep.Id}: {dep.Category}\n";
                    message += $"     Location: {dep.Location}\n";
                    message += $"     Status: {dep.Status}\n\n";
                }
            }
            else
            {
                message += "  ✓ None - no requests are blocked\n\n";
            }

            message += "\n\n";

            // Show all related (BFS traversal)
            message += "ALL RELATED REQUESTS (BFS Traversal):\n\n";
            if (related.Count > 0)
            {
                foreach (var rel in related)
                {
                    message += $"  → Request #{rel.Id}: {rel.Category} ({rel.Status})\n";
                }
            }
            else
            {
                message += "  ✓ None - this request is isolated\n";
            }

            MessageBox.Show(message, "Dependency Graph - Graph Data Structure",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

//-----------------------------------------------END OF FILE-----------------------------------------------//