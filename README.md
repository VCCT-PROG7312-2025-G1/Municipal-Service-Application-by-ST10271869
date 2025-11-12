 # Municipal Service Application

 # Please Note that AI and InteliCode(Visual Studios built in capability) was used through the duration of this project 
 # for Hard Coded Layout guide instead of using toolbox, and debuggin and error resolution

 # Reference List for Part 3 
	- https://www.geeksforgeeks.org/dsa/priority-queue-set-1-introduction/ 
	- https://www.c-sharpcorner.com/article/binary-search-tree/
	- https://www.geeksforgeeks.org/graph-and-its-representations/
	- https://www.geeksforgeeks.org/breadth-first-search-or-bfs-for-a-graph/
	- https://www.geeksforgeeks.org/hashing-data-structure/
	- https://stackoverflow.com/questions/30618651/minheap-implementation-in-c-sharp

A Windows Forms desktop application for reporting and managing municipal issues.
Built with C# (.NET Framework) using object-oriented programming and a custom linked list data structure.


# Features 

# Report a Municipal Issue

- Users can submit issues with:

- Location

- Category (Road Maintenance, Water, Electricity, etc.)

- Description

- Attachments (files/images/documents)

# Custom Data Structure
Issues are stored in a linked list (IssueLinkedList), implemented manually.

# Unique Issue Tracking
Each issue gets a unique ID (ID.1, ID.2, …).

User-Friendly Forms

MainForm: main menu with navigation.

ReportIssueForm: form to submit new issues with progress bar and validation.


# Local Events & Announcements

- Displays upcoming municipal events.

- Filter by catergory (e.g., Community, Safety, Environment).

- search events by title or description.

- View detailed event information.

- Sorted chronologically for ease of use 


# Service Request Status

- View submitted issues and their statuses.

- Update issue status (Pending, In Progress, Resolved).

- Search and filter issues by location, category, or status.

- Detailed issue view with all information and attachments.

- Sorted by submission date or status for efficient management.

- Priority Queue using Min Heap for urgent issues.

- Dependency Tracking using Graph structure with Binary Tree Search traversal

- Dependency visualization using Tree structure

# Binary Search Tree (BST)
Purpose: Fast ID-based lookups of service requests
- Time Complexity:** O(log n) for search operations
- Use Case:** When a user searches by Request ID, the BST quickly locates the exact request
- Implementation:** In-order traversal provides sorted output by ID

# Min-Heap (Priority Queue)
Purpose: Always retrieve the highest priority request instantly
- Time Complexity: O(1) for peek, O(log n) for insertion
- Use Case: Municipal workers can immediately see which request needs attention first (Critical → High → Medium → Low)
- Implementation: Heap maintains priority ordering automatically

# Graph (Adjacency List)
Purpose: Track dependencies between service requests
- Time Complexity:** O(V + E) for BFS traversal
- Use Case: Some requests depend on others (e.g., streetlight installation must wait for water leak repair)
- Implementation: 
  - Adjacency list stores dependencies
  - BFS finds all related requests
  - Cycle detection prevents circular dependencies

# Hash Table (Dictionary)
Purpose: Instant filtering by status
- Time Complexity: O(1) for status-based filtering
- Use Case: Quickly show all "Pending" or "InProgress" requests
- Implementation: Dictionary maps each status to its list of requests

# Custom Linked List
Purpose: Store reported issues with dynamic sizing
- Implementation: Manual linked list with nodes
- Use Case: Issues reported in Part 1 are stored efficiently without fixed array size



# Technologies Used
- Language: C# (.NET 9.0)
- Framework: Windows Forms
- IDE:Visual Studio 2022
- Design Patterns:** Object-Oriented Programming (OOP)
- Data Structures:** BST, Heap, Graph, Hash Table, Linked List


# Getting Started

1. Clone the repository:

2. Launch Visual Studio.

Click File → Open → Project/Solution.

Browse to the project folder and select the .sln file.

3. Build the Project

Press Ctrl+Shift+B or go to Build → Build Solution.

Make sure the build succeeds with no errors.

4. Run the Application

Press F5 (Debug mode) or Ctrl+F5 (Run without debugging).

The Main Menu window will appear.



# How to Use

Main Menu (MainForm)

# “Report Issues” → opens the issue reporting form.

Report Issue (ReportIssueForm)

Enter Location (e.g., “Main Street”).

Select a Category (e.g., “Road Maintenance”).

Write a Description.

Optionally attach files (e.g., images or documents).

Progress bar tracks completion.

Click Submit Report.

Success

A confirmation message will show your unique issue ID.

The issue is stored in the application’s linked list memory.

# Local Events & Announcements

Click Local Events from the Main Menu.

Browse upcoming events.

Use Search Bar to find specific events.

Filter by category or search by keywords.

use mouse to expand column to see description easier 

# Service Request Status

Click Service Request Status from the Main Menu.

Search By ID in search bar 

Filter By Status using the dropdown menu 

View Next Priority, click next Priority to see urgent requests Select a request and click "View Dependencies (Graph)" to see:
   - What this request depends on
   - What requests are waiting on this one
   - All related requests (BFS traversal)
	
Progress Status, Select a request a click Progress Status to advance the Request by one e.g Pending will advance to InProgress

View Details, Double click Any Request to see Full Details


# Change Log 

# Part 3 - Service Request Status Tracker 
Added 

- Binary Search Tree for fast ID searches
- Min-Heap for priority based request handling
- Graph structure for dependecy tracking
- BFS traversal for finding related requests
- Hash table for O(1) status filtering
- Statistics dashboard
- Color-coded UI with SA theme
- Status history timeline
- Dependency visualization

# Developer
Jared Allen / 10271869

# Improved 
- Overall UI/UX enhancements
- Main menu now displays landing image of table mountain to reference it to my own municipality as im from Cape Town


# Notes
- Data Persistence: Issues and requests are stored in memory only. Data is reset when the application closes.
- Sample Data: The application includes pre-loaded sample service requests to demonstrate data structures.
- Dependencies: Some sample requests have dependencies to showcase the graph functionality.

