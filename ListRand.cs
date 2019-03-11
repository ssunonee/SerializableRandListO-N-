using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

namespace RandList
{
    public class ListRand : IEnumerable
    {
        ListNode head;
        ListNode tail;
        int count;

        public void Serialize(FileStream s)
        {
            var cache = new Dictionary<ListNode, int>();
            var current = head;
            int counter = 0;
            while (current != null)
            {
                cache.Add(current, counter);
                current = current.next;
                counter++;
            }

            string line = "" + cache.Count + "\n";

            byte[] buffer = new UTF8Encoding(true).GetBytes(line);
            s.Write(buffer, 0, buffer.Length);

            current = head;

            while (current != null)
            {
                line = current.data + "<SEPARATOR>" + cache[current.rand];
                line += "\n";
                buffer = new UTF8Encoding(true).GetBytes(line);
                s.Write(buffer, 0, buffer.Length);

                current = current.next;
            }
        }

        public void Deserialize(FileStream s)
        {
            StreamReader sr = new StreamReader(s);
            int c_count = int.Parse(sr.ReadLine().Trim());

            var cache = new NodeModel[c_count];

            for (int i = 0; i < c_count; i++)
            {
                string[] res = sr.ReadLine().Trim().Split("<SEPARATOR>");
                cache[i] = new NodeModel
                {
                    node = new ListNode(res[0]),
                    random_index = int.Parse(res[1])
                };
            }

            head = cache[0].node;
            tail = cache[c_count - 1].node;
            for (int i = 0; i < c_count; i++)
            {
                cache[i].node.prev = (i == 0 ? null : cache[i - 1].node);
                cache[i].node.next = (i == c_count - 1 ? null : cache[i + 1].node);
                cache[i].node.rand = cache[cache[i].random_index].node;
            }
        }

        public void WriteList()
        {
            var current = head;
            while (current != null)
            {
                Console.WriteLine("ran " + IndexOf(current.rand) + "  " + current.data);
                current = current.next;
            }
        }


        public void Add(string data)
        {
            ListNode node = new ListNode(data);
 
            if (head == null)
                head = node;
            else
            {
                tail.next = node;
                node.prev = tail;
            }
            tail = node;
            count++;
            node.rand = Find(new Random().Next(0, count));
        }
        public void AddFirst(string data)
        {
            ListNode node = new ListNode(data);
            ListNode temp = head;
            node.next = temp;
            head = node;
            if (count == 0)
                tail = head;
            else
                temp.prev = node;
            count++;
            node.rand = Find(new Random().Next(0, count));
        }

        public bool Remove(string data)
        {
            ListNode current = head;
 
            while (current != null)
            {
                if (current.data.Equals(data))
                {
                    break;
                }
                current = current.next;
            }
            if(current!=null)
            {
                if(current.next!=null)
                {
                    current.next.prev = current.prev;
                }
                else
                {
                    tail = current.prev;
                }
 
                if(current.prev!=null)
                {
                    current.prev.next = current.next;
                }
                else
                {
                    head = current.next;
                }
                count--;
                return true;
            }
            return false;
        }
 
        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
 
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
 
        public bool Contains(string data)
        {
            ListNode current = head;
            while (current != null)
            {
                if (current.data.Equals(data))
                    return true;
                current = current.next;
            }
            return false;
        }
 
        IEnumerator IEnumerable.GetEnumerator()
        {
            ListNode current = head;
            while (current != null)
            {
                yield return current.data;
                current = current.next;
            }
        }
 
        public IEnumerable BackEnumerator()
        {
            ListNode current = tail;
            while (current != null)
            {
                yield return current.data;
                current = current.prev;
            }
        }

        public ListNode Find(int id)
        {
            var current = head;
            int counter = 0;
            while (current != null)
            {
                if (counter == id)
                {
                    return current;
                }
                current = current.next;
                counter++;
            }
            throw new IndexOutOfRangeException();
        }

        public int IndexOf(ListNode node)
        {
            var current = head;
            int counter = 0;
            while (current != null)
            {
                if (current == node)
                {
                    return counter;
                }
                current = current.next;
                counter++;
            }
            return -1;
        }
    }
}