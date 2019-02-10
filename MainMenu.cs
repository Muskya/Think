using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Think
{
    class MainMenu
    {
        private Texture2D _backgroundImage;
        private Song _backgroundTheme;
        private SpriteFont _mainFont;

        public MainMenu(Texture2D menu_bg, Song menu_theme)
        {
            _backgroundImage = menu_bg;
            _backgroundTheme = menu_theme;
        }

        //Called just once before Update()
        public void BeginRun()
        {
            //Menu theme
            #region Menu theme
            MediaPlayer.Play(_backgroundTheme);

            MediaPlayer.IsRepeating = true; //Boucle la musique
            MediaPlayer.Volume = 0.45f;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            #endregion
        }

        public void Update(GameTime gt)
        { 

        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(_backgroundImage, Vector2.Zero, Color.White);
        }

        //Evenement qui decrease lentement le volume quand la musique 
        //va s'arrêter pour loop de façon cohérente
        private void MediaPlayer_MediaStateChanged(object sender, EventArgs e) {
            MediaPlayer.Volume -= 0.1f; 
            MediaPlayer.Play(_backgroundTheme);
        }

        
    }
}
