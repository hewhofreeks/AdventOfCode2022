using System.Runtime.CompilerServices;
List<MonkeyNode> nodesInFile = new List<MonkeyNode>();

PartTwo();

void PartOne()
{
    Dictionary<string, Func<long>> monkeyActions = new Dictionary<string, Func<long>>();
    using var input = new StreamReader("input.txt");

    while (!input.EndOfStream)
    {
        var line = input.ReadLine().Split(' ');

        var monkeyName = line[0].Replace(":", "");

        if (line.Length == 4)
        {
            monkeyActions.Add(monkeyName,
                () =>
                {
                    long retValue = 0;
                    if (monkeyName == "root")
                        return monkeyActions[line[1]]() == monkeyActions[line[3]]() ? 1 : 0;

                    switch (line[2])
                    {
                        case "+":
                            retValue = monkeyActions[line[1]]() + monkeyActions[line[3]]();
                            break;
                        case "*":
                            retValue = monkeyActions[line[1]]() * monkeyActions[line[3]]();
                            break;
                        case "/":
                            retValue = monkeyActions[line[1]]() / monkeyActions[line[3]]();
                            break;
                        case "-":
                            retValue = monkeyActions[line[1]]() - monkeyActions[line[3]]();
                            break;
                    }

                    return retValue;
                });
        }
        else
        {
            monkeyActions.Add(monkeyName, () => long.Parse(line[1]));
        }
    }
}


void PartTwo()
{
    using var input = new StreamReader("input.txt");
    MonkeyNode root = null;
    while (!input.EndOfStream)
    {
        var line = input.ReadLine().Split(' ');

        var monkeyName = line[0].Replace(":", "");

        if (line.Length == 4)
        {
            nodesInFile.Add(new MonkeyNode()
            {
                ID = monkeyName,
                LeftID = line[1],
                RightID = line[3],
                Operation = line[2]
            });
        }
        else
        {
            nodesInFile.Add(new MonkeyNode()
            {
                ID = monkeyName,
                Value = monkeyName == "humn" ? null : long.Parse(line[1])
            });            
        }
    }

    // construct tree
    root = nodesInFile.First(n => n.ID == "root");
    root.Operation = "=";
    
    ConstructNode(root);
    
    // Generate Values when available
    GetValueOfNode(root.Left);
    GetValueOfNode(root.Right);

    var knownNodeValue = root.Left.Value.HasValue ? root.Left : root.Right;
    var humnNodeSearchTree = root.Left.Value.HasValue ? root.Right : root.Left;

    var num = SetMysteryValue(humnNodeSearchTree, knownNodeValue.Value.Value);
    
    Console.WriteLine(num);
}

long SetMysteryValue(MonkeyNode start, long finalValue)
{
    start.Value = finalValue;

    if (start.ID == "humn")
        return finalValue;
    
    var mysteryNode = start.Left.Value.HasValue ? start.Right : start.Left;
    var knownNode = start.Left.Value.HasValue ? start.Left : start.Right;

    if (start.Operation == "+")
        return SetMysteryValue(mysteryNode, finalValue - knownNode.Value.Value);
    if (start.Operation == "-")
    {
        if (mysteryNode == start.Left)
            return SetMysteryValue(mysteryNode, finalValue + knownNode.Value.Value);
        else
            return SetMysteryValue(mysteryNode, knownNode.Value.Value - finalValue);
    }
    if (start.Operation == "*")
    {
        return SetMysteryValue(mysteryNode, finalValue / knownNode.Value.Value);
    }
    if (start.Operation == "/")
    {
        if (mysteryNode == start.Left)
            return SetMysteryValue(mysteryNode, finalValue * knownNode.Value.Value);
        else
            return SetMysteryValue(mysteryNode, knownNode.Value.Value / finalValue);
    }
    throw new Exception("Why didn't this work");
}

void ConstructNode(MonkeyNode node)
{
    if (!node.Value.HasValue && node.LeftID != null)
    {
        node.Left = nodesInFile.First(n => n.ID == node.LeftID);
        node.Right = nodesInFile.First(n => n.ID == node.RightID);
        
        ConstructNode(node.Left);
        ConstructNode(node.Right);
    }
}

long? GetValueOfNode(MonkeyNode node)
{
    long retVal = 0;

    if (node.ID == "humn")
        return null;
    
    if (node.Value.HasValue)
        return node.Value.Value;

    var leftVal = GetValueOfNode(node.Left);
    var rightValue = GetValueOfNode(node.Right);
    
    if (!leftVal.HasValue || !rightValue.HasValue)
        return null;

    if (node.Operation == "+")
        return node.Value = leftVal + rightValue;
    if (node.Operation == "-")
        return node.Value = leftVal - rightValue;
    if (node.Operation == "*")
        return node.Value = leftVal * rightValue;
    if (node.Operation == "/")
        return node.Value = leftVal / rightValue;
    
    return null;
}
class MonkeyNode
{
    public string ID { get; set; }

    public long? Value { get; set; }

    public string LeftID { get; set; }

    public string RightID { get; set; }

    public string Operation { get; set; }

    public MonkeyNode Left { get; set; }

    public MonkeyNode Right { get; set; }
}