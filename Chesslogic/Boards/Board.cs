

using ChessLogic;

namespace Chesslogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];
        private string fen { get; }

        private readonly Dictionary<Player, Position> SkippedMoves = new Dictionary<Player, Position>()
        {
            {Player.White, null},
            {Player.Black, null}
        };

        // Code cannot access the pieces array directly
        // So we'll make two indexers one with position one with col,row
        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }
        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }
        public Position getPawnSkippedSpaces(Player player)
        {
            return SkippedMoves[player];
        }
        public void setPawnSkippedSpaces(Player player, Position pos)
        {
            SkippedMoves[player] = pos;
        }
        public static Board Initialize()
        {
            Board board = new Board();
            board.AddBasePieces();
            return board;
        }
        public static Board Initialize(string fen)
        {
            Board board = new Board();
            FenParser parse = new FenParser(fen);
            board.AddFenPieces(parse.BoardStateData);
            return board;
        }
        public void AddFenPieces(BoardStateData boardStateData)
        {
            // Access the piece placement data from BoardStateData object
            string[][] ranks = boardStateData.Ranks;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Check and initialize pieces based on piece placement in ranks array
                    string piece = ranks[i][j];

                    if (!string.IsNullOrEmpty(piece))
                    {
                        Player player = piece.ToUpper() == piece ? Player.White : Player.Black; // Determine player based on piece case

                        switch (piece.ToUpper())
                        {
                            case "P":
                                this[i, j] = new Pawn(player);
                                break;
                            case "R":
                                this[i, j] = new Rook(player);
                                break;
                            case "N":
                                this[i, j] = new Knight(player);
                                break;
                            case "B":
                                this[i, j] = new Bishop(player);
                                break;
                            case "Q":
                                this[i, j] = new Queen(player);
                                break;
                            case "K":
                                this[i, j] = new King(player);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        public void ResetBoard()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (this[x, y] == null)
                    {
                        continue;
                    }
                    this[x, y] = null;
                }
            }
        }
        private void AddBasePieces()
        {
            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.Black);
                this[6, i] = new Pawn(Player.White);
            }
            //Generate black back rank peices
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);

            this[7, 0] = new Rook(Player.White);
            this[7, 1] = new Knight(Player.White);
            this[7, 2] = new Bishop(Player.White);
            this[7, 3] = new Queen(Player.White);
            this[7, 4] = new King(Player.White);
            this[7, 5] = new Bishop(Player.White);
            this[7, 6] = new Knight(Player.White);
            this[7, 7] = new Rook(Player.White);
        }
        public static bool IsInBounds(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }
        public bool isEmpty(Position pos)
        {
            return this[pos] == null;
        }
        public bool IsInCheck(Player player)
        {
            if (PiecePositionsFor(player.Oppponent()).Any(pos => this[pos].canCaptureOpponent(pos, this)))
            {
                return true;
            }
            return false;
        }

        public Board Copy()
        {
            Board copy = new Board();
            foreach (Position pos in PiecePosition())
            {
                copy[pos] = this[pos].Copy();
            }
            return copy;
        }
        public IEnumerable<Position> PiecePosition()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Position pos = new Position(x, y);
                    if (!isEmpty(pos))
                    {
                        yield return pos;
                    }
                }

            }
        }
        public IEnumerable<Position> PiecePositionsFor(Player player)
        {
            return PiecePosition().Where(pos => this[pos].Color == player);
        }

    }
}