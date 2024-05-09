using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesslogic
{
    public class Chess960 : Game
    {
        public new Board Board { get; }
        public new Player Current { get; private set; }

        public new Result Result { get; private set; } = null;

        public Chess960(Player player, Board board) : base(player, board)
        {
            this.Board = board;
        }
        public override void MakeMove(Moves move)
        {
            bool isCapture = Board[move.ToPosition] != null && Board[move.ToPosition].Color != Current;
            base.MakeMove(move);

            CheckForGameOver();
            ToggleCurrentPlayer();
        }
        private void ToggleCurrentPlayer()
        {
            Current = (Current == Player.White) ? Player.Black : Player.White;
        }

    }
}
