﻿using Chesslogic;
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
        private int RunRegular = 0;

        public MainWindow()
        {
            InitializeComponent();
            DisplayMenu();
            if (RunRegular == 0)
            {
                gameState = new Game(Player.White, Board.Initialize());
                InitializeBoard();
                Showcase(gameState.Board);
            }
            else if (RunRegular == 2)
            {
                gameState = new AtomicChess(Player.White, Board.Initialize());
                InitializeBoard();
                Showcase(gameState.Board);
            }
            else if(RunRegular == 1)
            {
                InitializeHex();
                ShowHexagonalChessBoard();

                //gameState = new HexChess(Player.White, Board.Initialize());
            }
            
        }

        private void ShowRegularChessBoard()
        {
            RegularChessBoard.Visibility = Visibility.Visible;
            HexBoard.Visibility = Visibility.Collapsed;
        }

        private void ShowHexagonalChessBoard()
        {
            RegularChessBoard.Visibility = Visibility.Collapsed;
            HexBoard.Visibility = Visibility.Visible;
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
                        RunRegular = 0;
                        break;

                    case StartMenuOption.FourChess:
                        RunRegular = 1; // Adjust as needed for 4x4 Chess mode
                        break;

                    case StartMenuOption.ExpChess:
                        RunRegular = 2; // Adjust as needed for Exploding Chess mode
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
        private void InitializeHex()
        {
            for (int i = 0; i < 91; i++)
            {
                int row, col;
                GetRowColFromHex(i, out row, out col);

                // Create and add the image for the piece
                Image image = new Image();
                PieceImages[row, 0] = image;
                HexGrid.Children.Add(image);

                // Create and add the highlight rectangle
                System.Windows.Shapes.Rectangle highlight = new Rectangle();
                highlights[row, col] = highlight;
                HighlightGrid.Children.Add(highlight);
            }
        }

        private void GetRowColFromHex(int index, out int row, out int col)
        {
            row = index / 9;
            col = index % 9;
        }
        private void ShowcaseHex(Board board)
        {
            for (int i = 0; i < 91; i++)
            {
                int row, col;
                GetRowColFromIndex(i, out row, out col);
                Piece piece = board[i, 0];
                PieceImages[row, col].Source = Images.GetImage(piece);
            }
        }

        private void GetRowColFromIndex(int index, out int row, out int col)
        {
            row = index / 9;
            col = index % 9;
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
                if (option == Option.Restart)
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


            if (RunRegular == 0)
            {
                gameState = new Game(Player.White, Board.Initialize());
                Showcase(gameState.Board);
            }
            else if(RunRegular == 2)
            {
                gameState = new AtomicChess(Player.White, Board.Initialize());
                Showcase(gameState.Board);
            }
            else if(RunRegular == 1)
            {
                InitializeHex();
                ShowHexagonalChessBoard();
                //gameState = new HexChess(Player.White, Board.Initialize());
            }
        }

        private void HexGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}