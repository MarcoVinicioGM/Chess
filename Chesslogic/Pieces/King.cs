namespace Chesslogic
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }

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

        public static bool hasRookMoved(Position pos, Board board)
        {
            if (board.isEmpty(pos))
            {
                return false;
            }
            Piece piece = board[pos];
            if (piece.Type != PieceType.Rook && !piece.HasMoved)
            {
                return true;
            }
            return false; ;
        }

        private static bool CanKingCastleKingSide(Position from, Board board)
        {
            if (board[from].HasMoved)
            {
                Console.WriteLine("King has moved");
                return false;
            }
            Position Rookpos = new Position(from.Row, 7);

            if (hasRookMoved(Rookpos, board))
            {
                Console.WriteLine("Rook has not moved");
                return false;
            }
            for (int i = 5; i < 7; i++)
            {
                if (!board.isEmpty(new Position(from.Row, i)))
                {
                    Console.WriteLine("Space is not empty");
                    return false;
                }
            }
            Console.WriteLine("Can castle");
            return true;
        }
        private static bool CanKingCastleQueenSide(Position from, Board board)
        {
            if (board[from].HasMoved)
            {
                return false;
            }
            if (hasRookMoved(new Position(from.Row, 0), board))
            {
                return false;
            }
            for (int i = 1; i < 4; i++)
            {
                if (!board.isEmpty(new Position(from.Row, i)))
                {
                    return false;
                }
            }
            return true;
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
            foreach (PositionDirection dir in dirs)
            {
                Position to = from + dir;
                if (!Board.IsInBounds(to))
                {
                    continue;
                }
                if (board.isEmpty(to) || board[to].Color != Color)
                {
                    yield return to;
                }
            }
        }

        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            foreach (Position to in MovePositions(from, board))
            {
                yield return new normalMove(from, to);
            }
            if (CanKingCastleKingSide(from, board))
            {
                yield return new Castling(MovementType.CastleKingSide, from);
            }
            if (CanKingCastleQueenSide(from, board))
            {
                yield return new Castling(MovementType.CastleQueenSide, from);
            }
        }
        public override bool canCaptureOpponent(Position from, Board board)
        {
            return false;
        }
    }
}
