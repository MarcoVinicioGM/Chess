using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesslogic
{
    public class AtomicChess : Game
    {
        public new Board Board { get; }
        public new Player Current { get; private set; }

        public new Result Result { get; private set; } = null;

        public AtomicChess(Player player, Board board) : base(player, board)
        {
            this.Board = board;
        }
        public override void MakeMove(Moves move)
        {
            bool isCapture = Board[move.ToPosition] != null && Board[move.ToPosition].Color != Current;
            base.MakeMove(move);

            if (isCapture)
            {
                Explode(move.ToPosition, Current);
            }

            CheckForGameOver();
            ToggleCurrentPlayer();
        }
        private void ToggleCurrentPlayer()
        {
            Current = (Current == Player.White) ? Player.Black : Player.White;
        }
        private void Explode(Position position, Player currentPlayer)
        {
            var positions = GetSurroundingPositions(position);

            foreach (Position pos in positions)
            {
                if (Board == null)
                {
                    System.Diagnostics.Debug.WriteLine("Error: Board is null!");
                }
                if (Board.IsInBounds(pos))
                {
                    Piece piece = Board[pos];  // Error check here
                    if (piece == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"No piece at valid position {pos.Row}, {pos.Column}");
                    }
                    else if (piece.Color != currentPlayer && piece.Type != PieceType.Pawn && piece.Type != PieceType.King)
                    {
                        Board[pos] = null;
                    }
                }
            }
        }

        private IEnumerable<Position> GetSurroundingPositions(Position pos)
        {
            List<Position> positions = new List<Position>();

            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    if (rowOffset == 0 && colOffset == 0) continue; // Skip the current position
                    Position adjacentPosition = new Position(pos.Row + rowOffset, pos.Column + colOffset);
                    if (Board.IsInBounds(adjacentPosition))
                    {
                        positions.Add(adjacentPosition);
                    }
                }
            }

            return positions;
        }

    }
}
