using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//WIll contain classes for all movement types
namespace Chesslogic
{
    public abstract class Moves
    {
        public abstract MovementType Type { get; }
        public abstract Position FromPosition { get; }
        public abstract Position ToPosition { get; }
        public abstract void Execute(Board board); // Command Pattern

    }
}
