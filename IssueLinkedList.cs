using System;
using System.Collections;
using System.Collections.Generic;

namespace Municipal_Service_Application
{
    internal class IssueNode
    {
        public Issue Data { get; set; }
        public IssueNode Next { get; set; }
        public IssueNode(Issue data) { Data = data; Next = null; }
    }

    public class IssueLinkedList : IEnumerable<Issue>
    {
        private IssueNode head;
        private IssueNode tail;
        private int count;

        public int Count => count;

        public void Add(Issue issue)
        {
            if (issue == null) throw new ArgumentNullException(nameof(issue));
            var node = new IssueNode(issue);
            if (head == null) head = tail = node;
            else { tail.Next = node; tail = node; }
            count++;
        }

        public IEnumerator<Issue> GetEnumerator()
        {
            var current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
