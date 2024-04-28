using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesslogic
{
    public class AtomicChess : Game
    {
        public AtomicChess(Player player, Board board) : base(player, board)
        {
        }
        public override void MakeMove(Moves move)
        {
            base.MakeMove(move);
            if (move.ToPosition != null)
            {
                Explode(move.ToPosition);
            }
        }
        private void Explode(Position pos)
        {
            var Victims = GetSurroundingPositions(pos);
            foreach(Position victim in Victims)
            {
                if (Board.IsInBounds(victim) && Board[pos] != null && Board[pos].Type != PieceType.Pawn){
                    Board[victim] = null;
                }
            }
        }
        private IEnumerable<Position> GetSurroundingPositions(Position pos)
        {
            Position[] positions = new Position[8];
            positions[0] = new Position(pos.Row - 1, pos.Column - 1);
            positions[1] = new Position(pos.Row - 1, pos.Column);
            positions[2] = new Position(pos.Row - 1, pos.Column + 1);
            positions[3] = new Position(pos.Row, pos.Column - 1);
            positions[4] = new Position(pos.Row, pos.Column + 1);
            positions[5] = new Position(pos.Row + 1, pos.Column - 1);
            positions[6] = new Position(pos.Row + 1, pos.Column);
            positions[7] = new Position(pos.Row + 1, pos.Column + 1);
            return positions;
        }
    }
}
