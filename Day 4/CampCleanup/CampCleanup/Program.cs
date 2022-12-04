
PartOne();

PartTwo();

void PartOne()
{
    using var input = new StreamReader("input.txt");

    var totalCountOfContainedPairs = 0;
    while (!input.EndOfStream)
    {
        var line = input.ReadLine();

        var elfOne = line.Split(',')[0];
        var elfTwo = line.Split(',')[1];

        ElfWork elfOneWork = null;
        if (int.TryParse(elfOne.Split('-')[0], out int min) && int.TryParse(elfOne.Split('-')[1], out int max))
        {
            elfOneWork = new ElfWork(min, max);
        }

        ElfWork elfTwoWork = null;
        if (int.TryParse(elfTwo.Split('-')[0], out int min2) && int.TryParse(elfTwo.Split('-')[1], out int max2))
        {
            elfTwoWork = new ElfWork(min2, max2);
        }

        if (elfOneWork == null || elfTwoWork == null)
            throw new Exception("Parsing error");


        if(IsFullyContained(elfOneWork, elfTwoWork) || IsFullyContained(elfTwoWork, elfOneWork))
        {
            totalCountOfContainedPairs++;
        }
    }

    Console.WriteLine($"Total Contained Pairs: {totalCountOfContainedPairs}");
}

void PartTwo()
{
    using var input = new StreamReader("input.txt");

    var totalCountOfContainedPairs = 0;
    while (!input.EndOfStream)
    {
        var line = input.ReadLine();

        var elfOne = line.Split(',')[0];
        var elfTwo = line.Split(',')[1];

        ElfWork elfOneWork = null;
        if (int.TryParse(elfOne.Split('-')[0], out int min) && int.TryParse(elfOne.Split('-')[1], out int max))
        {
            elfOneWork = new ElfWork(min, max);
        }

        ElfWork elfTwoWork = null;
        if (int.TryParse(elfTwo.Split('-')[0], out int min2) && int.TryParse(elfTwo.Split('-')[1], out int max2))
        {
            elfTwoWork = new ElfWork(min2, max2);
        }

        if (elfOneWork == null || elfTwoWork == null)
            throw new Exception("Parsing error");


        if (IsPartiallyContained(elfOneWork, elfTwoWork) || IsPartiallyContained(elfTwoWork, elfOneWork))
        {
            totalCountOfContainedPairs++;
        }
    }

    Console.WriteLine($"Total Contained Pairs: {totalCountOfContainedPairs}");
}

bool IsPartiallyContained(ElfWork elfOne, ElfWork elfTwo)
{
    return elfOne.Start <= elfTwo.End && elfOne.End >= elfTwo.Start;
}

bool IsFullyContained(ElfWork elfOne, ElfWork elfTwo)
{
    return elfOne.End >= elfTwo.End && elfOne.Start <= elfTwo.Start;
}

public class ElfWork
{
    public ElfWork(int start, int end)
    {
        Start = start;
        End = end;
    }

    public int Start { get; }
    public int End { get; }
}
