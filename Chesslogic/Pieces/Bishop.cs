namespace Chesslogic
{
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; }

        private static readonly PositionDirection[] dirs = new PositionDirection[]
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
        public Bishop(int row, int col, Player color)
        {
            this.row = row;
            this.col = col;
            Color = color;
        }
        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            return MovePositionInDir(from, board, dirs).Select(to => new normalMove(from, to));
        }
    }
}
