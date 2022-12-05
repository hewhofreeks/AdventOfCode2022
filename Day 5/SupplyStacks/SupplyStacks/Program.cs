
using SupplyStacks;
using System.Text;
using System.Text.RegularExpressions;

PartOne();

PartTwo();

void PartOne()
{
    ParseInputAndMoveCrates(new CrateMover9000());
}

void PartTwo()
{
    ParseInputAndMoveCrates(new CrateMover9001());
}

void ParseInputAndMoveCrates(CrateMover crateMover)
{
    using var input = new StreamReader("input.txt");

    var supplyStack = CreateSupplyStack(input);

    // Account for empty line
    input.ReadLine();

    while (!input.EndOfStream)
    {
        var numbersInInstructions = Regex.Matches(input.ReadLine(), @"\d+").Select(s => int.Parse(s.Value)).ToArray();

        var fromIndex = numbersInInstructions[1] - 1;
        var toIndex = numbersInInstructions[2] - 1;

        crateMover.MoveCrates(numbersInInstructions[0], supplyStack.ElementAt(fromIndex), supplyStack.ElementAt(toIndex));
    }

    StringBuilder sb = new StringBuilder();
    foreach (var stack in supplyStack)
    {
        if (stack.TryPeek(out char c))
        {
            sb.Append(c);
        }
    }

    Console.WriteLine(sb.ToString());
}

IList<Stack<char>> CreateSupplyStack(StreamReader input)
{
    List<Stack<char>> supplies = new List<Stack<char>>();

    // Find until we get to the box numbers
    bool shouldBreak = false; ;
    while (!shouldBreak)
    {
        var line = input.ReadLine();

        // Convert our string into a queue to parse
        Queue<char> queue = new Queue<char>(line);


        int supplyBoxNumber = 1;
        do
        {
            String box = String.Empty;
            for (int i = 0; i < 3; i++)
            {
                box += queue.Dequeue();
            }

            // Add a supply box if we haven't created one yet
            if (supplies.Count < supplyBoxNumber)
            {
                supplies.Add(new Stack<char>());
            }

            // Once we hit the numbers, we are done
            if (char.IsDigit(box[1]))
            {
                shouldBreak = true;
                break;
            }
            // Box will be empty or contain a supply box
            else if (!char.IsWhiteSpace(box[1]))
            {
                supplies.ElementAt(supplyBoxNumber - 1).Push(box[1]);
            }

            supplyBoxNumber++;
        } while (queue.TryDequeue(out char c));

    }

    List<Stack<char>> supplystack = new List<Stack<char>>();
    // Reverse our queue into a stack
    foreach (var queue in supplies)
    {
        Stack<char> stack = new Stack<char>();

        while (queue.TryPop(out char c))
            stack.Push(c);

        supplystack.Add(stack);
    }
    return supplystack;
}

