List<List<char>> hill = new List<List<char>>();

using var input = new StreamReader("input.txt");

(int x, int y) start = (0, 0);

List<(int x, int y)> aPositions = new List<(int x, int y)>();

int rowNum = 0;
while (!input.EndOfStream)
{
    var row = input.ReadLine().ToList();

    var startIndex = row.IndexOf('S');
    if (startIndex != -1)
    {
        start = (startIndex, rowNum);
    }

    // Find all 'a' starts
    for(int i = 0;i<row.Count;i++)
    {
        if (row[i] == 'a')
            aPositions.Add((i, rowNum));
    }

    hill.Add(row);
    rowNum++;
}


var hillArray = hill.Select(a => a.ToArray()).ToArray();

int findShortest(char[][] hillArray, (int y, int x) starter)
{
    var visited = hill.Select(a => a.Select(h => false).ToArray()).ToArray();

    Queue<HillNode> bfsQueue = new Queue<HillNode>();

    visited[starter.y][starter.x] = true;
    bfsQueue.Enqueue(new HillNode(0, (starter.x, starter.y)));

    int count = 0;
    int numSteps = int.MaxValue;
    while (bfsQueue.TryDequeue(out var node))
    {
        count++;

        var x = node.Position.x;
        var y = node.Position.y;

        if (hillArray[y][x] == 'E')
        {
            numSteps = node.Length;
            break;
        }

        var currentValue = hillArray[y][x] == 'S' ? 'a' : hillArray[y][x];

        // Top
        if (y > 0 && !visited[y - 1][x] && ((hillArray[y - 1][x] != 'E' && hillArray[y - 1][x] - currentValue <= 1) || (hillArray[y - 1][x] == 'E' && currentValue == 'z')))
        {
            visited[y - 1][x] = true;

            bfsQueue.Enqueue(new HillNode(node.Length + 1, (x, y - 1)));
        }
        // Left
        if (x > 0 && !visited[y][x - 1] && ((hillArray[y][x - 1] != 'E' && hillArray[y][x - 1] - currentValue <= 1) || (hillArray[y][x - 1] == 'E' && currentValue == 'z')))
        {
            visited[y][x - 1] = true;

            bfsQueue.Enqueue(new HillNode(node.Length + 1, (x - 1, y)));
        }
        // Right
        if (x < hillArray[y].Length - 1 && !visited[y][x + 1] && ((hillArray[y][x + 1] != 'E' && hillArray[y][x + 1] - currentValue <= 1) || (hillArray[y][x + 1] == 'E' && currentValue == 'z')))
        {
            visited[y][x + 1] = true;

            bfsQueue.Enqueue(new HillNode(node.Length + 1, (x + 1, y)));
        }
        // Bottom
        if (y < hillArray.Length - 1 && !visited[y + 1][x] && ((hillArray[y + 1][x] != 'E' && hillArray[y + 1][x] - currentValue <= 1) || (hillArray[y + 1][x] == 'E' && currentValue == 'z')))
        {
            visited[y + 1][x] = true;
            bfsQueue.Enqueue(new HillNode(node.Length + 1, (x, y + 1)));
        }
    }

    return numSteps;
}

var finalMinStep = int.MaxValue;
foreach(var aStarter in aPositions)
{
    finalMinStep = Math.Min(finalMinStep, findShortest(hillArray, (aStarter.y, aStarter.x)));
}

Console.WriteLine(finalMinStep);


struct HillNode
{
    public int Length { get; set; }

    public (int x, int y) Position { get; set; }

    public HillNode(int length, (int x, int y) position)
    {
        Length = length;
        Position = position;
    }
}


