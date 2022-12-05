using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyStacks
{
    internal abstract class CrateMover
    {
        public abstract void MoveCrates(int numCrates, Stack<char> toMoveFrom, Stack<char> toMoveTo);
    }

    internal class CrateMover9000 : CrateMover
    {
        public override void MoveCrates(int numCrates, Stack<char> toMoveFrom, Stack<char> toMoveTo)
        {
            for (int i = 0; i < numCrates; i++)
            {
                toMoveTo.Push(toMoveFrom.Pop());
            }
        }
    }

    internal class CrateMover9001 : CrateMover
    {
        public override void MoveCrates(int numCrates, Stack<char> toMoveFrom, Stack<char> toMoveTo)
        {
            Stack<char> holdingStack = new Stack<char>();

            for (int i = 0; i < numCrates; i++)
            {
                holdingStack.Push(toMoveFrom.Pop());
            }

            while(holdingStack.TryPop(out char c))
            {
                toMoveTo.Push(c);
            }
        }
    }
}
