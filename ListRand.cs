using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

namespace t
{
    public class ListRand : IEnumerable
    {
        ListNode head;
        ListNode tail;
        int count;

        public void Add(string data)
        {
            ListNode node = new ListNode(data);
 
            if (head == null)
                head = node;
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            count++;
            node.Random = Find(new Random().Next(0, count));
        }
        public void AddFirst(string data)
        {
            ListNode node = new ListNode(data);
            ListNode temp = head;
            node.Next = temp;
            head = node;
            if (count == 0)
                tail = head;
            else
                temp.Previous = node;
            count++;
            node.Random = Find(new Random().Next(0, count));
        }

        public bool Remove(string data)
        {
            ListNode current = head;
 
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    break;
                }
                current = current.Next;
            }
            if(current!=null)
            {
                if(current.Next!=null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    tail = current.Previous;
                }
 
                if(current.Previous!=null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    head = current.Next;
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
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }
 
        IEnumerator IEnumerable.GetEnumerator()
        {
            ListNode current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
 
        public IEnumerable BackEnumerator()
        {
            ListNode current = tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
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
                current = current.Next;
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
                current = current.Next;
                counter++;
            }
            return -1;
        }

        public void Serialize(FileStream s)
        {
            var cache = new Dictionary<ListNode, int>();
            var current = head;
            int counter = 0;
            while (current != null)
            {
                cache.Add(current, counter);
                current = current.Next;
                counter++;
            }

            string line = "" + cache.Count + "\n";

            byte[] buffer = new UTF8Encoding(true).GetBytes(line);
            s.Write(buffer, 0, buffer.Length);

            current = head;

            while (current != null)
            {
                line = current.Data + "<SEPARATOR>" + cache[current.Random];
                line += "\n";
                buffer = new UTF8Encoding(true).GetBytes(line);
                s.Write(buffer, 0, buffer.Length); 

                current = current.Next;
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
            tail = cache[c_count-1].node;
            for (int i = 0; i < c_count; i++)
            {
                cache[i].node.Previous = (i == 0 ? null : cache[i - 1].node);
                cache[i].node.Next = (i == c_count - 1 ? null : cache[i + 1].node);
                cache[i].node.Random = cache[cache[i].random_index].node;
            }
        }

        public void WriteList()
        {
            var current = head;
            while (current != null)
            {
                Console.WriteLine("ran " + IndexOf(current.Random) + "  " + current.Data);
                current = current.Next;
            }
        }
    }
}