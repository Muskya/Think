using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

using System;

namespace Think
{
    class MainMenu
    {
        //GUI
        #region GUI
        public Texture2D _backgroundMG { get; set; }
        //Layers displaying conditional booleans
        public bool _mgFadedOut = false, _mgFadedIn = false, skipIntro = false,
            introSkipped = false, canSkip = true;

        public Texture2D _backgroundImg { get; set; }
        //Délai d'apparaition. 1Sec/60fps = 0.016 donc ~2 sec de délai
        private double _fadeDelay = .010;
        //Valeurs décimales de couleur. a = alpha 
        //(0, 0, 0) = Noir && (255, 255, 255) = White. <= L'image s'affiche normalement
        private int _r = 0, _g = 0, _b = 0;

        //Panel de boutons
        MainMenuButton loadBtn, optionsBtn, playBtn;
        private SoundEffect btnClickSound;

        //Autres menus
        //Menus déclarés en tant que GUIMenu pour regrouper les types,
        //mais initialisés en tant que leur classe respective.
        GUIMenu newgameMenu, loadgameMenu, optionsMenu;
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

        //LoadContent, contentManager du programme passé en paramètre lors de son
        //appel dans Main.Cs
        public void LoadContent(ContentManager Content)
        {
            

            //Raccourci pour récupérer la hauteur / largeur de l'écran
            var scrHeight = Main.screenHeight; var scrWidth = Main.screenWidth;

            //Load les différentes textures des backgrounds
            this._backgroundImg = Content.Load<Texture2D>("Graphics/menu_background");
            this._backgroundMG = Content.Load<Texture2D>("Graphics/monogame_screen");
            this._backgroundTheme = Content.Load<Song>("Music/menu_theme");

            this.btnClickSound = Content.Load<SoundEffect>("SFX/important_menu_clicksound");
            //Buttons panel instanciation
            #region GUI
            playBtn = new
                MainMenuButton(new Vector2(scrWidth - scrWidth + 60, scrHeight - scrHeight + 60),
                Content.Load<Texture2D>("Graphics/Buttons/playBtnNormal2"),
                Content.Load<Texture2D>("Graphics/Buttons/playBtnPressed2"), btnClickSound);
            loadBtn = new
                MainMenuButton(new Vector2(playBtn._position.X, playBtn._position.Y + 125),
                Content.Load<Texture2D>("Graphics/Buttons/loadBtnNormal2"),
                Content.Load<Texture2D>("Graphics/Buttons/loadBtnPressed2"), btnClickSound);
            optionsBtn = new
               MainMenuButton(new Vector2(playBtn._position.X, loadBtn._position.Y + 65),
               Content.Load<Texture2D>("Graphics/Buttons/optionsBtnNormal2"),
               Content.Load<Texture2D>("Graphics/Buttons/optionsBtnPressed2"), btnClickSound);
            #endregion

            #region Other
            this._debugFontArial = Content.Load<SpriteFont>("Other/ariaFont");
            #endregion
        }

        //Gère l'introduction du jeu. (Mentions outils / autres
        //jusqu'à l'affiche du main menu, des boutons, des animations, etc.
        public void GameIntro(GameTime gameTime)
        {
            //Si clic, on skip l'intro et on passe au fade-in du menu principal
            if (canSkip && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                skipIntro = true;
            }

            //Décrémente le délai de fade. gameTime.ElapsedGameTime =
            //intervalle de temps depuis le dernier appel de Update(). 
            //Donc ici, .TotalSeconds renverra 1 seconde / 60 frames = ~ 0.016.
            //.Seconds renverrait 0 car .Seconds renvoie les secondes en INT. Donc 1, 2, 3..
            if (!_mgFadedOut)
            {
                _fadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            }
                
 
            //Si on est en train de fade (<= 0 en gros), qu'on a pas cliqué pour 
            //skip l'intro et que l'intro est déjà passée (évite le retour dans ce if)
            if (_fadeDelay <= 0 && !skipIntro && !introSkipped)
            {
                if (!_mgFadedIn) //Si la mention n'est pas apparue
                {
                    //Affiche la mention MonoGame (passe au blanc)
                    _r++; _g++; _b++;

                    if (_r >= 310 && _g >= 310 && _b >= 310)
                    {
                        _mgFadedIn = true;
                        _fadeDelay = 0.020;
                    }
                        
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
            }

            //Si on a cliqué pour skip l'intro
            if (skipIntro)
            {
                _r--; _g--; _b--;
                
                if (_r <= 0 && _g <= 0 && _b <= 0)
                {
                    _mgFadedOut = true; //Lance l'affichage du menu dans Draw()
                    skipIntro = false; //Empêche le retour dans ce if()
                    introSkipped = true; //Empêche le retour dans le premier if() au dessus
                }            
            }

            //Si la mention MonoGame est disparue
            if (_mgFadedOut) //Lance l'apparition du main menu
            {
                //On augmente la couleur, donc indirectement on affiche le main menu
                if (_r <= 310 || _g <= 310 || _b <=310)
                {
                    _r++; _g++; _b++;
                }

                //On enlève la possibilité de skip / fade l'écran
                canSkip = false;
            }
        }

        //Called once just before Update()
        public void BeginRun()
        {
            //Menu theme
            #region Menu theme
            MediaPlayer.IsRepeating = _themeLooping; //Boucle la musique
            MediaPlayer.Volume = _themeVolume; //Default 0.45f
     
            MediaPlayer.Play(_backgroundTheme);
            #endregion
        }

        //Limite les valeurs rgb entre -20 et 310.  
        //Non utilisé pour l'instant.
        public void ColorValueLimiter(int colorValue)
        {
            if (colorValue <= 0)
                colorValue = 0;
            if (colorValue >= 310)
                colorValue = 310;
        }

        public void Update(GameTime gameTime)
        {
            //Introduction (Mention MonoGame + Autres + Main Menu)
            GameIntro(gameTime);

            //Update les boutons de la liste (Pressed, Route, etc.)
            for (int i = 0; i < MainMenuButton.menuPanel.Count; i++)
            {
                MainMenuButton.menuPanel[i].Update(gameTime);
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
                for (int i = 0; i < MainMenuButton.menuPanel.Count; i++)
                {
                    MainMenuButton.menuPanel[i].DrawFade(gameTime, spriteBatch, _r, _g, _b);
                }  
                    
            }

            //Debug stuff
            #region Debug
            spriteBatch.DrawString(_debugFontArial, _fadeDelay.ToString(), Vector2.Zero, Color.Green);
            spriteBatch.DrawString(_debugFontArial, gameTime.ElapsedGameTime.TotalSeconds.ToString(),
                new Vector2(0, 20), Color.Green);
            spriteBatch.DrawString(_debugFontArial, String.Format("_r: {0}, _g: {1}, _b: {2}", _r, _g, _b),
                new Vector2(0, 40), Color.PaleVioletRed);
            #endregion 
        }
        
    }
}
