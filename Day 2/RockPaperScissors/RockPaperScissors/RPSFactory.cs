using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class RPSFactory
    {
        public RPSFactory() { }

        public RPS ParseInput_Part1(string inputChar)
        {
            switch(inputChar)
            {
                case "A":
                case "X":
                    return RPS.Rock;
                case "B":
                case "Y":
                    return RPS.Paper;
                case "C":
                case "Z":
                    return RPS.Scissors;
            }

            throw new Exception("Bad input");
        }

        private RPS ParseRPSInput(string inputChar) 
        {
            switch (inputChar)
            {
                case "A":
                    return RPS.Rock;
                case "B":
                    return RPS.Paper;
                case "C":
                    return RPS.Scissors;
            }

            throw new Exception("Bad input");
        }

        private MatchResult ParseMatchResultInput(string inputChar)
        {
            switch (inputChar)
            {
                case "X":
                    return MatchResult.Loss;
                case "Y":
                    return MatchResult.Tie;
                case "Z":
                    return MatchResult.Win;
            }

            throw new Exception("Bad input");
        }

        private RPS GenerateRPSThrow(RPS challenge, MatchResult intendedResult)
        {
            if(intendedResult == MatchResult.Win)
            {
                var result = (int)challenge + 1;
                return result == 4 ? RPS.Rock : (RPS)result;
            }

            if(intendedResult == MatchResult.Loss)
            {
                var lossResult = (int)challenge - 1;
                return lossResult == 0 ? RPS.Scissors : (RPS)lossResult;
            }

            return challenge;
        }

        public (RPS val1, RPS val2) ParseLine(string line)
        {
            var player1 = ParseRPSInput(line.Split(' ')[0]);

            var intendedResult = ParseMatchResultInput(line.Split(' ')[1]);

            return (player1, GenerateRPSThrow(player1, intendedResult));
        }
    }
}
