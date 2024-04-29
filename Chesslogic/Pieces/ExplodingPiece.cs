namespace Chesslogic.Pieces
{
    public abstract class ExplodingPiece : Piece
    {
        public int row;
        public int col;
        public ExplodingPiece(int row, int column, Player player)
        {
            this.row = row;
            this.col = column;
        }
        private void Explode(Board board, Position from)
        {

        }

    }
}
