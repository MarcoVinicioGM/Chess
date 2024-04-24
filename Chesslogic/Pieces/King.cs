

namespace Chesslogic
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }
        public bool HasMoved { get; set; } = false;

        private static readonly PositionDirection[] dirs = new PositionDirection[]
        {
            PositionDirection.Up,
            PositionDirection.Down,
            PositionDirection.Right,
            PositionDirection.Left,
            PositionDirection.UpLeft,
            PositionDirection.UpRight,
            PositionDirection.DownLeft,
            PositionDirection.DownRight
        };

        public King(Player color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public King(int row, int col, Player color)
        {
            this.row = row;
            this.col = col;
            Color = color;
        }
        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach(PositionDirection dir in dirs)
            {
                Position to = from + dir;
                if(!Board.IsInBounds(to))
                {
                    continue;
                }
                if (board[to].Color != Color || board.isEmpty(to))
                {
                    yield return to;
                }
            }
        }

        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            foreach(Position to in MovePositions(from, board))
            {
                yield return new normalMove(from, from + to);
            }
        }
    }
}
