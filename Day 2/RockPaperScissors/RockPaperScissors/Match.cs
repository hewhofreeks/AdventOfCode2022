using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class Match
    {
        private readonly RPS _player1;
        private readonly RPS _player2;

        public Match(RPS player1, RPS player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public int ScoreForPlayer1()
        {
            var matchResult = GetMatchResult(_player1, _player2);

            return (int)_player1 + (int)matchResult;
        }

        public int ScoreForPlayer2()
        {
            var matchResult = GetMatchResult(_player2, _player1);

            return (int)_player2 + (int)matchResult;
        }

        private MatchResult GetMatchResult(RPS checkingToWin, RPS opponent)
        {
            if(checkingToWin == opponent)
            {
                return MatchResult.Tie;
            }

            if (checkingToWin == RPS.Scissors && opponent == RPS.Rock)
                return MatchResult.Loss;

            if ((checkingToWin == RPS.Rock && opponent == RPS.Scissors) || checkingToWin > opponent)
            {
                return MatchResult.Win;
            }

            return MatchResult.Loss;
        }
        
    }
}
