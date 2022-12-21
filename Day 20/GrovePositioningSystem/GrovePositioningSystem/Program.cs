// See https://aka.ms/new-console-template for more information
using System.Text;

using var input = new StreamReader("test.txt");
Dictionary<int, int> originalListPositions = new Dictionary<int, int>();
LinkedList<int> currentList = new LinkedList<int>();

MyLinkedList list = new MyLinkedList();

var index = 0;
while (!input.EndOfStream)
{
    var val = int.Parse(input.ReadLine());

    list.Add(val);

    originalListPositions.Add(index++, index);
    currentList.AddFirst(val);
}
Console.WriteLine(list.Print());

for (int i = 0; i < 10; i++)
{
    list.MoveNode(i % 6);
    Console.WriteLine(list.Print());
}

class MyNode
{
    public int Value { get; set; }

    public int OriginalPosition { get; set; }

    public int CurrentPosition { get { return Previous == null ? 0 : Previous.CurrentPosition + 1; } }

    public MyNode Previous { get; set; }

    public MyNode Next { get; set; }
}


class MyLinkedList
{
    public MyNode Head { get; private set; }

    public MyNode Tail { get; private set; }

    public MyLinkedList()
    {

    }

    public void Add(int value)
    {
        var newNode = new MyNode();
        newNode.Value = value;

        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            newNode.Previous = Tail;
            Tail = newNode;
            Tail.Next = null;
        }


        newNode.OriginalPosition = newNode.CurrentPosition;
    }

    public string Print()
    {
        StringBuilder sb = new StringBuilder();
        var node = Head;

        do
        {
            sb.Append(node.Value + ", ");
            node = node.Next;
        } while (node != null);

        return sb.ToString();
    }

    public void MoveNode(int originalPosition)
    {
        var searchNode = Head;

        while (searchNode.OriginalPosition != originalPosition)
            searchNode = searchNode.Next;

        // If positive
        if (searchNode.Value > 0)
        {
            int start = 0;
            while (start < searchNode.Value)
            {
                if (Tail == searchNode)
                {
                    Tail = searchNode.Previous;
                    Tail.Next = null;

                    searchNode.Next = Head;
                    searchNode.Previous = null;

                    searchNode.Next.Previous = searchNode;

                    Head = searchNode;
                }
                else if (Head == searchNode)
                {
                    // Repoint current
                    var next = searchNode.Next;
                    Head = next;
                    next.Previous = null;
                    searchNode.Previous = next;
                    searchNode.Next = next.Next;

                    // Repoint next
                    searchNode.Previous.Next = searchNode;
                    searchNode.Next.Previous = searchNode;
                }
                else if (searchNode.Next != null)
                {
                    // Repoint previous
                    searchNode.Previous.Next = searchNode.Next;
                    searchNode.Next.Previous = searchNode.Previous;

                    // Repoint current
                    var next = searchNode.Next;
                    searchNode.Previous = next;
                    searchNode.Next = next.Next;

                    // Repoint next
                    searchNode.Previous.Next = searchNode;
                    searchNode.Next.Previous = searchNode;
                }

                start++;
            }
        }
        else
        {
            // if negative
            int start = 0;
            while (start > searchNode.Value)
            {
                if (Tail == searchNode)
                {
                    
                    var previous = searchNode.Previous;
                    Tail = searchNode.Previous;

                    searchNode.Next = Tail;
                    searchNode.Previous = Tail.Previous;
                    searchNode.Previous.Next = searchNode;

                    Tail.Previous = searchNode;
                    Tail.Next = null;
                }
                else if (Head == searchNode)
                {
                    // Move before current tail
                    Head = searchNode.Next;

                    searchNode.Next = Tail;
                    searchNode.Previous = Tail.Previous;

                    Tail.Previous = searchNode;
                    searchNode.Previous.Next = searchNode;
                }
                else if (searchNode.Next != null)
                {
                    var previous = searchNode.Previous;
                    var previousNext = searchNode.Next;

                    searchNode.Previous.Next = searchNode.Next;
                    searchNode.Next.Previous = searchNode.Previous;

                    // Repoint current
                    var next = searchNode.Previous;
                    searchNode.Previous = next.Previous;
                    searchNode.Next = next;

                    // Repoint next
                    if(searchNode.Previous != null)
                        searchNode.Previous.Next = searchNode;
                    if(searchNode.Next != null)
                        searchNode.Next.Previous = searchNode;

                    // Repoint previous
                    if (previous == Head)
                    {
                        Head = searchNode;
                    }
                    if (previousNext == Tail)
                    {
                        Tail = searchNode;
                    }
                }

                start--;
            }
        }
    }
}