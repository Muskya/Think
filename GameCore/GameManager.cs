using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Think
{
    static class GameManager
    {
        //Game States (more to come ?)
        public enum GameState
        {
            MainMenu,
            GameRunning,
            GameOver
        }

        //Permettra d'utiliser des Input relatifs à toutes
        //les classes avec GameManager.AltF4Pressed() par exemple...

        public static Vector2 CenterElementOnScreen(Texture2D texture, ref Vector2 pos)
        {
            pos.X = (Main.screenWidth - texture.Width) / 2;
            pos.Y = (Main.screenHeight - texture.Height) / 2;

            return pos;
        }

    }
}
