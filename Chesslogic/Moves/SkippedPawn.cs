
namespace Chesslogic
{
    public class SkippedPawn : Moves
    {
        public override MovementType Type => MovementType.DoublePawn;
        public override Position FromPosition { get; }
        public override Position ToPosition { get; }

        private readonly Position SkippedPosition;
        public SkippedPawn(Position from, Position to)
        {
            FromPosition = from;
            ToPosition = to;
            SkippedPosition = new Position((to.Row - from.Row), from.Column);

        }
        public override void Execute(Board board)
        {
            Player player = board[FromPosition].Color;
            board.setPawnSkippedSpaces(player, SkippedPosition);
            new normalMove(FromPosition, ToPosition).Execute(board);
        }
    }
}
