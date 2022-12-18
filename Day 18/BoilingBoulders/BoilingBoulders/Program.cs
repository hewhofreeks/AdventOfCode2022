using var input = new StreamReader("input.txt");

HashSet<(int x, int y, int z)> drops = new HashSet<(int x, int y, int z)>();
HashSet<(int x, int y, int z)> surroundingAir = new HashSet<(int x, int y, int z)>();


while (!input.EndOfStream)
{

    var line = input.ReadLine();
    var drop = line.Split(',').Select(int.Parse).ToArray();

    // Add lava
    drops.Add((drop[0], drop[1], drop[2]));

    // Remove from surrounding air if we previously added it there
    surroundingAir.Remove((drop[0], drop[1], drop[2]));

    // Add 10x10x10 air around if not blocked

    for (int i = -5; i < 5; i++)
        for (int j = -5; j < 5; j++)
            for (int k = -5; k < 5; k++)
            {
                if (!drops.Contains((drop[0] + i, drop[1] + j, drop[2] + k)))
                    surroundingAir.Add((drop[0] + i, drop[1] + j, drop[2] + k));
            }

}


PartOne();

PartTwo();

void PartOne()
{
    int blockedSides = 0;
    foreach (var drop in drops)
    {
        // check x
        if (drops.Contains((drop.x + 1, drop.y, drop.z)))
            blockedSides++;
        if (drops.Contains((drop.x - 1, drop.y, drop.z)))
            blockedSides++;

        // check y
        if (drops.Contains((drop.x, drop.y + 1, drop.z)))
            blockedSides++;
        if (drops.Contains((drop.x, drop.y - 1, drop.z)))
            blockedSides++;

        // check z
        if (drops.Contains((drop.x, drop.y, drop.z + 1)))
            blockedSides++;
        if (drops.Contains((drop.x, drop.y, drop.z - 1)))
            blockedSides++;
    }

    var totalFreeSides = drops.Count * 6 - blockedSides;

    Console.WriteLine(totalFreeSides);
}

void PartTwo()
{
    HashSet<(int x, int y, int z)> airToTurnIntoLava = new HashSet<(int x, int y, int z)>();
    HashSet<(int x, int y, int z)> airThatisAir = new HashSet<(int x, int y, int z)>();

    // Search all air drops to see if they are contained within lava
    foreach (var airDrop in surroundingAir)
    {
        Stack<(int x, int y, int z)> searchQueue = new Stack<(int x, int y, int z)>();
        HashSet<(int x, int y, int z)> airInCurrentSearch = new HashSet<(int x, int y, int z)>();
        searchQueue.Push(airDrop);
        airInCurrentSearch.Add(airDrop);

        bool encasedInLava = true;

        while (searchQueue.TryPop(out var dropToSearch))
        {
            if(airThatisAir.Contains(dropToSearch))
            {
                encasedInLava = false;
                break;
            }

            // check x
            if (surroundingAir.Contains((dropToSearch.x + 1, dropToSearch.y, dropToSearch.z)))
            {
                if(airInCurrentSearch.Add((dropToSearch.x + 1, dropToSearch.y, dropToSearch.z)))
                    searchQueue.Push((dropToSearch.x + 1, dropToSearch.y, dropToSearch.z));
            }
            else if (!drops.Contains((dropToSearch.x + 1, dropToSearch.y, dropToSearch.z)))
            {
                encasedInLava = false;
                break;
            }

            if (surroundingAir.Contains((dropToSearch.x - 1, dropToSearch.y, dropToSearch.z)))
            {
                if (airInCurrentSearch.Add((dropToSearch.x - 1, dropToSearch.y, dropToSearch.z)))
                    searchQueue.Push((dropToSearch.x - 1, dropToSearch.y, dropToSearch.z));
            }
            else if (!drops.Contains((dropToSearch.x - 1, dropToSearch.y, dropToSearch.z)))
            {
                encasedInLava = false;
                break;
            }

            // check y
            if (surroundingAir.Contains((dropToSearch.x, dropToSearch.y + 1, dropToSearch.z)))
            {
                if (airInCurrentSearch.Add((dropToSearch.x, dropToSearch.y + 1, dropToSearch.z)))
                    searchQueue.Push((dropToSearch.x, dropToSearch.y + 1, dropToSearch.z));
            }
            else if (!drops.Contains((dropToSearch.x, dropToSearch.y + 1, dropToSearch.z)))
            {
                encasedInLava = false;
                break;
            }

            if (surroundingAir.Contains((dropToSearch.x, dropToSearch.y - 1, dropToSearch.z)))
            {
                if (airInCurrentSearch.Add((dropToSearch.x, dropToSearch.y - 1, dropToSearch.z)))
                    searchQueue.Push((dropToSearch.x, dropToSearch.y - 1, dropToSearch.z));
            }
            else if (!drops.Contains((dropToSearch.x, dropToSearch.y - 1, dropToSearch.z)))
            {
                encasedInLava = false;
                break;
            }

            // check z
            if (surroundingAir.Contains((dropToSearch.x, dropToSearch.y, dropToSearch.z + 1)))
            {
                if (airInCurrentSearch.Add((dropToSearch.x, dropToSearch.y, dropToSearch.z + 1)))
                    searchQueue.Push((dropToSearch.x, dropToSearch.y, dropToSearch.z + 1));
            }
            else if (!drops.Contains((dropToSearch.x, dropToSearch.y, dropToSearch.z + 1)))
            {
                encasedInLava = false;
                break;
            }

            if (surroundingAir.Contains((dropToSearch.x, dropToSearch.y, dropToSearch.z - 1)))
            {
                if (airInCurrentSearch.Add((dropToSearch.x, dropToSearch.y, dropToSearch.z - 1)))
                    searchQueue.Push((dropToSearch.x, dropToSearch.y, dropToSearch.z - 1));
            }
            else if (!drops.Contains((dropToSearch.x, dropToSearch.y, dropToSearch.z - 1)))
            {
                encasedInLava = false;
                break;
            }
        }

        if (encasedInLava)
        {
            foreach (var airDropToTransform in airInCurrentSearch)
            {
                airToTurnIntoLava.Add(airDropToTransform);
            }
        }
        else
        {
            foreach (var airDropToTransform in airInCurrentSearch)
            {
                airThatisAir.Add(airDropToTransform);
            }
        }

    }

    foreach (var airDropToTransform in airToTurnIntoLava)
    {
        // Remove from air
        surroundingAir.Remove(airDropToTransform);

        // Turn into lava for calculation
        drops.Add(airDropToTransform);
    }

    int blockedSides = 0;
    foreach (var drop in drops)
    {
        // check x
        if (drops.Contains((drop.x + 1, drop.y, drop.z)))
            blockedSides++;
        if (drops.Contains((drop.x - 1, drop.y, drop.z)))
            blockedSides++;

        // check y
        if (drops.Contains((drop.x, drop.y + 1, drop.z)))
            blockedSides++;
        if (drops.Contains((drop.x, drop.y - 1, drop.z)))
            blockedSides++;

        // check z
        if (drops.Contains((drop.x, drop.y, drop.z + 1)))
            blockedSides++;
        if (drops.Contains((drop.x, drop.y, drop.z - 1)))
            blockedSides++;
    }

    var totalFreeSides = drops.Count * 6 - blockedSides;

    Console.WriteLine(totalFreeSides);
}

