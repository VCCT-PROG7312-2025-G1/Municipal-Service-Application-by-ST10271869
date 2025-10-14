using System;
using System.Collections.Generic;

namespace Municipal_Service_Application
{

    public class Event
    {
        public string Title { get; set; }
        public string Catergory { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

     
        
        public Event(string title, string description, DateTime date, string catergory)
        {
            Title = title;
            Catergory = catergory;
            Date = date;
            Description = description;
        }

     
                // Returns a formatted string for displaying the event in UI controls
        public override string ToString()
        {
            return $"{Title} - {Catergory} - {Date.ToShortDateString()}";
        }
    }


                // Manages the collection of events with functionality for adding, searching, and retrieving events
    public class EventManager
    {
                // SortedDictionary keeps events in chronological order by date
                // Queue allows multiple events per date in the order they were added
        private SortedDictionary<DateTime, Queue<Event>> eventSchedule = new SortedDictionary<DateTime, Queue<Event>>();

                // HashSet stores unique category names for filtering and validation
        private HashSet<string> catergories = new HashSet<string>();

   
                // Adds a new event to the schedule and registers its category
        public void AddEvent(Event newEvent)
        {
                // Add category to the set (duplicates are automatically ignored)
            catergories.Add(newEvent.Catergory);

                // Check if this date already has events scheduled
            if (!eventSchedule.ContainsKey(newEvent.Date))
            {
                // Create a new queue for this date if it doesn't exist
                eventSchedule[newEvent.Date] = new Queue<Event>();
            }

                // Add the event to the queue for its date
                eventSchedule[newEvent.Date].Enqueue(newEvent);
        }

     
                // Searches for all events matching the specified category
        public IEnumerable<Event> SearchByCatergory(string catergory)
        {
                // Iterate through all dates
            foreach (var dateEvents in eventSchedule.Values)
            {
                // Check each event in the queue for that date
                foreach (var ev in dateEvents)
                {
                // Case-insensitive comparison for better user experience
                    if (ev.Catergory.Equals(catergory, StringComparison.OrdinalIgnoreCase))
                    {
                        yield return ev; // Return matching event without creating a full list
                    }
                }
            }
        }

     
                // Retrieves all events in chronological order
        public IEnumerable<Event> GetAllEvents()
        {
                // Iterate through dates in chronological order (SortedDictionary handles this)
            foreach (var dateEvents in eventSchedule)
            {
                // Return each event for the current date
                foreach (var ev in dateEvents.Value)
                    yield return ev; // Memory-efficient iteration
            }
        }

       
                // Returns the set of all unique categories currently registered 
        public HashSet<string> GetCatergories()
        {
            return catergories;
        }
    }
}

//_____________________________________________________________End of file____________________________________________________//



