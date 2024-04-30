namespace Chesslogic
{
    public class EnPassant : Moves
    {
        public override MovementType Type => MovementType.EnPassant;
        public override Position FromPosition { get; }
        public override Position ToPosition { get; }
        public Position Capture { get; }

        public EnPassant(Position from, Position to)
        {
            FromPosition = from;
            ToPosition = to;
            Capture = new Position(from.Row, to.Column);
        }
        public override void Execute(Board board)
        {
            new normalMove(FromPosition, ToPosition).Execute(board);
            board[Capture] = null;
        }
    }
}
