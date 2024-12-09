using Common.DisplayClasses;

namespace ResourceReader.SUtils
{
    internal class Node<T> where T : IMachineData
    {
        public T Value { get; private set; }
        public Node<T>? Next { get; private set; }

        public Node(T value)
        {
            Value = value;
        }

        public void AddNext(Node<T> next)
        {
            Next = next;
        }
    }
}
