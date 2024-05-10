namespace Chesslogic
{
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; }

        private static readonly PositionDirection[] Directions =
        {
            PositionDirection.UpLeft,
            PositionDirection.UpRight,
            PositionDirection.DownLeft,
            PositionDirection.DownRight
        };

        public Bishop(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Moves> GetMoves(Position from, Board board)
            => GenerateMoves(from, board, Directions);

        private IEnumerable<Moves> GenerateMoves(Position from, Board board, PositionDirection[] directions)
        {
            foreach (PositionDirection direction in directions)
            {
                Position nextPosition = from + direction;

                while (Board.IsInBounds(nextPosition) && board.isEmpty(nextPosition))
                {
                    yield return new normalMove(from, nextPosition);
                    nextPosition += direction;
                }

                if (Board.IsInBounds(nextPosition) && board[nextPosition].Color != Color)
                    yield return new normalMove(from, nextPosition);
            }
        }
    }
}