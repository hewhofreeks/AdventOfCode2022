using var input = new StreamReader("input.txt");

Queue<int> registerAdds = new Queue<int>();
int xRegister = 1;

var cycle = 1;
Dictionary<int, int> valueAtCycle = new Dictionary<int, int>();
valueAtCycle.Add(1, 1);
do
{
    cycle++;

    if (!input.EndOfStream)
    {
        var instructions = input.ReadLine().Split(" ");

        if (instructions[0] == "noop")
        {
            registerAdds.Enqueue(0);
        }
        else
        {
            registerAdds.Enqueue(0);
            registerAdds.Enqueue(int.Parse(instructions[1]));
        }
    }
    xRegister = xRegister + registerAdds.Dequeue();

    valueAtCycle.Add(cycle, xRegister);


} while (registerAdds.Any());

//Console.WriteLine(
//    GetValueAt(20) +
//    GetValueAt(60) +
//    GetValueAt(100) +
//    GetValueAt(140) +
//    GetValueAt(180) +
//    GetValueAt(220));

//PrintValueAt(20);
//PrintValueAt(60);
//PrintValueAt(100);
//PrintValueAt(140);
//PrintValueAt(180);
//PrintValueAt(220);

foreach(var cycleVal in valueAtCycle)
{
    Console.Write(ShouldPrintPixel(cycleVal.Key) ? "#" : ".");


    if (cycleVal.Key % 40 == 0)
        Console.WriteLine();
}

bool ShouldPrintPixel(int cycle)
{
    var val = valueAtCycle[cycle];
    var screenPosition = (cycle % 40);
    return Enumerable.Range(val, 3).Contains(screenPosition);
}

void PrintValueAt(int cycle)
{
    Console.WriteLine($"{cycle}: {valueAtCycle[cycle]} * {cycle} = {GetValueAt(cycle)}");
}

int GetValueAt(int cycle)
{
    return valueAtCycle[cycle] * cycle;
}