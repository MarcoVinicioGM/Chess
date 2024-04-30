namespace Chesslogic
{
    public class Castling : Moves
    {
        public override MovementType Type { get; }
        public override Position FromPosition { get; }
        public override Position ToPosition { get; }

        public readonly PositionDirection KingDirection;
        public readonly Position RookFromPosition;
        public readonly Position RookToPosition;

        public Castling(MovementType type, Position KingPos)
        {
            Type = type;
            FromPosition = KingPos;
            if (type == MovementType.CastleKingSide)
            {
                KingDirection = PositionDirection.Right;
                ToPosition = new Position(KingPos.Row, KingPos.Column + 2);
                RookFromPosition = new Position(KingPos.Row, 7);
                RookToPosition = new Position(KingPos.Row, 5);

            }
            if (type == MovementType.CastleQueenSide)
            {
                KingDirection = PositionDirection.Left;
                ToPosition = new Position(KingPos.Row, KingPos.Column - 2);
                RookFromPosition = new Position(KingPos.Row, 0);
                RookToPosition = new Position(KingPos.Row, 3);
            }
        }
        public override void Execute(Board board)
        {
            new normalMove(FromPosition, ToPosition).Execute(board);
            new normalMove(RookFromPosition, RookToPosition).Execute(board);
        }
        public override bool isLegal(Board board)
        {
            Player player = board[FromPosition].Color;
            if (board.IsInCheck(player))
            {
                return false;
            }
            Board copy = board.Copy();
            Position KingPosCopy = FromPosition;

            for (int i = 0; i < 2; i++)
            {
                new normalMove(KingPosCopy, KingPosCopy + KingDirection).Execute(copy);
                if (copy.IsInCheck(player))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
