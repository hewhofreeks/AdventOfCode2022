// See https://aka.ms/new-console-template for more information

using System;
using RockPaperScissors;
using System.Globalization;
using System.IO;

Part1();

Part2();

void Part1()
{
    using var input = new StreamReader("input.txt");

    var totalScore = 0;
    var rpsInputFactory = new RPSFactory();

    while (!input.EndOfStream)
    {
        var curLine = input.ReadLine();
        var player1 = rpsInputFactory.ParseInput_Part1(curLine.Split(' ')[0]);
        var player2 = rpsInputFactory.ParseInput_Part1(curLine.Split(' ')[1]);

        var currentMatch = new Match(player1, player2);

        totalScore = totalScore + currentMatch.ScoreForPlayer2();
    }

    Console.WriteLine(totalScore);
}

void Part2()
{
    using var input = new StreamReader("input.txt");

    var totalScore = 0;
    var rpsInputFactory = new RPSFactory();

    while (!input.EndOfStream)
    {
        var curLine = input.ReadLine();
        var (player1, player2) = rpsInputFactory.ParseLine(curLine);

        var currentMatch = new Match(player1, player2);

        totalScore = totalScore + currentMatch.ScoreForPlayer2();
    }

    Console.WriteLine(totalScore);
}



