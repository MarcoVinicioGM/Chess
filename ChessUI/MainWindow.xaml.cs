using Chesslogic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace ChessUI
{
    public partial class MainWindow : Window
    {
        private readonly Image[,] PieceImages = new Image[8, 8];

        private readonly Game gameState;
        private Position selectedPosition = null;

        private readonly System.Windows.Shapes.Rectangle[,] highlights = new System.Windows.Shapes.Rectangle[8, 8];
        private readonly Dictionary<Position, Moves> moveCache = new Dictionary<Position, Moves>();
        private static String fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            gameState = new Game(Player.White, Board.Initialize(fen));
            Showcase(gameState.Board);
        }
        private void InitializeBoard()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Image image = new Image();
                    PieceImages[x, y] = image;
                    PieceGrid.Children.Add(image);

                    System.Windows.Shapes.Rectangle highlight = new Rectangle();
                    highlights[x, y] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }
        private void Showcase(Board board)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Piece piece = board[x, y];
                    PieceImages[x, y].Source = Images.GetImage(piece);
                }
            }
        }
        private void BoardGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquare(point);

            if (selectedPosition == null)
            {
                OnFromSelected(pos);
            }
            else
            {
                onToSelected(pos);
            }
        }
        private Position ToSquare(System.Windows.Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }
        private void OnFromSelected(Position pos)
        {
            IEnumerable<Moves> moves = gameState.LegalMoves(pos);

            if (moves.Any())
            {
                selectedPosition = pos;
                cacheMoves(moves);
                ShowHighlights();
            }
        }
        private void onToSelected(Position pos)
        {
            selectedPosition = null;
            HideHighlights();

            if (moveCache.TryGetValue(pos, out Moves move))
            {
                HandleMove(move);
            }
        }
        private void HandleMove(Moves move)
        {
            gameState.MakeMove(move);
            Showcase(gameState.Board);
        }

        private void cacheMoves(IEnumerable<Moves> move)
        {
            moveCache.Clear();
            foreach (Moves m in move)
            {
                moveCache[m.ToPosition] = m;
            }
        }
        private void ShowHighlights()
        {
            System.Windows.Media.Color color = Color.FromArgb(100, 0, 255, 0);
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);
            }
        }
        private void HideHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(Colors.Transparent);
            }
        }

    }
}