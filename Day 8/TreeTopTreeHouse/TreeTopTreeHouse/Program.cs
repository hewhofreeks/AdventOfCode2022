using System.Globalization;

using var input = new StreamReader("input.txt");

List<IEnumerable<int>> forestInput = new List<IEnumerable<int>>();

// Create array of ints based on forest

while (!input.EndOfStream)
{
    var intLine = new List<int>();
    var inputLine = input.ReadLine().Select(i => (int)(i - '0'));
    forestInput.Add(inputLine);
}

var forestArray = forestInput
    .Select(s => s.ToArray())
    .ToArray();

//PartOne();

PartTwo();

void PartOne()
{
    bool[,] visibleTrees = new bool[forestArray.Length, forestArray[0].Length];

    int[] currentMaxTops = new int[forestArray.Length];
    // Analyze top
    for (int topPointer = 0; topPointer < forestArray.Length; topPointer++)
    {
        for (int j = 0; j < forestArray[topPointer].Length; j++)
        {
            // All zero index elements can be seen
            if (topPointer == 0)
            {
                visibleTrees[topPointer, j] = true;
                currentMaxTops[j] = forestArray[topPointer][j];
                continue;
            }

            // If the previous one was visible and this one is larger, you can see it
            if (currentMaxTops[j] < forestArray[topPointer][j])
            {
                visibleTrees[topPointer, j] = true;
                currentMaxTops[j] = forestArray[topPointer][j];
            }
        }
    }


    int[] currentMaxBottoms = new int[forestArray.Length];
    // Analyze bottom
    for (int bottomPointer = forestArray.Length - 1; bottomPointer >= 0; bottomPointer--)
    {
        for (int j = 0; j < forestArray[bottomPointer].Length; j++)
        {
            // All zero index elements can be seen
            if (bottomPointer == forestArray.Length - 1)
            {
                visibleTrees[bottomPointer, j] = true;
                currentMaxBottoms[j] = forestArray[bottomPointer][j];

                continue;
            }

            // If the previous one was visible and this one is larger, you can see it
            if (currentMaxBottoms[j] < forestArray[bottomPointer][j])
            {
                visibleTrees[bottomPointer, j] = true;
                currentMaxBottoms[j] = forestArray[bottomPointer][j];
            }
        }
    }
    int currentMax = 0;
    // Analyze Left
    for (int i = 0; i < forestArray.Length; i++)
    {
        for (int leftPointer = 0; leftPointer < forestArray[i].Length; leftPointer++)
        {
            // All zero index elements can be seen
            if (leftPointer == 0)
            {
                visibleTrees[i, leftPointer] = true;
                currentMax = forestArray[i][leftPointer];

                continue;
            }

            // If the previous one was visible and this one is larger, you can see it
            if (currentMax < forestArray[i][leftPointer])
            {
                visibleTrees[i, leftPointer] = true;
                currentMax = forestArray[i][leftPointer];
            }
        }
    }

    // Analyze Right
    for (int i = 0; i < forestArray.Length; i++)
    {
        for (int rightPointer = forestArray[i].Length - 1; rightPointer >= 0; rightPointer--)
        {
            // All zero index elements can be seen
            if (rightPointer == forestArray[i].Length - 1)
            {
                visibleTrees[i, rightPointer] = true;
                currentMax = forestArray[i][rightPointer];

                continue;
            }

            // If the previous one was visible and this one is larger, you can see it
            if (currentMax < forestArray[i][rightPointer])
            {
                visibleTrees[i, rightPointer] = true;
                currentMax = forestArray[i][rightPointer];

            }
        }
    }

    //PrintTreeArray(visibleTrees);

    Console.WriteLine(CountTreesSeen(visibleTrees));
}

void PartTwo()
{
    var maxScore = 0;
    var elements = "";
    for(int i = 0; i< forestArray.Length; i++)
    {
        for(int j = 0; j< forestArray[i].Length; j++)
        {
            var score = CalculatLeftScene(forestArray, i, j) * CalculatRightScene(forestArray, i, j) * CalculateBottomScene(forestArray, i, j) * CalculateTopScene(forestArray, i, j);
            if (score > maxScore)
            {
                maxScore = score;
                elements = $"[{i},{j}]";
            }
        }
    }

    Console.WriteLine($"Score: {maxScore}. Elements: {elements}");
}

int CalculateTopScene(int[][] array, int x, int y)
{
    int scene = 1;
    int houseX = x;
    int houseY = y;
    y--;

    while(y > 0 && array[x][y] < array[houseX][houseY])
    {
        scene++;
        y--;
    }
    return scene;
}
int CalculateBottomScene(int[][] array, int x, int y)
{
    int scene = 1;
    int houseX = x;
    int houseY = y;
    y++;
    while (y < array.Length - 1 && array[x][y] < array[houseX][houseY])
    {
        scene++;
        y++;
    }
    return scene;
}
int CalculatLeftScene(int[][] array, int x, int y)
{
    int scene = 1;
    int houseX = x;
    int houseY = y;
    x--;
    while (x > 0 && array[x][y] < array[houseX][houseY])
    {
        scene++;
        x--;
    }
    return scene;
}
int CalculatRightScene(int[][] array, int x, int y)
{
    int scene = 1;
    int houseX = x;
    int houseY = y;
    x++;
    while (x < array.Length - 1 && array[x][y] < array[houseX][houseY])
    {
        scene++;
        x++;
    }
    return scene;
}

int CountTreesSeen(bool[,] trees)
{
    int count = 0;
    for (int i = 0; i < trees.GetLength(0); i++)
    {

        for (int j = 0; j < trees.GetLength(1); j++)
        {
            if (trees[i, j])
                count++;
        }

    }
    return count;
}

void PrintTreeArray(bool[,] trees)
{
    for (int i = 0; i < trees.GetLength(0); i++)
    {

        for (int j = 0; j < trees.GetLength(1); j++)
        {
            Console.Write(trees[i, j] ? "1" : "0");
        }

        Console.WriteLine();
    }
}
