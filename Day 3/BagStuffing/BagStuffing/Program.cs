
// Part 1
PartOne();

PartTwo();

void PartOne()
{
    using var input = new StreamReader("input.txt");

    int score = 0;
    while (!input.EndOfStream)
    {
        var line = input.ReadLine();

        var commonCharacter = FindCommonCharacterInBothHalves(line);

        score = score + CalculateItemScore(commonCharacter);
    }

    Console.WriteLine($"Score Part 1: {score}");
}

void PartTwo()
{
    using var input = new StreamReader("input.txt");

    int score = 0;
    while (!input.EndOfStream)
    {
        List<string> lines = new List<string>();

        for (int i = 0; i < 3; i++)
            lines.Add(input.ReadLine());

        HashSet<char>[] sets = lines.Select(s => new HashSet<char>(s)).ToArray();

        var commonChars = FindCommonCharacterInListOfStringSets(FindCommonCharacterInListOfStringSets(sets[0], sets[1]), sets[2]);

        var c = commonChars.First();

        score = score + CalculateItemScore(c);
    }

    Console.WriteLine($"Score Part 2: {score}");
}

int CalculateItemScore(char c)
{
    var nextScore = 0;
    if (Char.IsLower(c))
        nextScore = c - 'a' + 1;
    else
        nextScore = c - 'A' + 27;

    return nextScore;
}

char FindCommonCharacterInBothHalves(string text)
{
    var sizeOfEachItem = text.Length / 2;
    HashSet<char> setOfItemsInFirstHalf = new HashSet<char>();
    HashSet<char> setOfItemsInSecondHalf = new HashSet<char>();

    // Add each half to their own hashmaps
    for (int i = 0; i < sizeOfEachItem; i++)
    {
        var j = i + sizeOfEachItem;

        setOfItemsInFirstHalf.Add(text[i]);

        setOfItemsInSecondHalf.Add(text[j]);
    }

    // Try to combine hash sets, if one fails we've found our duplicate
    foreach (var c in setOfItemsInFirstHalf)
    {
        if (!setOfItemsInSecondHalf.Add(c))
            return c;
    }

    throw new Exception("No common characters, input error");
}

HashSet<char> FindCommonCharacterInListOfStringSets(HashSet<char> list1, HashSet<char> list2)
{
    HashSet<char> commonCharacters = new HashSet<char>();

    foreach (var c in list1)
    {
        if (!list2.Add(c))
            commonCharacters.Add(c);
    }
    return commonCharacters;
}