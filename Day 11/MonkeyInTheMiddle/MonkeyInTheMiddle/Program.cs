using MonkeyInTheMiddle;
using System.Text.RegularExpressions;

using var input = new StreamReader("input.txt");

List<Monkey> monkeys = new List<Monkey>();
ulong SUPER_MOD = 1;

while(!input.EndOfStream)
{
    var monkey = new Monkey();
    var number = input.ReadLine(); // Monkey 0:
    var startingItems = input.ReadLine(); //  Starting items: 79, 98
    var items = Regex.Matches(startingItems, @"\d+").Select(s => ulong.Parse(s.Value));
    foreach(var item in items)
        monkey.Items.Enqueue(item);

    var operation = input.ReadLine(); //  Operation: new = old * 19
    var opValue = Regex.Match(operation, @"= (.*)").Value.Split(" ");
    if (opValue[1] == "old")
    {
        monkey.LeftSideOfOperation = (m) => m.Items.Peek();
    }
    else
    {
        monkey.LeftSideOfOperation = (m) => ulong.Parse(opValue[1]);
    }

    monkey.Operation = opValue[2];

    if (opValue[3] == "old")
    {
        monkey.RightSideOfOperation = (m) => m.Items.Peek();
    }
    else
    {
        monkey.RightSideOfOperation = (m) => ulong.Parse(opValue[3]);
    }

    var test = input.ReadLine(); //   Test: divisible by 23
    monkey.TestNum = ulong.Parse(Regex.Match(test, @"\d+").Value);

    SUPER_MOD *= monkey.TestNum;

    var ifTrue = input.ReadLine(); //     If true: throw to monkey 2
    monkey.ThrowToMonkeyIfTrue = int.Parse(Regex.Match(ifTrue, @"\d+").Value);

    var ifFalse = input.ReadLine(); //    If false: throw to monkey 3
    monkey.ThrowToMonkeyIfFalse = int.Parse(Regex.Match(ifFalse, @"\d+").Value);

    monkeys.Add(monkey);
    if(!input.EndOfStream)
    input.ReadLine();
}

Console.WriteLine("Done creating monkeys");

for(int i = 0;i<10000;i++)
{
    foreach(var monkey in monkeys)
    {
        while(monkey.Items.TryPeek(out var item))
        {
            var (worryResults, newVal) = monkey.TestWorry(SUPER_MOD);
            var toThrow = monkey.Items.Dequeue();

            if (worryResults)
            {
                monkeys[monkey.ThrowToMonkeyIfTrue].Items.Enqueue(newVal);
            }
            else
            {
                monkeys[monkey.ThrowToMonkeyIfFalse].Items.Enqueue(newVal);
            }
        }
    }
}

var monkeyBusiness = monkeys.Select(m => m.InspectionCount).OrderDescending().Take(2).ToArray();

Console.WriteLine($"{monkeyBusiness[0]} * {monkeyBusiness[1]}");