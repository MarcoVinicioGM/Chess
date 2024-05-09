using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesslogic.Boards
{
    public class BoardHorde : Board
    {
        public static BoardHorde Initialize()
        {
            BoardHorde board = new BoardHorde();
            board.AddBasePieces();
            return board;
        }

        protected override void AddBasePieces()
        {
            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.Black);
                this[7, i] = new Pawn(Player.White);
                this[6, i] = new Pawn(Player.White);
                this[5, i] = new Pawn(Player.White);
                this[4, i] = new Pawn(Player.White);
            }
            //Generating the 4 Pawn Pieces Above
            this[3, 1] = new Pawn(Player.White);
            this[3, 2] = new Pawn(Player.White);
            this[3, 5] = new Pawn(Player.White);
            this[3, 6] = new Pawn(Player.White);

            //Generate black back rank peices
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);

        }
    }
}

