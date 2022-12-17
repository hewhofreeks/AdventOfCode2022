// DFS Until Time Runs out or no other moves

using System.Net.Http.Headers;

using var input = new StreamReader("input.txt");

List<ValveRoom> rooms = new List<ValveRoom>();

List<ValveRoom> roomsWithFlow = new List<ValveRoom>();
var isFirst = true;
string start = "AA";
int startIndex = 0;

while (!input.EndOfStream)
{
    var line = input.ReadLine().Split(' ');

    var valveId = line[1];
    var flowRate = int.Parse(line[4].Split('=')[1].Replace(";", ""));

    var room = new ValveRoom { ID = valveId, FlowRate = flowRate };

    for (int i = 9; i < line.Length; i++)
    {
        room.Paths.Add(line[i].Replace(",", ""));
    }

    rooms.Add(room);
    if (room.FlowRate > 0 || room.ID == start)
        roomsWithFlow.Add(room);

    isFirst = false;
}

int[,] distanceBetweenRooms = new int[roomsWithFlow.Count, roomsWithFlow.Count];

for (int i = 0;i< roomsWithFlow.Count;i++)
{
    var room = roomsWithFlow[i];
    if (room.ID == start)
        startIndex = i;

    // calculate distance between all rooms
    for (int j = 0;j < roomsWithFlow.Count;j++)
    {
        // Do not count the same room
        if (i == j)
            continue;

        Queue<ValveSearchNode> queue = new Queue<ValveSearchNode>();
        queue.Enqueue(new ValveSearchNode { Room = room, Length = 0 });
        while(queue.TryDequeue(out var searchRoom))
        {
            if(searchRoom.Room.ID == roomsWithFlow[j].ID)
            {
                distanceBetweenRooms[i, j] = searchRoom.Length;
                break;
            }

            foreach(var nextRoom in searchRoom.Room.Paths)
            {
                var r = rooms.First(f => f.ID == nextRoom);

                queue.Enqueue(new ValveSearchNode { Room = r, Length = searchRoom.Length + 1 });
            }
        }
    }
}

Console.WriteLine("");


var findFlowRate = GetTotalFlowCount(startIndex, 30, new List<int>(), true);

Console.WriteLine(findFlowRate);


long GetTotalFlowCount(int startIndex, int timeRemaining, List<int> valvesOpen, bool isStart)
{
    var room = roomsWithFlow[startIndex];

    long maxFlowCount = 0;
    
    for(int nextIndexToLook = 0; nextIndexToLook < roomsWithFlow.Count; nextIndexToLook++)
    {
        var roomToSearch = roomsWithFlow[nextIndexToLook];

        if (roomToSearch.FlowRate == 0 || valvesOpen.Contains(nextIndexToLook))
            continue;
        
        var currentOpenValves = new List<int>(valvesOpen);
        
        currentOpenValves.Add(nextIndexToLook);

        var timeLeft = timeRemaining - distanceBetweenRooms[startIndex, nextIndexToLook] - 1;

        if (timeLeft <= 0)
            continue;

        var flowCount = GetTotalFlowCount(
                nextIndexToLook,
                timeLeft,
                 currentOpenValves, false);

        maxFlowCount = Math.Max(maxFlowCount, flowCount);
    }

    long calc = 0;
    if (!isStart)
        calc = room.FlowRate * timeRemaining;

    return calc + maxFlowCount;
}

class ValveRoom
{
    public int FlowRate { get; set; }

    public string ID { get; set; }

    public List<string> Paths { get; set; } = new List<string>();
}

class ValveSearchNode
{
    public ValveRoom Room { get; set; }

    public int Length { get; set; }
}

