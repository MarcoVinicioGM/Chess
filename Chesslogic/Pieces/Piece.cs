using System.Security.AccessControl;

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

        protected IEnumerable<Position> MovePositionInDir(Position from, Board board, PositionDirection[] dirs)
        {
            return dirs.SelectMany(d => MovePositionInDir(from, board, d));
        }
        public static Piece Factory(PieceType pt, int col, int row, Player player)
        {
            switch (pt)
            {
                case PieceType.Pawn:
                    return new Pawn(col, row, player);
                case PieceType.Rook:
                    return new Rook(col, row, player);
                case PieceType.Knight:
                    return new Knight(col, row, player);
                case PieceType.Bishop:
                    return new Bishop(col, row, player);
                case PieceType.Queen:
                    return new Queen(col, row, player);
                case PieceType.King:
                    return new King(col, row, player);
                default:
                    throw new Exception("Invalid PieceType");
            }
        }
    }
}
