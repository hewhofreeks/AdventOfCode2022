// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;
using DistressSignal;

PartTwo();

void PartOne()
{
    using var input = new StreamReader("input.txt");

    var index = 1;
    var count = 0;
    while (!input.EndOfStream)
    {
        var packetOne = JsonSerializer.Deserialize<List<dynamic>>(input.ReadLine());
        var packetTwo = JsonSerializer.Deserialize<List<dynamic>>(input.ReadLine());

        var compare = PacketComparer.ComparePackets(packetOne, packetTwo);
        if (compare == 1)
        {
            count += index;
        }

        index++;
        if (!input.EndOfStream)
            input.ReadLine();
    }

    Console.WriteLine($"Total count: {count}");
}

void PartTwo()
{
    using var input = new StreamReader("input.txt");

    PriorityQueue<string, string> minPacketHeap = new PriorityQueue<string, string>(new PacketComparer());
    while (!input.EndOfStream)
    {
        var line = input.ReadLine();
        if (string.IsNullOrWhiteSpace(line))
            continue;

        minPacketHeap.Enqueue(line, line);
    }

    minPacketHeap.Enqueue("[[2]]", "[[2]]");
    minPacketHeap.Enqueue("[[6]]", "[[6]]");
    
    var index = 0;
    var divider2Index = 0;
    var divider6Index = 0;
    while (minPacketHeap.TryDequeue(out var element, out var priority))
    {
        index++;
        if (element == "[[2]]")
            divider2Index = index;
        if (element == "[[6]]")
            divider6Index = index;

        if (divider2Index != 0 && divider6Index != 0)
            break;
    }
    
    Console.WriteLine($"{divider2Index * divider6Index}");
}



