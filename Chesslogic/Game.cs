namespace Chesslogic
{
    public class Game
    {
        public Board Board { get; }
        public Player Current { get; private set; }

        public Result Result { get; private set; } = null;

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
            IEnumerable<Moves> Candidates = piece.GetMoves(pos, Board);
            return Candidates.Where(move => move.isLegal(Board));
        }
        public void MakeMove(Moves move)
        {
            Board.setPawnSkippedSpaces(Current, null);
            move.Execute(Board);
            Current = Current.Oppponent();
            CheckForGameOver(Current);
        }
        public IEnumerable<Moves> AllPossibleMoves(Player player)
        {
            IEnumerable<Moves> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board);
            });
            return moveCandidates.Where(move => move.isLegal(Board));
        }
        public void CheckForGameOver(Player player)
        {
            if (AllPossibleMoves(player) == null)
            {
                if (Board.IsInCheck(player))
                {
                    Result = Result.Win(player.Oppponent());
                }
                else
                {
                    Result = Result.Draw(Endreason.Stalemate);
                }
            }
        }
        public bool IsGameOver()
        {
            return Result != null;

        }

    }
}
