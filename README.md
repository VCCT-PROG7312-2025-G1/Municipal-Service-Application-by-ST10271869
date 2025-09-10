 # Municipal Service Application

A Windows Forms desktop application for reporting and managing municipal issues.
Built with C# (.NET Framework) using object-oriented programming and a custom linked list data structure.

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

# Getting Started
Open in Visual Studio

Launch Visual Studio.

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

“Report Issues” → opens the issue reporting form.

Other buttons (“Local Events”, “Service Status”) are placeholders for future expansion.

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

# Note: Issues are not persisted after closing the app. Each run starts with a fresh list (Issue IDs restart at 1).
