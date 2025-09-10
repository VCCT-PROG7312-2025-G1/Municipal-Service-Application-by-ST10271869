using System;
using System.Collections;
using System.Collections.Generic;

namespace Municipal_Service_Application
{
    internal class FileNode
    {
        public string Path { get; set; }
        public FileNode Next { get; set; }
        public FileNode(string path) { Path = path; Next = null; }
    }

    public class FileLinkedList : IEnumerable<string>
    {
        private FileNode head;
        private FileNode tail;
        private int count;

        public int Count => count;

        public void Add(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            var node = new FileNode(path);
            if (head == null) head = tail = node;
            else { tail.Next = node; tail = node; }
            count++;
        }

        // Create a deep copy so you can safely transfer attachments between forms
        public FileLinkedList Clone()
        {
            var copy = new FileLinkedList();
            foreach (var p in this) copy.Add(p);
            return copy;
        }

        public IEnumerator<string> GetEnumerator()
        {
            var current = head;
            while (current != null)
            {
                yield return current.Path;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

//__________________________________________________________END OF FILE________________________________________________________________\\