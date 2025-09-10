using System;
using System.Collections.Generic;

namespace Municipal_Service_Application
{
    public class Issue
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime DateReported { get; set; }
        public List<string> AttachedFiles { get; set; }

        public Issue()
        {
            AttachedFiles = new List<string>();
            DateReported = DateTime.Now;
        }
    }
}
