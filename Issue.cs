using System;

namespace Municipal_Service_Application
{
    public class Issue
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime DateReported { get; set; }
        public FileLinkedList AttachedFiles { get; set; } 

        public Issue()
        {
            AttachedFiles = new FileLinkedList();
            DateReported = DateTime.Now;
        }
    }
}

//__________________________________________________________END OF FILE________________________________________________________________\\