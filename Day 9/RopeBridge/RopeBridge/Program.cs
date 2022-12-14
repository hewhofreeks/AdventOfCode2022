//PartOne();

PartTwo();

void PartOne()
{
    using var input = new StreamReader("input.txt");

    var rope = new Rope(2);
    var history = new HashSet<(int, int)>();
    history.Add(rope.Tail);

    while (!input.EndOfStream)
    {
        var instruction = input.ReadLine().Split(' ');
        int.TryParse(instruction[1], out var count);
        for (int i = 0; i < count; i++)
        {
            rope.MoveHead(instruction[0][0]);
            history.Add(rope.Tail);
        }
    }

    Console.WriteLine(history.Count());
}

void PartTwo()
{
    using var input = new StreamReader("input.txt");

    var rope = new Rope(10);
    var history = new HashSet<(int, int)>();
    history.Add(rope.Tail);

    while (!input.EndOfStream)
    {
        var instruction = input.ReadLine().Split(' ');
        int.TryParse(instruction[1], out var count);
        for (int i = 0; i < count; i++)
        {
            rope.MoveHead(instruction[0][0]);
            if (history.Add(rope.Tail))
                Console.WriteLine();

        }
    }

    Console.WriteLine(history.Count());
}

struct Knot
{
    public Knot(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

}

class Rope
{
    private LinkedList<Knot> Knots = new LinkedList<Knot>();

    public Rope(int numKnots)
    {
        for (int i = 0; i < numKnots; i++)
        {
            Knots.AddFirst(new Knot(0, 0));
        }
    }

    public (int x, int y) Tail => (Knots.Last.Value.X, Knots.Last.Value.Y);

    public (int x, int y) GetDelta(Knot head, Knot tail)
    {
        return (head.X - tail.X, head.Y - tail.Y);
    }

    public void MoveHead(char direction)
    {
        LinkedListNode<Knot> knotToMove = null;

        var head = knotToMove = Knots.First;
        var previousHeadPosition = head.Value;

        // Move head
        switch (direction)
        {
            case 'U':
                head.ValueRef.X = previousHeadPosition.X;
                head.ValueRef.Y = previousHeadPosition.Y - 1;
                break;
            case 'D':
                head.ValueRef.X = previousHeadPosition.X;
                head.ValueRef.Y = previousHeadPosition.Y + 1;
                break;
            case 'L':
                head.ValueRef.X = previousHeadPosition.X - 1;
                head.ValueRef.Y = previousHeadPosition.Y;
                break;
            case 'R':
                head.ValueRef.X = previousHeadPosition.X + 1;
                head.ValueRef.Y = previousHeadPosition.Y;
                break;
        }

        do
        {
            knotToMove = knotToMove.Next;

            var delta = GetDelta(knotToMove.Value, knotToMove.Previous.Value);

            var oldNode = knotToMove.Value;
            if (Math.Abs(delta.x) > Math.Abs(delta.y) && Math.Abs(delta.x) > 1)
            {
                knotToMove.ValueRef.X = knotToMove.Value.X + (delta.x > 0 ? -1 : 1);
                knotToMove.ValueRef.Y = knotToMove.Previous.Value.Y;
            }
            else if (Math.Abs(delta.y) > Math.Abs(delta.x) && Math.Abs(delta.y) > 1)
            {
                knotToMove.ValueRef.X = knotToMove.Previous.Value.X;
                knotToMove.ValueRef.Y = knotToMove.Value.Y + (delta.y > 0 ? -1 : 1);
            }
            else if (Math.Abs(delta.y) == Math.Abs(delta.x) && Math.Abs(delta.x) == 2)
            {
                knotToMove.ValueRef.X = knotToMove.Value.X + (delta.x > 0 ? -1 : 1);
                knotToMove.ValueRef.Y = knotToMove.Value.Y + (delta.y > 0 ? -1 : 1);
            }
            previousHeadPosition = oldNode;

        } while (knotToMove.Next != null);

    }
}