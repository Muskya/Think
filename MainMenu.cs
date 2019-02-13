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
        //GUI
        #region GUI
        public Texture2D _backgroundMG { get; set; }
        //Layers displaying conditional booleans
        public bool _mgFadedOut = false, _mgFadedIn = false;

        public Texture2D _backgroundImg { get; set; }
        //Délai d'apparaition. 1Sec/60fps = 0.016 donc ~2 sec de délai
        private double _fadeDelay = .010;
        //Valeurs décimales de couleur. a = alpha 
        //(0, 0, 0) = Noir && (255, 255, 255) = White. <= L'image s'affiche normalement
        private int _r = 0, _g = 0, _b = 0;

        //Panel de boutons
        List<Button> menuPanel;

        #endregion

        //Main Menu Music
        #region Music
        public Song _backgroundTheme { get; set; }
        private float _themeVolume = 0.45f;
        private bool _themeLooping = true;
        #endregion

        //Internal
        #region Internal
        public SpriteFont _debugFontArial { get; set; }
        #endregion

        public MainMenu()
        { }

        //Called just once before Update()
        public void BeginRun()
        {
            //Menu theme
            #region Menu theme
            MediaPlayer.IsRepeating = _themeLooping; //Boucle la musique
            MediaPlayer.Volume = _themeVolume; //Default 0.45f
     
            MediaPlayer.Play(_backgroundTheme);
            #endregion

            #region Menu background

            #endregion

            #region Menu GUI
            //Buttons panel initialization
            menuPanel = new List<Button>();
            int yPosScaler = 50;
            for (int i=0;i<3;i++)
            {
                menuPanel.Add(new Button(new Vector2(50, yPosScaler)));
                yPosScaler += 50;
                
            }
            #endregion
        }

        public void Update(GameTime gameTime)
        {
            //Décrémente le délai de fade. gameTime.ElapsedGameTime =
            //intervalle de temps depuis le dernier appel de Update(). 
            //Donc ici, .TotalSeconds renverra 1 seconde / 60 frames = ~ 0.016.
            _fadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            #region FadeIn/Out MG + Menu
            if (_fadeDelay <= 0) //
            {
                if (!_mgFadedIn) //Si la mention n'est pas apparue
                {
                    //Affiche la mention MonoGame (passe au blanc)
                    _r++; _g++; _b++;

                    if (_r >= 310 && _g >= 310 && _b >= 310)
                        _mgFadedIn = true;
                }

                if (_mgFadedIn) //Si la mention est apparue
                {
                    //Fais disparaître la mention MonoGame (passe au noir)
                    _r--; _g--; _b--;

                    if (_r <= 0 && _g <= 0 && _b <= 0)
                    {
                        _mgFadedIn = false; //On est plus en train d'afficher la mention
                        _mgFadedOut = true; //La mention est terminée
                    }
                }

                if (_mgFadedOut) //Si la mention est terminée
                {
                    //On repasse au blanc pour afficher le Main Menu
                    _r++; _g++; _b++;

                    //Reset du delay
                    _fadeDelay = 0.010;
                }
            }
            #endregion

            

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Affichage de la mention MonoGame selon les conditions de Update()
            spriteBatch.Draw(_backgroundMG, Vector2.Zero, new Color(_r, _g, _b));

            //Si la mention MonoGame a fini d'être affichée
            if (_mgFadedOut) {
                //Affiche le menu mrincipal
                spriteBatch.Draw(_backgroundImg, Vector2.Zero, new Color(_r, _g, _b));

                //Affichage boutons, options, logos, other...
                for (int i=0;i<3;i++)
                {
                    spriteBatch.Draw(menuPanel[i]._texture,
                                     menuPanel[i]._position, Color.White);
                }
            }

            //Debug stuff
            #region Debug
            spriteBatch.DrawString(_debugFontArial, _fadeDelay.ToString(), Vector2.Zero, Color.Green);
            spriteBatch.DrawString(_debugFontArial, gameTime.ElapsedGameTime.TotalSeconds.ToString(),
                new Vector2(0, 20), Color.Green);
            spriteBatch.DrawString(_debugFontArial, String.Format("_r: {0}, _g: {1}, _b: {2}", _r, _g, _b),
                new Vector2(0, 40), Color.Black);
            #endregion 
        }
        
    }
}
