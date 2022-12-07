
using NoSpaceLeftOnDevice;


using var input = new StreamReader("input.txt");

// Ignore first line since we're gonna start in the root
input.ReadLine();
var fileSystem = new FileSystem();

// Generate file system
while (!input.EndOfStream)
{
    var arg = input.ReadLine().Split(' ');

    if (arg[0] == "$" && arg[1] == "cd")
    {
        fileSystem.ChangeDirectory(arg[2]);
    }
    else if (arg[0] != "$")
    {
        // If it's not a command, we're just listing files. try to add them to the current directory

        if (arg[0] == "dir")
        {
            fileSystem.AddDirectoryToCurrentDirectory(arg[1]);
        }
        else
        {
            fileSystem.AddFileToCurrentDirectory(arg[1], int.Parse(arg[0]));
        }
    }
}

PartOne();

PartTwo();

void PartOne()
{
    var directories = FindAllDirectoriesWithSize(fileSystem, 100000);

    var sizeOf = directories.Sum(d => d.Size);

    Console.WriteLine(sizeOf);
}

void PartTwo()
{
    var dir = FindSmallestDirectoryToFreeUp(fileSystem, 70000000, 30000000);

    Console.WriteLine(dir.Size);
}

IEnumerable<NoSpaceLeftOnDevice.Directory> FindAllDirectoriesWithSize(FileSystem fileSystem, int maxSize)
{
    Queue<NoSpaceLeftOnDevice.Directory> bfsQueue = new Queue<NoSpaceLeftOnDevice.Directory>();
    List<NoSpaceLeftOnDevice.Directory> results = new List<NoSpaceLeftOnDevice.Directory>();

    bfsQueue.Enqueue(fileSystem.GetRoot());

    while (bfsQueue.TryDequeue(out var dir))
    {
        if (dir.Size <= maxSize)
        {
            results.Add(dir);
        }

        foreach (var directory in dir.Files.OfType<NoSpaceLeftOnDevice.Directory>())
            bfsQueue.Enqueue(directory);

    }

    return results;
}

NoSpaceLeftOnDevice.Directory FindSmallestDirectoryToFreeUp(FileSystem fileSystem, int totalSize, int neededSpace)
{
    Queue<NoSpaceLeftOnDevice.Directory> bfsQueue = new Queue<NoSpaceLeftOnDevice.Directory>();
    
    var root = fileSystem.GetRoot();
    NoSpaceLeftOnDevice.Directory result = root;

    var currentFreeSpace = totalSize - root.Size;
    var spaceNeededToMakeAvailable = neededSpace - currentFreeSpace;


    bfsQueue.Enqueue(root);

    while (bfsQueue.TryDequeue(out var dir))
    {
        if (dir.Size >= spaceNeededToMakeAvailable)
        {
            if (dir.Size < result.Size)
                result = dir;

            foreach (var directory in dir.Files.OfType<NoSpaceLeftOnDevice.Directory>())
                bfsQueue.Enqueue(directory);
        }
    }

    return result;
}
