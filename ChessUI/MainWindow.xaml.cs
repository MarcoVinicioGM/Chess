using Chesslogic;
using Chesslogic.Boards;
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

        private Game gameState;
        private Position selectedPosition = null;

        private readonly System.Windows.Shapes.Rectangle[,] highlights = new System.Windows.Shapes.Rectangle[8, 8];
        private readonly Dictionary<Position, Moves> moveCache = new Dictionary<Position, Moves>();
        private static String fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        private ChessMode currentMode;
        private enum ChessMode
        {
            RegularChess = 0,
            ExplodingChess = 1,
            Chess960 = 2,
            HordeBoard = 3
        }

        public MainWindow()
        {
            InitializeComponent();
            DisplayMenu();
            InitializeBoard();
            switch(currentMode)
            {
                case ChessMode.RegularChess:
                    gameState = new Game(Player.White, Board.Initialize());
                    break;
                case ChessMode.ExplodingChess:
                    gameState = new AtomicChess(Player.White, Board.Initialize());
                    break;
                case ChessMode.Chess960:
                    gameState = new Chess960(Player.White, Board.Initialize());
                    break;
                case ChessMode.HordeBoard:
                    //gameState = new FourChess(Player.White, Board.Initialize());
                    break;
                default:
                    gameState = new Game(Player.White, Board.Initialize());
                    break;
            }
            Showcase(gameState.Board);
            
        }
        private void DisplayMenu()
        {
            StartMenu startMenu = new StartMenu();
            MainContent.Content = startMenu;

            startMenu.OptionSelected += option =>
            {
                switch (option)
                {
                    case StartMenuOption.RegChess:
                        currentMode = ChessMode.RegularChess;   
                        break;

                    case StartMenuOption.HordeBoard:
                        currentMode = ChessMode.HordeBoard;
                        break;

                    case StartMenuOption.ExpChess:
                        currentMode = ChessMode.ExplodingChess;
                        break;
                    case StartMenuOption.Chess960:
                        currentMode = ChessMode.Chess960;
                        break;

                    default:
                        break;
                }
                MainContent.Content = null;
                RestartGame();
            };
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
            if (IsMenuOnScreen())
            {
                return;
            }

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
                if (move.Type == MovementType.Promotion)
                {
                    HandlePromotion(move.FromPosition, move.ToPosition);
                }
                else
                {
                    HandleMove(move);
                }
            }
        }
        private void HandlePromotion(Position fromPosition, Position toPosition)
        {
            PieceImages[toPosition.Row, toPosition.Column].Source = Images.GetImage(gameState.Current, PieceType.Pawn);
            PieceImages[fromPosition.Row, fromPosition.Column].Source = null;

            PromotionMenu promoMenu = new PromotionMenu(gameState.Current);
            MenuContainer.Content = promoMenu;

            promoMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Moves promomove = new Promotion(fromPosition, toPosition, type);
                HandleMove(promomove);
            };
        }
        private void HandleMove(Moves move)
        {
            gameState.MakeMove(move);
            Showcase(gameState.Board);
            System.Diagnostics.Debug.WriteLine(gameState.IsGameOver());
            if (gameState.IsGameOver())
            {
                ShowGameOver();
            }
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

        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }

        private void ShowGameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(gameState);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += option =>
            {
                if (option.Equals( Option.Restart))
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }
        private void RestartGame()
        {
            HideHighlights();
            moveCache.Clear();

            switch (currentMode)
            {
                case ChessMode.RegularChess:
                    gameState = new Game(Player.White, Board.Initialize());
                    break;
                case ChessMode.ExplodingChess:
                    gameState = new AtomicChess(Player.White, Board.Initialize());
                    break;
                case ChessMode.Chess960:
                    gameState = new Game(Player.White, Board960.Initialize());
                    break;
                case ChessMode.HordeBoard:
                    gameState = new Game(Player.White, BoardHorde.Initialize());
                    break;
                default:
                    // Handle default case
                    break;
            }

            Showcase(gameState.Board);
        }

    }
}