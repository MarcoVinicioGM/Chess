

namespace Chesslogic
{
    public class Rook : Piece
    {
        public override PieceType Type => PieceType.Rook;
        public override Player Color { get; }

        public static readonly PositionDirection[] dirs = new PositionDirection[]
        {
            PositionDirection.Up,
            PositionDirection.Down,
            PositionDirection.Right,
            PositionDirection.Left
        };
        public bool HasMoved { get; set; } = false;
        public Rook(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Rook copy = new Rook(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public Rook(int row, int col, Player color)
        {
            this.row = row;
            this.col = col;
            Color = color;
        }

        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            return MovePositionInDir(from, board, dirs).Select(to => new normalMove(from, to));
        }
    }
}
