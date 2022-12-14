// See https://aka.ms/new-console-template for more information

using System.Reflection;

using var input = new StreamReader("input.txt");
List<List<(int x, int y)>> rockInputs = new List<List<(int x, int y)>>();

(int x, int y) startLocation = (500, 0);

// Generate lists of rocks
while (!input.EndOfStream)
{
    rockInputs.Add(input.ReadLine()
        .Split(" -> ")
        .Select(s => s.Split(","))
        .Select(s => (int.Parse(s[0]), int.Parse(s[1]))).ToList());
}

// Find largest y-value possible, to help create array
var yMax = rockInputs
    .Select(s => s.OrderByDescending(x => x.y).Select(x => x.y).First())
    .OrderDescending().First();

yMax+=3;

// Find largest x-value possible, to help create array
var xMax = rockInputs
    .Select(s => s.OrderByDescending(x => x.x).Select(x => x.x).First())
    .OrderDescending().First();

xMax+=200;

Console.WriteLine($"x: {xMax}, y: {yMax}");

char[,] world = new char[xMax, yMax];
for (int i = 0; i < world.GetLength(0); i++)
{
    for(int j = 0;j< world.GetLength(1);j++)
    {
        // Add Floor for Part 2
        if (j == world.GetLength(1) - 1)
        {
            world[i, j] = '#';
        }
        else
        {
            world[i, j] = '.';
        }
    }
}

foreach (var rocks in rockInputs)
{
    for (int i = 1; i < rocks.Count(); i++)
    {
        // Find out which way the rocks move
        var delta = (dx: rocks[i].x - rocks[i - 1].x, dy: rocks[i].y - rocks[i - 1].y);
        if (delta.dx != 0)
        {
            // draw rock right
            if (delta.dx > 0)
            {
                for (int j = 0; j <= delta.dx; j++)
                {
                    world[rocks[i -1].x + j,rocks[i - 1].y] = '#';
                }
            }
            else // draw rock left
            {
                for (int j = 0; j >= delta.dx; j--)
                {
                    world[rocks[i - 1].x + j,rocks[i - 1].y] = '#';
                }
            }
        }
        else
        {
            // draw rock up
            if (delta.dy < 0)
            {
                for (int j = 0; j >= delta.dy; j--)
                {
                    world[rocks[i - 1].x, rocks[i - 1].y + j] = '#';
                }
            }
            else
            {
                for (int j = 0; j <= delta.dy; j++)
                {
                    world[rocks[i - 1].x, rocks[i - 1].y + j] = '#';
                }
            }
            
        }
    }
}

(int x, int y)? FindNextLocationForSand((int x,int y) startLocation, char[,] zaWorld)
{
    if (startLocation.y == zaWorld.GetLength(1) - 1)
    {
        return null;
    }
    
    // Look down
    if (zaWorld[startLocation.x, startLocation.y + 1] == '.')
    {
        return FindNextLocationForSand((startLocation.x, startLocation.y + 1), zaWorld);
    }
    // down-left
    if (zaWorld[startLocation.x - 1, startLocation.y + 1] == '.')
    {
        return FindNextLocationForSand((startLocation.x - 1, startLocation.y + 1), zaWorld);
    }
    // down-right
    if (zaWorld[startLocation.x + 1, startLocation.y + 1] == '.')
    {
        return FindNextLocationForSand((startLocation.x + 1, startLocation.y + 1), zaWorld);
    }
    
    return (startLocation.x, startLocation.y);
}

// Play Game
int sandCount = 0;
while(true)
{
    
    var findSandPosition = FindNextLocationForSand(startLocation, world);

    if (!findSandPosition.HasValue)
        break;
    
    sandCount++;
    
    world[findSandPosition.Value.x, findSandPosition.Value.y] = 'O';
    
    if (findSandPosition.Value.x == startLocation.x && findSandPosition.Value.y == startLocation.y)
        break;
}

for (int i = 0;i<world.GetLength(1); i++)
{
    for(int j=474;j<world.GetLength(0) - 100;j++)
        Console.Write(world[j, i]);
    Console.WriteLine();
}

Console.WriteLine(sandCount);