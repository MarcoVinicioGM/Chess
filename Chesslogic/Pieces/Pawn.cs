using System.Runtime.InteropServices;

namespace Chesslogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }
        public bool HasMoved { get; set; } = false;

        private readonly PositionDirection forward;

        public Pawn(Player color)
        {
            Color = color;
            if(Color == Player.White)
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
            return Board.IsInBounds(pos) && board[pos] == null;
        }
        private bool CanCapture(Position pos, Board board)
        {
            if(!Board.IsInBounds(pos) || board.isEmpty(pos))
            {
                return false;
            }
            return board[pos].Color != Color;
        }

        private IEnumerable<Moves> ForwardMoves(Position from, Board board)
        {
            Position ahead = from + forward;
            if (HasMoved == true)
            {
                if (canMove(ahead, board))
                {
                    yield return new normalMove(from, ahead);
                }
            }
            else if (canMove(ahead + forward, board))
            {
                yield return new normalMove(from, ahead + forward);
            }
        }
        private IEnumerable<Moves> Captures(Position from, Board board)
        {
            foreach(PositionDirection dir in new PositionDirection[] 
            {PositionDirection.UpLeft, PositionDirection.UpRight})
            {
                Position pos = from + dir;
                if(CanCapture(pos, board))
                {
                    yield return new normalMove(from, pos);
                }
            }
        }

        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            return ForwardMoves(from, board).Concat(Captures(from, board));
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
