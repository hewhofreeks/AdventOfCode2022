
PartOne();

PartTwo();

void PartOne()
{
    using var input = new StreamReader("input.txt");

    int totalCount = 0;
    Queue<char> buffer = new Queue<char>();

    while (!input.EndOfStream)
    {
        var nextChar = (char)input.Read();
        totalCount++;

        buffer.Enqueue(nextChar);

        if (buffer.Count > 4)
            buffer.Dequeue();

        if(new HashSet<char>(buffer).Count == 4)
        {
            break;
        }
    }

    Console.WriteLine(totalCount);
}

void PartTwo()
{
    using var input = new StreamReader("input.txt");

    int totalCount = 0;
    Queue<char> buffer = new Queue<char>();

    while (!input.EndOfStream)
    {
        var nextChar = (char)input.Read();
        totalCount++;

        buffer.Enqueue(nextChar);

        if (buffer.Count > 14)
            buffer.Dequeue();

        if (new HashSet<char>(buffer).Count == 14)
        {
            break;
        }
    }

    Console.WriteLine(totalCount);
}