using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Municipal_Service_Application
{
    public partial class LocalEventsForm : Form
    {
        // Tracks how often the user searches for each keyword or category
        private Dictionary<string, int> searchHistory = new Dictionary<string, int>();


        // Store reference to the event manager to access event data
        private EventManager eventManager;

        public LocalEventsForm(EventManager manager)
        {
            InitializeComponent();
            // Assign the passed event manager so we can work with existing events
            eventManager = manager;

            // Set up the form when it loads
            this.Load += LocalEventsForm_Load;
        }

        private void LocalEventsForm_Load(object sender, EventArgs e)
        {
            ApplySATheme();
            LoadCategories();
            LoadAllEvents();
            CreateRecommendButton();
        }

        private void CreateRecommendButton()
        {
            Button btnRecommend = new Button();
            btnRecommend.Name = "btnRecommend";
            btnRecommend.Text = "Recommendation";
            btnRecommend.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRecommend.BackColor = Color.FromArgb(0, 122, 51); // SA Green
            btnRecommend.ForeColor = Color.White;
            btnRecommend.FlatStyle = FlatStyle.Flat;
            btnRecommend.FlatAppearance.BorderSize = 0;
            btnRecommend.Size = new Size(160, 30);
            btnRecommend.Location = new Point(480, 20);
            btnRecommend.Click += BtnRecommend_Click;
            // hover effects 
            btnRecommend.MouseEnter += (s, e) => btnRecommend.BackColor = Color.FromArgb(255, 184, 28); // SA Gold
            btnRecommend.MouseLeave += (s, e) => btnRecommend.BackColor = Color.FromArgb(0, 122, 51); // Back to Green
            pnlFilters.Controls.Add(btnRecommend);
            btnRecommend.BringToFront();
        }

        private void ApplySATheme()
        {
            // Style the DataGridView headers with SA colors
            dgvEvents.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvEvents.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvEvents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEvents.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgvEvents.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Style the data rows
            dgvEvents.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvEvents.DefaultCellStyle.BackColor = Color.White;
            dgvEvents.DefaultCellStyle.ForeColor = Color.Black;
            dgvEvents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 184, 28); // SA Gold
            dgvEvents.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvEvents.DefaultCellStyle.Padding = new Padding(5);
            dgvEvents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvEvents.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 184, 28);
            dgvEvents.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.BackColor = Color.White;
            cmbCategoryFilter.BackColor = Color.White;
            cmbCategoryFilter.ForeColor = Color.Black;
        }


        private void LoadCategories()
        {
            // Clear any existing items to avoid duplicates
            cmbCategoryFilter.Items.Clear();

            // Add a default option to show all events
            cmbCategoryFilter.Items.Add("All Categories");

            // Get all unique categories from the event manager
            foreach (var category in eventManager.GetCatergories())
            {
                cmbCategoryFilter.Items.Add(category);
            }
                cmbCategoryFilter.SelectedIndex = 0;
        }


        // Loads all events into the DataGridView
        // Called when the form first opens or when "All Categories" is selected
        private void LoadAllEvents()
        {
            // Clear existing rows to prevent duplicates
            dgvEvents.Rows.Clear();

            // Get all events from the manager (already sorted by date)
            foreach (var ev in eventManager.GetAllEvents())
            {
                // Add each event as a new row with its properties
                dgvEvents.Rows.Add(
                    ev.Title,
                    ev.Catergory,
                    ev.Date.ToShortDateString(),
                    ev.Description
                );
            }
        }


        // Filters events based on the selected category
        // Uses case-insensitive search for better user experience

        private void FilterEventsByCategory(string category)
        {
            // Clear the grid before adding filtered results
            dgvEvents.Rows.Clear();

            // Search for events matching the selected category
            foreach (var ev in eventManager.SearchByCatergory(category))
            {
                // Add matching events to the grid
                dgvEvents.Rows.Add(
                    ev.Title,
                    ev.Catergory,
                    ev.Date.ToShortDateString(),
                    ev.Description
                );
            }
        }


        // Handles search/filter logic when user types in the search bar
        // Filters by title or description containing the search text

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Get the search text and convert to lowercase for case-insensitive search
            string searchText = txtSearch.Text.ToLower();

            // Record user searches to analyze preferences
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (searchHistory.ContainsKey(searchText))
                    searchHistory[searchText]++;
                else
                    searchHistory.Add(searchText, 1);
            }

            // Clear grid for fresh search results
            dgvEvents.Rows.Clear();

            // Get all events and filter based on search text
            foreach (var ev in eventManager.GetAllEvents())
            {
                // Check if title or description contains the search text
                bool matchesSearch = ev.Title.ToLower().Contains(searchText) ||
                                   ev.Description.ToLower().Contains(searchText);

                // Only add events that match the search criteria
                if (matchesSearch)
                {
                    dgvEvents.Rows.Add(
                        ev.Title,
                        ev.Catergory,
                        ev.Date.ToShortDateString(),
                        ev.Description
                    );
                }
            }
        }


        // Handles category filter changes in the ComboBox
        // Either shows all events or filters by selected category

        private void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected category from the combo box
            string selectedCategory = cmbCategoryFilter.SelectedItem.ToString();

            // Check if user selected "All Categories"
            if (selectedCategory == "All Categories")
            {
                // Show all events without filtering
                LoadAllEvents();
            }
            else
            {
                // Filter events by the selected category
                FilterEventsByCategory(selectedCategory);
            }
        }

        private List<Event> GetRecommendedEvents(string searchText)
        {
            List<Event> recommendedEvents = new List<Event>();
            var allEvents = eventManager.GetAllEvents();
            string favouriteSearch = "";

            if (searchHistory.Count > 0)
            {
                // Find the most frequently searched keyword
                favouriteSearch = searchHistory.OrderByDescending(s => s.Value).First().Key.ToLower();
            }

            foreach (var ev in allEvents)
            {
                // Skip events that exactly match what's currently being searched
                bool isCurrentSearch = !string.IsNullOrEmpty(searchText) &&
                                       (ev.Title.ToLower().Contains(searchText) ||
                                        ev.Description.ToLower().Contains(searchText));

                if (isCurrentSearch)
                    continue;

                // Recommend based on favourite search history OR similar categories
                bool matchesFavourite = !string.IsNullOrEmpty(favouriteSearch) &&
                                        (ev.Title.ToLower().Contains(favouriteSearch) ||
                                         ev.Catergory.ToLower().Contains(favouriteSearch) ||
                                         ev.Description.ToLower().Contains(favouriteSearch));

                if (matchesFavourite)
                {
                    recommendedEvents.Add(ev);
                }
            }

            // If no recommendations from search history, recommend upcoming events
            if (recommendedEvents.Count == 0)
            {
                recommendedEvents = allEvents.Take(5).ToList();
            }

            return recommendedEvents.Take(5).ToList();
        }


        private void BtnRecommend_Click(object sender, EventArgs e)
        {
            // Get the current search text (if any)
            string searchText = txtSearch?.Text?.ToLower() ?? "";

            // Get recommended events
            List<Event> recommended = GetRecommendedEvents(searchText);

            // Clear the DataGridView
            dgvEvents.Rows.Clear();

            // Display recommended events in the DataGridView
            foreach (var ev in recommended)
            {
                dgvEvents.Rows.Add(
                    ev.Title,
                    ev.Catergory,
                    ev.Date.ToShortDateString(),
                    ev.Description
                );
            }

            // Optional: Show a message if no recommendations found
            if (recommended.Count == 0)
            {
                MessageBox.Show("No recommendations available at this time.",
                               "Recommendations",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Showing {recommended.Count} recommended events based on your search history.",
                               "Recommendations",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
            }
        }
    }
}
       

    


//____________________________________________END OF FILE____________________________________________________//