namespace Chesslogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        private readonly PositionDirection forwardDirection;

        public Pawn(Player color)
        {
            Color = color;
            forwardDirection = color == Player.White ? PositionDirection.Up : PositionDirection.Down;
        }

        private bool CanMoveForward(Position pos, Board board)
            => Board.IsInBounds(pos) && board.isEmpty(pos);

        private bool CanCapture(Position pos, Board board)
        {
            if (!Board.IsInBounds(pos) || board.isEmpty(pos))
                return false;

            return board[pos].Color != Color;
        }

        private IEnumerable<Moves> GenerateForwardMoves(Position from, Board board)
        {
            Position nextPosition = from + forwardDirection;

            if (CanMoveForward(nextPosition, board))
            {
                if (IsPromotionRow(nextPosition.Row))
                {
                    foreach (Moves move in GeneratePromotionMoves(from, nextPosition))
                        yield return move;
                }
                else
                {
                    yield return new normalMove(from, nextPosition);
                }

                if (CanDoubleAdvance(nextPosition, board) && !HasMoved)
                    yield return new SkippedPawn(from, nextPosition + forwardDirection);
            }
        }

        private IEnumerable<Moves> GenerateCaptureMoves(Position from, Board board)
        {
            foreach (PositionDirection direction in new[] { PositionDirection.Left, PositionDirection.Right })
            {
                Position capturePosition = from + forwardDirection + direction;

                if (capturePosition == board.getPawnSkippedSpaces(Color.Oppponent()))
                    yield return new EnPassant(from, capturePosition);
                else if (CanCapture(capturePosition, board))
                {
                    if (IsPromotionRow(capturePosition.Row))
                    {
                        foreach (Moves move in GeneratePromotionMoves(from, capturePosition))
                            yield return move;
                    }
                    else
                    {
                        yield return new normalMove(from, capturePosition);
                    }
                }
            }
        }

        public override IEnumerable<Moves> GetMoves(Position from, Board board)
            => GenerateForwardMoves(from, board).Concat(GenerateCaptureMoves(from, board));

        private static IEnumerable<Moves> GeneratePromotionMoves(Position from, Position to)
        {
            foreach (PieceType pieceType in new[] { PieceType.Queen, PieceType.Rook, PieceType.Bishop, PieceType.Knight })
                yield return new Promotion(from, to, pieceType);
        }

        private bool CanDoubleAdvance(Position nextPosition, Board board)
            => Board.IsInBounds(nextPosition + forwardDirection) && board.isEmpty(nextPosition) && board.isEmpty(nextPosition + forwardDirection);

        private bool IsPromotionRow(int row)
            => row == 0 || row == 7;

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}