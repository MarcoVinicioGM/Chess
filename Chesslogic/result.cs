namespace Chesslogic
{
    public class Result
    {
        public Player Winner { get; }
        public Endreason Reasonforend { get; }

        public Result(Player winner, Endreason reasonforend)
        {
            this.Winner = winner;
            this.Reasonforend = reasonforend;
        }
        public static Result Win(Player winner)
        {
            return new Result(winner, Endreason.Checkmate);
        }
        public static Result Draw(Endreason reason)
        {
            return new Result(Player.NoOne, reason);
        }


    }
}
