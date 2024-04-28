namespace Chesslogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        private readonly PositionDirection forward;

        public Pawn(Player color)
        {
            Color = color;
            if (Color == Player.White)
            {
                forward = PositionDirection.Up;
            }
            else
            {
                forward = PositionDirection.Down;
            }
        }
        private static bool canMove(Position pos, Board board)
        {
            return Board.IsInBounds(pos) && board.isEmpty(pos);
        }
        private bool CanCapture(Position pos, Board board)
        {
            if (!Board.IsInBounds(pos) || board.isEmpty(pos))
            {
                return false;
            }
            return board[pos].Color != Color;
        }

        private IEnumerable<Moves> ForwardMoves(Position from, Board board)
        {
            Position ahead = from + forward;

            if (canMove(ahead, board))
            {
                if (ahead.Row == 0 || ahead.Row == 7)
                {
                    foreach (Moves move in PromotionMoves(from, ahead))
                    {
                        yield return move;
                    }
                }
                else
                {
                    yield return new normalMove(from, ahead);
                }

                if (canMove(ahead + forward, board) && !HasMoved)
                {
                    yield return new SkippedPawn(from, ahead + forward);
                }
            }
        }
        private IEnumerable<Moves> Captures(Position from, Board board)
        {
            foreach (PositionDirection dir in new PositionDirection[]
            {PositionDirection.Left, PositionDirection.Right})
            {
                Position to = from + forward + dir;

                if (to == board.getPawnSkippedSpaces(Color.Oppponent()))

                {
                    yield return new EnPassant(from, to);
                }
                else if (CanCapture(to, board))
                {
                    if (to.Row == 0 || to.Row == 7)
                    {
                        foreach (Moves move in PromotionMoves(from, to))
                        {
                            yield return move;
                        }
                    }
                    else
                    {
                        yield return new normalMove(from, to);
                    }
                }
            }
        }

        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            return ForwardMoves(from, board).Concat(Captures(from, board));
        }

        private static IEnumerable<Moves> PromotionMoves(Position from, Position to)
        {

            foreach (PieceType pt in new PieceType[]
            {
                PieceType.Queen,
                PieceType.Rook,
                PieceType.Bishop,
                PieceType.Knight
            })
            {
                yield return new Promotion(from, to, pt);
            }

        }
        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        public Pawn(int row, int col, Player color)
        {
            this.row = row;
            this.col = col;
            Color = color;
        }
    }
}
