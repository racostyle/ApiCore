using Common.DisplayClasses;
using System.Collections;

namespace ResourceReader.SUtils
{
    internal class Linked<T> : IEnumerable<Node<T>>  where T : IMachineData
    {
        public Node<T>? Head { get; private set; }

        public void Add(T value)
        {
            if (Head == null)
                Head = new Node<T>(value);
            else
            {
                Node<T>? current = Head;
                while (true)
                {
                    if (current.Next == null)
                    {
                        current.AddNext(new Node<T>(value));
                        break;
                    }
                    else
                        current = current.Next;
                }
            }
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            Node<T>? current = Head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
