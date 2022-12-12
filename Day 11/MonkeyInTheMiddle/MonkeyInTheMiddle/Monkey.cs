using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyInTheMiddle
{
    public class Monkey
    {
        public Queue<ulong> Items { get; set; } = new Queue<ulong>();

        public uint InspectionCount { get; set; } = 0;

        public Func<Monkey, ulong> LeftSideOfOperation { get; set; }

        public Func<Monkey, ulong> RightSideOfOperation { get; set; }

        public string Operation { get; set; }

        public ulong TestNum { get; set; }

        public int ThrowToMonkeyIfFalse { get; set; }
        public int ThrowToMonkeyIfTrue { get; set; }


        public (bool worryTrue, ulong newWorryValue) TestWorry(ulong MOD)
        {
            InspectionCount++;

            var left = LeftSideOfOperation(this);
            var right = RightSideOfOperation(this);
            ulong total = 0;

            if(Operation == "+")
            {
                total = left + right;
            }
            else
            {
                total = left * right;
            }

            total %= MOD;

           //total = total / 3;

            return (total % TestNum == 0, total);
        }

    }
}
