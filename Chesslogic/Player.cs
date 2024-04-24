
namespace Chesslogic
{
    public enum Player
    {
        White,
        Black,
        NoOne
    }

    public static class ExtendPlayer
    {
        public static Player Oppponent(this Player player)
        {
            switch (player)
            {
                case Player.White:
                    return Player.Black;
                case Player.Black:
                    return Player.White;
                default:
                    return Player.NoOne;
            }
        }
    }
}
