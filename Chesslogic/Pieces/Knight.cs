

namespace Chesslogic
{
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;

        public override Player Color { get; }
        public Knight(Player color)
        {
            Color = color;

        }
        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        public Knight(int row, int col, Player color)
        {
            this.row = row;
            this.col = col;
            Color = color;
        }
        private static IEnumerable<Position> PotentialPositions(Position from)
        {
            foreach (PositionDirection dir in new PositionDirection[] {PositionDirection.Up, PositionDirection.Down})
            {
                foreach(PositionDirection dir2 in new PositionDirection[] {PositionDirection.Left, PositionDirection.Right})
                {
                    yield return from + 2 *dir + dir2;
                    yield return from + dir + 2 *dir2;
                }
            }
        }
        private IEnumerable<Position> actualPositions(Position from, Board board)
        {
            return PotentialPositions(from).Where(pos => Board.IsInBounds(pos) && (board.isEmpty(pos)
            || board[pos].Color != Color));
        }
        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            return actualPositions(from, board).Select(to => new normalMove(from, to));
        }

    }
}
