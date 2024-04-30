namespace Chesslogic
{
    public class Promotion : Moves
    {
        public override MovementType Type => MovementType.Promotion;
        public override Position FromPosition { get; }
        public override Position ToPosition { get; }
        public PieceType PromoteTo { get; }

        public Promotion(Position from, Position to, PieceType newType)
        {
            FromPosition = from;
            ToPosition = to;
            this.PromoteTo = newType;
        }
        private Piece CreatePromotedPiece(Player color)
        {
            switch (PromoteTo)
            {
                case PieceType.Queen:
                    return new Queen(color);
                case PieceType.Rook:
                    return new Rook(color);
                case PieceType.Bishop:
                    return new Bishop(color);
                case PieceType.Knight:
                    return new Knight(color);
                default:
                    throw new Exception("this will never happen");

            };
        }
        public override void Execute(Board board)
        {
            Piece piece = board[FromPosition];
            board[FromPosition] = null;

            Piece PromotedPiece = CreatePromotedPiece(piece.Color);
            PromotedPiece.HasMoved = true;
            board[ToPosition] = PromotedPiece;
        }

    }
}
