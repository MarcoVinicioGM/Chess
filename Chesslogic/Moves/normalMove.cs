namespace Chesslogic
{
    public class normalMove : Moves
    {
        public override MovementType Type => MovementType.Move;
        public override Position FromPosition { get; }
        public override Position ToPosition { get; }

        public normalMove(Position from, Position to)
        {
            FromPosition = from;
            ToPosition = to;
        }

        public override void Execute(Board board)
        {
            Piece piece = board[FromPosition];

            if (piece != null)
            {
                board[ToPosition] = piece;
                board[FromPosition] = null;
                piece.HasMoved = true;
            }

        }
    }
}
