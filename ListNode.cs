namespace RandList
{
    public class ListNode
    {
        public ListNode prev;
        public ListNode next;
        public ListNode rand;
        public string data;

        public ListNode(string _data)
        {
            data = _data;
        }
    }
}