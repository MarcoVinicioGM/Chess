//WIll contain classes for all movement types
namespace Chesslogic
{
    public abstract class Moves
    {
        public abstract MovementType Type { get; }
        public abstract Position FromPosition { get; }
        public abstract Position ToPosition { get; }
        public abstract void Execute(Board board); // Command Pattern
        //Might be worth finding a faster solution than this below
        public virtual bool isLegal(Board board)
        {
            Player player = board[FromPosition].Color;
            Board newBoard = board.Copy();
            Execute(newBoard);
            return !newBoard.IsInCheck(player);
        }

    }
}
