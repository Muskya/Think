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
        TitleButton newBtn, optionsBtn, playBtn;

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
        {
            
        }

        public void LoadContent(ContentManager Content)
        {
            var scrHeight = Main.screenHeight; var scrWidth = Main.screenWidth;

            this._backgroundImg = Content.Load<Texture2D>("menu_background");
            this._backgroundMG = Content.Load<Texture2D>("monogame_screen");
            this._backgroundTheme = Content.Load<Song>("menu_theme");

            //Buttons panel instanciation
            #region GUI
            playBtn = new
                TitleButton(new Vector2(scrWidth - scrWidth + 60, scrHeight - scrHeight + 60),
                Content.Load<Texture2D>("Buttons/playBtnNormal"),
                Content.Load<Texture2D>("Buttons/playBtnPressed"));
            newBtn = new
                TitleButton(new Vector2(playBtn._position.X, playBtn._position.Y + 125),
                Content.Load<Texture2D>("Buttons/newBtnNormal"),
                Content.Load<Texture2D>("Buttons/newBtnPressed"));
            optionsBtn = new
               TitleButton(new Vector2(playBtn._position.X, newBtn._position.Y + 65),
               Content.Load<Texture2D>("Buttons/optionsBtnNormal"),
               Content.Load<Texture2D>("Buttons/optionsBtnPressed"));
            #endregion

            #region Other
            this._debugFontArial = Content.Load<SpriteFont>("ariaFont");
            #endregion
        }

        //Called just once before Update()
        public void BeginRun()
        {
            //Menu theme
            #region Menu theme
            MediaPlayer.IsRepeating = _themeLooping; //Boucle la musique
            MediaPlayer.Volume = _themeVolume; //Default 0.45f
     
            MediaPlayer.Play(_backgroundTheme);
            #endregion
        }

        public void Update(GameTime gameTime)
        {
            //Décrémente le délai de fade. gameTime.ElapsedGameTime =
            //intervalle de temps depuis le dernier appel de Update(). 
            //Donc ici, .TotalSeconds renverra 1 seconde / 60 frames = ~ 0.016.
            _fadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            #region FadeIn/Out MG + Menu
            if (_fadeDelay <= 0) //1 seconde de fade
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

            //Update les boutons de la liste (Pressed, Route, etc.)
            for (int i = 0; i < TitleButton.menuPanel.Count; i++)
            {
                TitleButton.menuPanel[i].Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Affichage de la mention MonoGame selon les conditions de Update()
            spriteBatch.Draw(_backgroundMG, Vector2.Zero, new Color(_r, _g, _b));

            //Si la mention MonoGame a fini d'être affichée
            if (_mgFadedOut) {
                //Affiche le menu mrincipal
                spriteBatch.Draw(_backgroundImg, Vector2.Zero, new Color(_r, _g, _b));

                //Draw tous les boutons de la liste
                for (int i = 0; i < TitleButton.menuPanel.Count; i++)
                {
                    TitleButton.menuPanel[i].Draw(gameTime, spriteBatch);
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
