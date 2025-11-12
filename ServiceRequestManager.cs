using System;
using System.Collections.Generic;
using System.Linq;

namespace Municipal_Service_Application
{

    // Central manager that coordinates all data structures for service request tracking
    // INTEGRATES: BST for searching, Heap for priority, Graph for dependencies
    // WHY MULTIPLE STRUCTURES: Each optimized for different operations
    public class ServiceRequestManager
    {
        // BST for fast ID-based lookups - O(log n)
        private ServiceRequestBST requestBST;

        // Min-Heap for priority-based processing - O(1) to get highest priority
        private ServiceRequestMinHeap priorityQueue;

        // Graph for tracking dependencies between requests
        private ServiceRequestGraph dependencyGraph;

        // Hash table for O(1) status lookups
        private Dictionary<ServiceRequestStatus, List<ServiceRequest>> statusIndex;

        // Keep track of all requests for full iteration
        private List<ServiceRequest> allRequests;

        public ServiceRequestManager()
        {
            requestBST = new ServiceRequestBST();
            priorityQueue = new ServiceRequestMinHeap();
            dependencyGraph = new ServiceRequestGraph();
            statusIndex = new Dictionary<ServiceRequestStatus, List<ServiceRequest>>();
            allRequests = new List<ServiceRequest>();

            // Initialize status index with all status types
            foreach (ServiceRequestStatus status in Enum.GetValues(typeof(ServiceRequestStatus)))
            {
                statusIndex[status] = new List<ServiceRequest>();
            }
        }


        // Adds a new service request to all data structures
        // WHY ADD TO MULTIPLE: Each structure serves different query needs
        // - BST: For ID-based search
        // - Heap: For priority-based processing
        // - Graph: For dependency tracking
        // - StatusIndex: For status-based filtering

        public void AddRequest(ServiceRequest request)
        {
            // Add to all data structures for different access patterns
            requestBST.Insert(request);
            priorityQueue.Insert(request);
            dependencyGraph.AddRequest(request.Id);
            statusIndex[request.Status].Add(request);
            allRequests.Add(request);
        }


        // Creates from existing Issue for backward compatibility
        // Converts old Issue objects to new ServiceRequest objects
        public void AddFromIssue(Issue issue, Priority priority = Priority.Medium)
        {
            var request = new ServiceRequest
            {
                Id = issue.Id,
                Location = issue.Location,
                Category = issue.Category,
                Description = issue.Description,
                DateReported = issue.DateReported,
                AttachedFiles = new List<string>(issue.AttachedFiles),
                Status = ServiceRequestStatus.Pending,
                Priority = priority,
                EstimatedCompletion = CalculateEstimatedCompletion(priority)
            };

            request.UpdateStatus(ServiceRequestStatus.Pending, "Request submitted");
            AddRequest(request);
        }

        // Searches for a request by ID using BST
        // Time Complexity: O(log n) - MUCH faster than linear search
        // DEMONSTRATION: Shows BST efficiency for lookups
        public ServiceRequest SearchById(int id)
        {
            return requestBST.Search(id);
        }


        // Gets requests filtered by status using hash table index
        // Time Complexity: O(1) lookup + O(k) where k = number of matching requests
        // DEMONSTRATION: Shows hash table efficiency for filtering

        public List<ServiceRequest> GetByStatus(ServiceRequestStatus status)
        {
            return new List<ServiceRequest>(statusIndex[status]);
        }


        // Gets the highest priority pending request
        // Time Complexity: O(1) - Shows heap efficiency
        // USE CASE: Municipal worker gets next most urgent task
        public ServiceRequest GetNextHighestPriority()
        {
            return priorityQueue.Peek();
        }


        // Updates a request's status and maintains all data structures
        // IMPORTANT: Must update all indices to keep data consistent
        public void UpdateRequestStatus(int id, ServiceRequestStatus newStatus, string comment = "")
        {
            var request = SearchById(id);
            if (request == null) return;

            // Remove from old status index
            statusIndex[request.Status].Remove(request);

            // Update the request
            request.UpdateStatus(newStatus, comment);

            // Add to new status index
            statusIndex[newStatus].Add(request);

            // Auto-calculate estimated completion for in-progress items
            if (newStatus == ServiceRequestStatus.InProgress && !request.EstimatedCompletion.HasValue)
            {
                request.EstimatedCompletion = CalculateEstimatedCompletion(request.Priority);
            }
        }


        // Creates a dependency between two requests using graph
        // EXAMPLE: Request 5 (streetlight) depends on Request 3 (electrical repair)
        // USE CASE: Ensure prerequisite requests are completed first
        public void AddDependency(int requestId, int dependsOnId)
        {
            dependencyGraph.AddDependency(requestId, dependsOnId);
        }


        // Gets all requests that depend on a given request using graph traversal
        // USE CASE: When completing a request, see what can now proceed
        // Time Complexity: O(V + E) using BFS
        public List<ServiceRequest> GetDependentRequests(int requestId)
        {
            var dependentIds = dependencyGraph.GetDependentRequests(requestId);
            var result = new List<ServiceRequest>();

            foreach (var id in dependentIds)
            {
                var request = SearchById(id);
                if (request != null)
                    result.Add(request);
            }

            return result;
        }

        public List<int> GetDependencies(int requestId)
        {
            return dependencyGraph.GetDependencies(requestId);
        }


        // Finds all related requests using graph BFS traversal
        // DEMONSTRATION: Shows graph traversal for finding connected components
        // USE CASE: Impact analysis - see entire chain of related requests
        public List<ServiceRequest> GetRelatedRequests(int requestId)
        {
            var relatedIds = dependencyGraph.BFS(requestId);
            var result = new List<ServiceRequest>();

            foreach (var id in relatedIds)
            {
                var request = SearchById(id);
                if (request != null && request.Id != requestId)
                    result.Add(request);
            }

            return result;
        }

        // Gets all requests sorted by ID using BST in-order traversal
        // Time Complexity: O(n)
        // DEMONSTRATION: Shows BST property of sorted output
        public List<ServiceRequest> GetAllSortedById()
        {
            return requestBST.GetAllSorted();
        }

  
        // Gets all requests (unsorted)
        public List<ServiceRequest> GetAll()
        {
            return new List<ServiceRequest>(allRequests);
        }


        // Searches requests by location or description
        // Uses linear search as this is text-based (no specific structure advantage)
        public List<ServiceRequest> SearchByText(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAll();

            searchTerm = searchTerm.ToLower();
            return allRequests.Where(r =>
                r.Location.ToLower().Contains(searchTerm) ||
                r.Description.ToLower().Contains(searchTerm) ||
                r.Category.ToLower().Contains(searchTerm)
            ).ToList();
        }


        // Gets statistics about service requests
        // Useful for dashboard display
        public ServiceRequestStatistics GetStatistics()
        {
            return new ServiceRequestStatistics
            {
                TotalRequests = allRequests.Count,
                PendingCount = statusIndex[ServiceRequestStatus.Pending].Count,
                InProgressCount = statusIndex[ServiceRequestStatus.InProgress].Count,
                ResolvedCount = statusIndex[ServiceRequestStatus.Resolved].Count,
                RejectedCount = statusIndex[ServiceRequestStatus.Rejected].Count,
                HighPriorityCount = allRequests.Count(r => r.Priority == Priority.High || r.Priority == Priority.Critical),
                AverageResolutionTime = CalculateAverageResolutionTime()
            };
        }

  
        // Calculates estimated completion date based on priority
        // Higher priority = sooner completion
        private DateTime CalculateEstimatedCompletion(Priority priority)
        {
            int daysToAdd;
            switch (priority)
            {
                case Priority.Critical:
                    daysToAdd = 1; // 1 day
                    break;
                case Priority.High:
                    daysToAdd = 3; // 3 days
                    break;
                case Priority.Medium:
                    daysToAdd = 7; // 1 week
                    break;
                case Priority.Low:
                    daysToAdd = 14; // 2 weeks
                    break;
                default:
                    daysToAdd = 7;
                    break;
            }

            return DateTime.Now.AddDays(daysToAdd);
        }


        // Calculates average time to resolve requests
        private double CalculateAverageResolutionTime()
        {
            var resolved = statusIndex[ServiceRequestStatus.Resolved];
            if (resolved.Count == 0) return 0;

            var totalDays = resolved.Sum(r =>
            {
                var lastUpdate = r.StatusHistory.LastOrDefault();
                if (lastUpdate != null)
                    return (lastUpdate.Timestamp - r.DateReported).TotalDays;
                return 0;
            });

            return totalDays / resolved.Count;
        }
    }

    // Statistics class for dashboard display
    public class ServiceRequestStatistics
    {
        public int TotalRequests { get; set; }
        public int PendingCount { get; set; }
        public int InProgressCount { get; set; }
        public int ResolvedCount { get; set; }
        public int RejectedCount { get; set; }
        public int HighPriorityCount { get; set; }
        public double AverageResolutionTime { get; set; }
    }
}

//__________________________________________________________END OF FILE________________________________________________________________\\