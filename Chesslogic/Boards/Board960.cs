using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chesslogic;

namespace Chesslogic
{
    public class Board960 : Board
    {
        public static Board960 Initialize()
        {
            Board960 board = new Board960();
            board.AddRandomPieces();
            return board;
        }
        protected virtual void AddRandomPieces()
        {
            // Initialize the pieces for both players
            Piece[] whitePieces = new Piece[]
            {
            new Rook(Player.White),
            new Knight(Player.White),
            new Bishop(Player.White),
            new Queen(Player.White),
            new King(Player.White),
            new Bishop(Player.White),
            new Knight(Player.White),
            new Rook(Player.White)
            };

            Piece[] blackPieces = new Piece[]
            {
            new Rook(Player.Black),
            new Knight(Player.Black),
            new Bishop(Player.Black),
            new Queen(Player.Black),
            new King(Player.Black),
            new Bishop(Player.Black),
            new Knight(Player.Black),
            new Rook(Player.Black)
            };

            Random random = new Random();
            whitePieces = whitePieces.OrderBy(x => random.Next()).ToArray();
            blackPieces = blackPieces.OrderBy(x => random.Next()).ToArray();

            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.Black);
                this[6, i] = new Pawn(Player.White);
                this[0, i] = blackPieces[i];
                this[7, i] = whitePieces[i];
            }
        }
    }
}
