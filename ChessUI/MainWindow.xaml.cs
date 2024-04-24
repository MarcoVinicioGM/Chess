using Chesslogic;
using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    public partial class MainWindow : Window
    {
        private readonly Image[,] PieceImages = new Image[8, 8];

        private readonly Game gameState;
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            gameState = new Game(Player.White, Board.Initialize());
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
    }
}