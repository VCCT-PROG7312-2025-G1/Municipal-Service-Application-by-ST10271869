//__________________________________________________________START OF FILE_______________________________________________________________\\

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// Enhanced data structures for ServiceRequest management

namespace Municipal_Service_Application
{
    public enum ServiceRequestStatus
    {
        // Submitted = Just Submitted, InProgress = Being Worked On,  Completed = Finished, Rejected = Denied
        Pending, InProgress, Resolved, Rejected
    }

    public enum Priority
    {
        Low = 1, Medium = 2, High = 3, Critical = 4
    }
    // Enhanced ServiceRequest class with status tracking and relationships
    public class ServiceRequest
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime DateReported { get; set; }
        public List<string> AttachedFiles { get; set; }
        public string Comment { get; set; }

        // New properties for status tracking
        public ServiceRequestStatus Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime? EstimatedCompletion { get; set; }
        public List<StatusUpdate> StatusHistory { get; set; }
        public List<int> RelatedRequestIds { get; set; } // For graph relationships

        // Constructor
        public ServiceRequest()
        {
            AttachedFiles = new List<string>();
            StatusHistory = new List<StatusUpdate>();
            RelatedRequestIds = new List<int>();
            Status = ServiceRequestStatus.Pending;
            Priority = Priority.Medium;
        }

        // Method to update status and log history
        public void UpdateStatus(ServiceRequestStatus newStatus, string notes = "")
        {
            Status = newStatus;
            StatusHistory.Add(new StatusUpdate
            {
                Status = newStatus,
                Timestamp = DateTime.Now,
                Notes = notes
            });
        }
    }
    // Class to represent status updates
    public class StatusUpdate
    {
        public ServiceRequestStatus Status { get; set; }
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; }
    }
    // Node for Binary Search Tree
    public class BSTNode
    {
        public ServiceRequest Request { get; set; }
        public BSTNode Left { get; set; }
        public BSTNode Right { get; set; }
        public BSTNode(ServiceRequest request)
        {
            Request = request;
            Left = null;
            Right = null;
        }
    }
    // Binary Search Tree based on Request ID
    public class ServiceRequestBST
    {
        private BSTNode root;
        public void Insert(ServiceRequest request)
        {
            root = InsertRecursive(root, request);
        }
        private BSTNode InsertRecursive(BSTNode node, ServiceRequest request)
        {
            if (node == null)
            {
                return new BSTNode(request);
            }
            if (request.Id < node.Request.Id)
            {
                node.Left = InsertRecursive(node.Left, request);
            }
            else if (request.Id > node.Request.Id)
            {
                node.Right = InsertRecursive(node.Right, request);
            }
            return node;
        }

        // Search by Request ID
        public ServiceRequest Search(int Id)
        {
            return SearchRecursive(root, Id);
        }

        private ServiceRequest SearchRecursive(BSTNode node, int id)
        {
            // Base cases
            if (node == null)
                return null;
            if (node.Request.Id == id)
                return node.Request;

            // Recursive search in appropriate subtree
            if (id < node.Request.Id)
                return SearchRecursive(node.Left, id);
            else
                return SearchRecursive(node.Right, id);
        }

        // In-order traversal to get all requests sorted by ID
        public List<ServiceRequest> GetAllSorted()
        {
            var result = new List<ServiceRequest>();
            InOrderTraversal(root, result);
            return result;
        }

        // Helper method for in-order traversal
        private void InOrderTraversal(BSTNode node, List<ServiceRequest> result)
        {
            if (node == null) return;
            InOrderTraversal(node.Left, result);
            result.Add(node.Request);
            InOrderTraversal(node.Right, result);
        }
    }

    // Max-Heap based on Priority
    public class ServiceRequestMinHeap
    {
        private List<ServiceRequest> heap;
        public int Count => heap.Count;
        public ServiceRequestMinHeap()
        {
            heap = new List<ServiceRequest>();
        }
        // Insert a new service request into the heap
        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            BubbleUp(heap.Count - 1);
        }
        // Extract the highest priority request
        public ServiceRequest ExtractMax()
        {
            if (heap.Count == 0)
                return null;

            var max = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
                BubbleDown(0);

            return max;
        }

        // Peek at the highest priority request without removing it
        public ServiceRequest Peek()
        {
            return heap.Count > 0 ? heap[0] : null;
        }


        // Helper methods for maintaining heap property
        public void BubbleUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[index].Priority <= heap[parentIndex].Priority)
                    break;
                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        // Helper method to bubble down the element at index
        private void BubbleDown(int index)
        {
            while (true)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int largest = index;

                if (leftChild < heap.Count && heap[leftChild].Priority > heap[largest].Priority)
                    largest = leftChild;

                if (rightChild < heap.Count && heap[rightChild].Priority > heap[largest].Priority)
                    largest = rightChild;

                if (largest == index)
                    break;

                Swap(index, largest);
                index = largest;
            }
        }

        // Helper method to swap two elements in the heap
        private void Swap(int i, int j)
        {
            var temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }


        // Returns all requests in the heap (not sorted)
        public List<ServiceRequest> GetAll()
        {
            return new List<ServiceRequest>(heap);
        }
    }

    // Graph representation of Service Requests and their dependencies
    public class ServiceRequestGraph
    {
        private Dictionary<int, List<int>> adjacencyList;

        public ServiceRequestGraph()
        {
            adjacencyList = new Dictionary<int, List<int>>();
        }

        // Add a new request node
        public void AddRequest(int requestId)
        {
            if (!adjacencyList.ContainsKey(requestId))
            {
                adjacencyList[requestId] = new List<int>();
            }
        }

        // Add a dependency from one request to another
        public void AddDependency(int fromRequestId, int toRequestId)
        {
            AddRequest(fromRequestId);
            AddRequest(toRequestId);
            if (!adjacencyList[fromRequestId].Contains(toRequestId))
                adjacencyList[fromRequestId].Add(toRequestId);
        }

       
        public List<int> GetDependentRequests(int requestId)
        {
            var dependents = new List<int>();

            // Check all nodes to see which ones have this request as a dependency
            foreach (var kvp in adjacencyList)
            {
                if (kvp.Value.Contains(requestId))
                {
                    dependents.Add(kvp.Key);
                }
            }

            return dependents;
        }

        public List<int> GetDependencies(int requestId)
        {
            if (!adjacencyList.ContainsKey(requestId))
                return new List<int>();

            return new List<int>(adjacencyList[requestId]);
        }


        // Breadth-First Search to find all reachable requests from a starting request
        public List<int> BFS(int startId)
        {
            var visited = new HashSet<int>();
            var queue = new Queue<int>();
            var result = new List<int>();

            queue.Enqueue(startId);
            visited.Add(startId);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                result.Add(current);

                if (adjacencyList.ContainsKey(current))
                {
                    foreach (var neighbor in adjacencyList[current])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            return result;
        }

        // Detect cycles in the graph using Depth-First Search
        public bool HasCycle()
        {
            var visited = new HashSet<int>();
            var recursionStack = new HashSet<int>();

            foreach (var node in adjacencyList.Keys)
            {
                if (HasCycleDFS(node, visited, recursionStack))
                    return true;
            }

            return false;
        }

        // Helper method for cycle detection
        private bool HasCycleDFS(int node, HashSet<int> visited, HashSet<int> recursionStack)
        {
            if (recursionStack.Contains(node))
                return true;
            if (visited.Contains(node))
                return false;

            visited.Add(node);
            recursionStack.Add(node);

            if (adjacencyList.ContainsKey(node))
            {
                foreach (var neighbor in adjacencyList[node])
                {
                    if (HasCycleDFS(neighbor, visited, recursionStack))
                        return true;
                }
            }

            recursionStack.Remove(node);
            return false;
        }
    }
}





//__________________________________________________________END OF FILE________________________________________________________________\\