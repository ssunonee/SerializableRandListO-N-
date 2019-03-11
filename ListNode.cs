using System;

namespace t
{
    public class ListNode
    {
        public ListNode Previous;
        public ListNode Next;
        public ListNode Random;
        public string Data;

        public ListNode(string _Data)
        {
            Data = _Data;
        }
    }
}