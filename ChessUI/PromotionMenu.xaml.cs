using Chesslogic;
using System.Windows;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for PromotionMenu.xaml
    /// </summary>
    public partial class PromotionMenu : Window
    {
        public event Action<PieceType> PieceSelected;
        public PromotionMenu(Player player)
        {
            InitializeComponent();
            QueenW.Source = Images.GetImage(player, PieceType.Queen);
            RookW.Source = Images.GetImage(player, PieceType.Rook);
            BishopW.Source = Images.GetImage(player, PieceType.Bishop);
            KnightW.Source = Images.GetImage(player, PieceType.Knight);
        }
        private void Queen_MouseDown(object sender, RoutedEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Queen);
            Close();
        }
        private void Rook_MouseDown(object sender, RoutedEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Rook);
            Close();
        }
        private void Bishop_MouseDown(object sender, RoutedEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Bishop);
            Close();
        }
        private void Knight_MouseDown(object sender, RoutedEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Knight);
            Close();
        }

    }
}