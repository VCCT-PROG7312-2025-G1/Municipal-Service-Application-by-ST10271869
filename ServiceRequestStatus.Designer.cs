namespace Municipal_Service_Application
{
    partial class ServiceRequestStatusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblStatusFilter = new System.Windows.Forms.Label();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnShowNextPriority = new System.Windows.Forms.Button();
            this.btnViewDependencies = new System.Windows.Forms.Button();
            this.pnlStatistics = new System.Windows.Forms.Panel();
            this.lblTotalRequests = new System.Windows.Forms.Label();
            this.lblPendingCount = new System.Windows.Forms.Label();
            this.lblInProgressCount = new System.Windows.Forms.Label();
            this.lblResolvedCount = new System.Windows.Forms.Label();
            this.lblHighPriority = new System.Windows.Forms.Label();
            this.lblAvgResolution = new System.Windows.Forms.Label();
            this.dgvRequests = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPriority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateReported = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstimated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.pnlStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            this.SuspendLayout();
          
            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(71)))));
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 80);
            this.pnlHeader.TabIndex = 0;
           
            // lblHeader 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1200, 80);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Service Request Status Tracker";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
             
            // pnlFilters

            this.pnlFilters.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlFilters.Controls.Add(this.btnShowNextPriority);
            this.pnlFilters.Controls.Add(this.btnViewDependencies);
            this.pnlFilters.Controls.Add(this.btnUpdateStatus);
            this.pnlFilters.Controls.Add(this.lblStatusFilter);
            this.pnlFilters.Controls.Add(this.cmbStatusFilter);
            this.pnlFilters.Controls.Add(this.lblSearch);
            this.pnlFilters.Controls.Add(this.txtSearch);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(0, 80);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlFilters.Size = new System.Drawing.Size(1200, 100);
            this.pnlFilters.TabIndex = 1;
        
            // lblSearch
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearch.Location = new System.Drawing.Point(30, 23);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(120, 23);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search by ID:";
            
            // txtSearch
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(160, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 30);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
           
            // lblStatusFilter
            this.lblStatusFilter.AutoSize = true;
            this.lblStatusFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatusFilter.Location = new System.Drawing.Point(30, 63);
            this.lblStatusFilter.Name = "lblStatusFilter";
            this.lblStatusFilter.Size = new System.Drawing.Size(63, 23);
            this.lblStatusFilter.TabIndex = 2;
            this.lblStatusFilter.Text = "Status:";
           
            // cmbStatusFilter
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatusFilter.FormattingEnabled = true;
            this.cmbStatusFilter.Location = new System.Drawing.Point(160, 60);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(250, 31);
            this.cmbStatusFilter.TabIndex = 3;
            this.cmbStatusFilter.SelectedIndexChanged += new System.EventHandler(this.cmbStatusFilter_SelectedIndexChanged);
           
            // btnUpdateStatus

            this.btnUpdateStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(71)))));
            this.btnUpdateStatus.FlatAppearance.BorderSize = 0;
            this.btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdateStatus.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStatus.Location = new System.Drawing.Point(450, 20);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(180, 70);
            this.btnUpdateStatus.TabIndex = 4;
            this.btnUpdateStatus.Text = "▶️ Progress Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = false;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
        
            // btnShowNextPriority
            this.btnShowNextPriority.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(28)))));
            this.btnShowNextPriority.FlatAppearance.BorderSize = 0;
            this.btnShowNextPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowNextPriority.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnShowNextPriority.ForeColor = System.Drawing.Color.Black;
            this.btnShowNextPriority.Location = new System.Drawing.Point(650, 20);
            this.btnShowNextPriority.Name = "btnShowNextPriority";
            this.btnShowNextPriority.Size = new System.Drawing.Size(200, 70);
            this.btnShowNextPriority.TabIndex = 5;
            this.btnShowNextPriority.Text = "🔥 Next Priority\r\n(Heap)";
            this.btnShowNextPriority.UseVisualStyleBackColor = false;
            this.btnShowNextPriority.Click += new System.EventHandler(this.btnShowNextPriority_Click);

            // btnShowNextPriority
            this.btnShowNextPriority.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(28)))));
            this.btnShowNextPriority.FlatAppearance.BorderSize = 0;
            this.btnShowNextPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowNextPriority.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnShowNextPriority.ForeColor = System.Drawing.Color.Black;
            this.btnShowNextPriority.Location = new System.Drawing.Point(650, 20);
            this.btnShowNextPriority.Name = "btnShowNextPriority";
            this.btnShowNextPriority.Size = new System.Drawing.Size(200, 70);
            this.btnShowNextPriority.TabIndex = 5;
            this.btnShowNextPriority.Text = "🔥 Next Priority\r\n(Heap)";
            this.btnShowNextPriority.UseVisualStyleBackColor = false;
            this.btnShowNextPriority.Click += new System.EventHandler(this.btnShowNextPriority_Click);

            // btnViewDependencies
            this.btnViewDependencies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnViewDependencies.FlatAppearance.BorderSize = 0;
            this.btnViewDependencies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewDependencies.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnViewDependencies.ForeColor = System.Drawing.Color.White;
            this.btnViewDependencies.Location = new System.Drawing.Point(870, 20);
            this.btnViewDependencies.Name = "btnViewDependencies";
            this.btnViewDependencies.Size = new System.Drawing.Size(200, 70);
            this.btnViewDependencies.TabIndex = 6;
            this.btnViewDependencies.Text = "🔗 View Dependencies\r\n(Graph)";
            this.btnViewDependencies.UseVisualStyleBackColor = false;
            this.btnViewDependencies.Click += new System.EventHandler(this.btnViewDependencies_Click);

            // pnlStatistics
            this.pnlStatistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlStatistics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatistics.Controls.Add(this.lblAvgResolution);
            this.pnlStatistics.Controls.Add(this.lblHighPriority);
            this.pnlStatistics.Controls.Add(this.lblResolvedCount);
            this.pnlStatistics.Controls.Add(this.lblInProgressCount);
            this.pnlStatistics.Controls.Add(this.lblPendingCount);
            this.pnlStatistics.Controls.Add(this.lblTotalRequests);
            this.pnlStatistics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatistics.Location = new System.Drawing.Point(0, 630);
            this.pnlStatistics.Name = "pnlStatistics";
            this.pnlStatistics.Size = new System.Drawing.Size(1200, 70);
            this.pnlStatistics.TabIndex = 2;
         
            // lblTotalRequests 
            this.lblTotalRequests.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalRequests.Location = new System.Drawing.Point(20, 20);
            this.lblTotalRequests.Name = "lblTotalRequests";
            this.lblTotalRequests.Size = new System.Drawing.Size(150, 30);
            this.lblTotalRequests.TabIndex = 0;
            this.lblTotalRequests.Text = "Total: 0";
            this.lblTotalRequests.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
             
            // lblPendingCount 
            this.lblPendingCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPendingCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.lblPendingCount.Location = new System.Drawing.Point(200, 20);
            this.lblPendingCount.Name = "lblPendingCount";
            this.lblPendingCount.Size = new System.Drawing.Size(150, 30);
            this.lblPendingCount.TabIndex = 1;
            this.lblPendingCount.Text = "Pending: 0";
            this.lblPendingCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblInProgressCount
            this.lblInProgressCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInProgressCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblInProgressCount.Location = new System.Drawing.Point(380, 20);
            this.lblInProgressCount.Name = "lblInProgressCount";
            this.lblInProgressCount.Size = new System.Drawing.Size(180, 30);
            this.lblInProgressCount.TabIndex = 2;
            this.lblInProgressCount.Text = "In Progress: 0";
            this.lblInProgressCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         
            // lblResolvedCount
            this.lblResolvedCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblResolvedCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblResolvedCount.Location = new System.Drawing.Point(590, 20);
            this.lblResolvedCount.Name = "lblResolvedCount";
            this.lblResolvedCount.Size = new System.Drawing.Size(150, 30);
            this.lblResolvedCount.TabIndex = 3;
            this.lblResolvedCount.Text = "Resolved: 0";
            this.lblResolvedCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
     
            // lblHighPriority 
            this.lblHighPriority.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHighPriority.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblHighPriority.Location = new System.Drawing.Point(770, 20);
            this.lblHighPriority.Name = "lblHighPriority";
            this.lblHighPriority.Size = new System.Drawing.Size(180, 30);
            this.lblHighPriority.TabIndex = 4;
            this.lblHighPriority.Text = "High Priority: 0";
            this.lblHighPriority.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblAvgResolution
            this.lblAvgResolution.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAvgResolution.Location = new System.Drawing.Point(980, 20);
            this.lblAvgResolution.Name = "lblAvgResolution";
            this.lblAvgResolution.Size = new System.Drawing.Size(200, 30);
            this.lblAvgResolution.TabIndex = 5;
            this.lblAvgResolution.Text = "Avg Resolution: 0.0 days";
            this.lblAvgResolution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // dgvRequests
            this.dgvRequests.AllowUserToAddRows = false;
            this.dgvRequests.AllowUserToDeleteRows = false;
            this.dgvRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRequests.BackgroundColor = System.Drawing.Color.White;
            this.dgvRequests.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRequests.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvRequests.ColumnHeadersHeight = 40;
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRequests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colLocation,
            this.colCategory,
            this.colStatus,
            this.colPriority,
            this.colDateReported,
            this.colEstimated});
            this.dgvRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRequests.EnableHeadersVisualStyles = false;
            this.dgvRequests.GridColor = System.Drawing.Color.LightGray;
            this.dgvRequests.Location = new System.Drawing.Point(0, 180);
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.ReadOnly = true;
            this.dgvRequests.RowHeadersVisible = false;
            this.dgvRequests.RowHeadersWidth = 51;
            this.dgvRequests.RowTemplate.Height = 35;
            this.dgvRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRequests.Size = new System.Drawing.Size(1200, 450);
            this.dgvRequests.TabIndex = 3;
            this.dgvRequests.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRequests_CellDoubleClick);
 
            // colId
            this.colId.FillWeight = 10F;
            this.colId.HeaderText = "ID";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
      
            // colLocation
            this.colLocation.FillWeight = 20F;
            this.colLocation.HeaderText = "Location";
            this.colLocation.MinimumWidth = 6;
            this.colLocation.Name = "colLocation";
            this.colLocation.ReadOnly = true;
        
            // colCategory
            this.colCategory.FillWeight = 15F;
            this.colCategory.HeaderText = "Category";
            this.colCategory.MinimumWidth = 6;
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
        
            // colStatus
            this.colStatus.FillWeight = 15F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
       
            // colPriority
            this.colPriority.FillWeight = 12F;
            this.colPriority.HeaderText = "Priority";
            this.colPriority.MinimumWidth = 6;
            this.colPriority.Name = "colPriority";
            this.colPriority.ReadOnly = true;
    
            // colDateReported
            this.colDateReported.FillWeight = 14F;
            this.colDateReported.HeaderText = "Date Reported";
            this.colDateReported.MinimumWidth = 6;
            this.colDateReported.Name = "colDateReported";
            this.colDateReported.ReadOnly = true;

            // colEstimated
            this.colEstimated.FillWeight = 14F;
            this.colEstimated.HeaderText = "Est. Completion";
            this.colEstimated.MinimumWidth = 6;
            this.colEstimated.Name = "colEstimated";
            this.colEstimated.ReadOnly = true;
            
            // ServiceRequestStatusForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.dgvRequests);
            this.Controls.Add(this.pnlStatistics);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ServiceRequestStatusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Request Status - RSA Municipal Portal";
            this.pnlHeader.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlStatistics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
        // Controls
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblStatusFilter;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnShowNextPriority;
        private System.Windows.Forms.Button btnViewDependencies;
        private System.Windows.Forms.Panel pnlStatistics;
        private System.Windows.Forms.Label lblTotalRequests;
        private System.Windows.Forms.Label lblPendingCount;
        private System.Windows.Forms.Label lblInProgressCount;
        private System.Windows.Forms.Label lblResolvedCount;
        private System.Windows.Forms.Label lblHighPriority;
        private System.Windows.Forms.Label lblAvgResolution;
        private System.Windows.Forms.DataGridView dgvRequests;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPriority;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateReported;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstimated;

    }
}