namespace Chesslogic
{   //Abstract because all pieces will inherit from this class
    public abstract class Piece
    {
        public int row;
        public int col;
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public bool HasMoved { get; set; } = false;
        public abstract Piece Copy();
        public abstract IEnumerable<Moves> GetMoves(Position from, Board board);

        protected IEnumerable<Position> MovePositionInDir(Position from, Board board, PositionDirection dir)
        {
            for (Position pos = from + dir; Board.IsInBounds(pos); pos += dir)
            {
                if (board.isEmpty(pos))
                {
                    yield return pos;
                    continue;
                }

                Piece piece = board[pos];
                if (piece.Color != this.Color)
                {
                    yield return pos;
                }

                yield break;
            }
        }
        public IEnumerable<Position> MovePositionInDir(Position from, Board board, PositionDirection[] dirs)
        {
            return dirs.SelectMany(dir => MovePositionInDir(from, board, dir));
        }

        public virtual bool canCaptureOpponent(Position from, Board board)
        {
            return GetMoves(from, board).Any(move =>
            {
                Piece piece = board[move.ToPosition];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
