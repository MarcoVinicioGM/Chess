

namespace Chesslogic
{
    public class Game
    {
        public Board Board { get; }
        public Player Current { get; private set; }

        public Game(Player player, Board board)
        {
            Current = player;
            Board = board;
        }

        public IEnumerable<Moves> LegalMoves(Position pos)
        {
            if (Board.isEmpty(pos) || Board[pos].Color != Current)
            {
                return Enumerable.Empty<Moves>();
            }
            Piece piece = Board[pos];
            return piece.GetMoves(pos, Board);
        }
        public void MakeMove(Moves move)
        {
            move.Execute(Board);
            Current = Current.Oppponent();
        }
    }
}
