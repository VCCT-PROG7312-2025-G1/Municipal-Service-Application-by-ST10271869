using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Municipal_Service_Application
{
    public partial class LocalEventsForm : Form
    {
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
            // Populate the category filter when form opens
            LoadCategories();
            // Display all events initially
            LoadAllEvents();
        }

     
        // Populates the ComboBox with available event categories
        // Adds "All Categories" as the default option
     
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

            // Set "All Categories" as the selected item by default
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
    }
}