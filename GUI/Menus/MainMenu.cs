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
using System;

namespace Think
{
    class MainMenu
    {
        //GUI
        #region GUI
        public Texture2D MonogameBackground { get; set; }
        public Texture2D MainMenuBackground { get; set; }

        //Délai d'apparaition. 1Sec/60fps = 0.016 donc ~2 sec de délai
        private double IntroductionDelay = .010;
        //Valeurs décimales de couleur. a = alpha 
        //(0, 0, 0) = Noir && (255, 255, 255) = White. <= L'image s'affiche normalement
        private int _r = 0, _g = 0, _b = 0; //Private, uniquement géré depuis cette classe

        //Introduction Layers displaying conditional booleans
        private bool mgFadedOut = false, mgFadedIn = false, skipIntro = false,
            introSkipped = false, canSkip = true;

        //Panel de boutons
        MainMenuButtons loadButton, optionsButton, playButton;
        public SoundEffect BtnClickSound { get; set; }

        //Autres menus
        //Menus déclarés en tant que GUIMenu pour regrouper les types,
        public List<GUIMenu> listeMenus = new List<GUIMenu>();
        public GUIMenu optionsMenu, playMenu, loadMenu;
        #endregion

        //Sound
        #region Sound
        public Song MainMenuTheme { get; set; }
        public float MainMenuThemeVolume { get; set; } //Initialisation dans le constructeur car auto-property 
        private bool MainMenuThemeLooping { get; set; } //Initialisation dans le constructeur car auto-property
        #endregion

        //Internal
        #region Internal
        public static SpriteFont _debugFontArial { get; set; } //Police de debug
        #endregion
        
        //Constructor
        public MainMenu()
        {
            //Initialisation des différents menus affichables depuis le Main Menu
            optionsMenu = new OptionsMenu("OptionsMenu");
            playMenu = new PlayMenu("PlayMenu");
            loadMenu = new LoadMenu("LoadMenu");

            listeMenus.Add(optionsMenu);
            listeMenus.Add(playMenu);
            listeMenus.Add(loadMenu);

            this.MainMenuThemeVolume = 0.45f; //Initialisation ici car auto-property (non possible en tête de classe)
            this.MainMenuThemeLooping = true; //Initialisation ici car auto-property
        }

        //Load du content du Main Menu, appelé dans Main.cs avec
        //le ContentManager du jeu en argument
        public void LoadContent(ContentManager Content)
        {
            //Hauteur / largeur de l'écran
            var scrHeight = Main.screenHeight; var scrWidth = Main.screenWidth;

            //Load les différentes textures des backgrounds
            this.MainMenuBackground = Content.Load<Texture2D>("Graphics/menu_background");
            this.MonogameBackground = Content.Load<Texture2D>("Graphics/monogame_screen");
            this.MainMenuTheme = Content.Load<Song>("Music/menu_theme");

            //Load du son lors du clic sur un bouton
            this.BtnClickSound = Content.Load<SoundEffect>("SFX/important_menu_clicksound");

            //Load le content des différents menus accessibles depuis
            //le menu principal
            playMenu.LoadContent(Content);
            optionsMenu.LoadContent(Content);
            loadMenu.LoadContent(Content);
            
            //Buttons panel instanciation
            playButton = new
                MainMenuButtons("playbtn", new Vector2(scrWidth - scrWidth + 40, scrHeight - scrHeight + 60),
                Content.Load<Texture2D>("Graphics/Buttons/playBtnNormal2"),
                Content.Load<Texture2D>("Graphics/Buttons/playBtnPressed2"), BtnClickSound, playMenu);
            loadButton = new
                MainMenuButtons("loadbtn", new Vector2(playButton.Position.X + 30, playButton.Position.Y + 65),
                Content.Load<Texture2D>("Graphics/Buttons/loadBtnNormal2"),
                Content.Load<Texture2D>("Graphics/Buttons/loadBtnPressed2"), BtnClickSound, loadMenu);
            optionsButton = new
               MainMenuButtons("optionsbtn", new Vector2(playButton.Position.X + 60, loadButton.Position.Y + 65),
               Content.Load<Texture2D>("Graphics/Buttons/optionsBtnNormal2"),
               Content.Load<Texture2D>("Graphics/Buttons/optionsBtnPressed2"), BtnClickSound, optionsMenu);

            //Load de la debug font
            _debugFontArial = Content.Load<SpriteFont>("Other/ariaFont");
        }

        //Gère l'introduction du jeu. (Mentions outils / autres
        //jusqu'à l'affiche du main menu, des boutons, des animations, etc.
        public void GameIntro(GameTime gameTime)
        {
            //Si clic, on skip l'intro et on passe au fade-in du menu principal
            if (canSkip && Mouse.GetState().LeftButton == ButtonState.Pressed) //canSkip = true de base
            {
                skipIntro = true;
            }

            //Si on a cliqué pour skip l'intro
            if (skipIntro)
            {
                _r--; _g--; _b--; //L'écran passe au noir pour la transition visuelle

                if (_r <= 0 && _g <= 0 && _b <= 0) //Quand l'écran est devenu noir
                {
                    mgFadedOut = true; //Lance l'affichage du menu principal dans Draw()
                    skipIntro = false; //Empêche le retour dans ce if()
                    introSkipped = true; //Empêche le retour dans le premier if() au dessus
                }
            }

            //Décrémente le délai de fade. 
            //gameTime.ElapsedGameTime = intervalle de temps depuis le dernier appel de Update(). 
            //Donc ici, .TotalSeconds renverra 1 seconde / 60 frames = ~ 0.016f.
            if (!mgFadedOut)
            {
                IntroductionDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            }
   
            //Si on est en train de fade (delay <= 0 en gros), qu'on a pas cliqué pour 
            //skip l'intro et que l'intro est déjà passée (évite le retour dans ce if)
            if (IntroductionDelay <= 0 && !skipIntro && !introSkipped)
            {
                if (!mgFadedIn) //Si la mention monogame n'est pas apparue
                {
                    //Affiche la mention MonoGame (passe au blanc)
                    _r++; _g++; _b++;

                    if (_r >= 310 && _g >= 310 && _b >= 310) //Quand on arrive au rgb=blanc, la mention monogame est apparue
                    {
                        mgFadedIn = true;
                        IntroductionDelay = 0.020; //Reset du fade delay
                    }
                        
                }

                if (mgFadedIn) //Si la mention est apparue
                {
                    //Fais disparaître la mention MonoGame (passe au noir)
                    _r--; _g--; _b--;

                    if (_r <= 0 && _g <= 0 && _b <= 0) //Quand l'écran est redevenu noir
                    {
                        mgFadedIn = false; //On est plus en train d'afficher la mention
                        mgFadedOut = true; //La mention est terminée
                    }
                }
            }

            //Si la mention MonoGame est disparue
            if (mgFadedOut) //Lance l'apparition du main menu
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
            //Lancement de la musique avant d'appeler les Update() et Draw() en boucle
            //Classe MediaPlayer utilisée pour les "Songs"
            MediaPlayer.IsRepeating = MainMenuThemeLooping; //Boucle la musique
            MediaPlayer.Volume = MainMenuThemeVolume; //Default 0.45f
            MediaPlayer.Play(MainMenuTheme);
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

            //Update les boutons du menu principal (Play, Load, Options)
            for (int i = 0; i < MainMenuButtons.mainMenuButtonsList.Count; i++) //Pour chaque bouton de la liste
            {
                //Update de chaque bouton (gère textures, états, booléens etc)
                MainMenuButtons.mainMenuButtonsList[i].Update(gameTime);

                //Ouverture / Fermeture du menu par clic sur le bouton correspondant
                if (MainMenuButtons.mainMenuButtonsList[i].doBtnAction)
                {
                    MainMenuButtons.mainMenuButtonsList[i].menuToOpen.isOpened = true;
                    MainMenuButtons.mainMenuButtonsList[i].menuToOpen.Update(gameTime); //Actions spécifiques avec le menu en question
                } else
                {
                    MainMenuButtons.mainMenuButtonsList[i].menuToOpen.isOpened = false;
                }

            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Affichage de la mention MonoGame selon les conditions de Update()
            spriteBatch.Draw(MonogameBackground, Vector2.Zero, new Color(_r, _g, _b));

            //Si la mention MonoGame a fini d'être affichée
            if (mgFadedOut) {

                //Affiche le menu mrincipal
                spriteBatch.Draw(MainMenuBackground, Vector2.Zero, new Color(_r, _g, _b));

                //Affiche tous les boutons de la liste
                for (int i = 0; i < MainMenuButtons.mainMenuButtonsList.Count; i++)
                {
                    MainMenuButtons.mainMenuButtonsList[i].DrawFade(gameTime, spriteBatch, _r, _g, _b);
                    MainMenuButtons.mainMenuButtonsList[i].menuToOpen.Draw(gameTime, spriteBatch);
                }

            }

            //Debug stuff
            #region Debug
            //spriteBatch.DrawString(_debugFontArial, IntroductionDelay.ToString(), Vector2.Zero, Color.Green);
            //spriteBatch.DrawString(_debugFontArial, gameTime.ElapsedGameTime.TotalSeconds.ToString(),
            //    new Vector2(0, 20), Color.Green); //Temps passé depuis le dernier Update() (toujours 0.016666667 en général)
            //spriteBatch.DrawString(_debugFontArial, String.Format("_r: {0}, _g: {1}, _b: {2}", _r, _g, _b),
            //    new Vector2(0, 40), Color.PaleVioletRed);

            //spriteBatch.DrawString(_debugFontArial, "PlayMenu.isOpened:" + playMenu.isOpened.ToString(), new Vector2(500, 20), Color.White);
            //spriteBatch.DrawString(_debugFontArial, "PlayButton.doBtnAction:" + playButton.doBtnAction.ToString(), new Vector2(500, 40), Color.White);
            //spriteBatch.DrawString(_debugFontArial, "PlayMenu.closeBtn.doBtnAction:" + playMenu.closeBtn.doBtnAction.ToString(), new Vector2(500, 60), Color.White);

            //spriteBatch.DrawString(_debugFontArial, "LoadMenu.isOpened:" + loadMenu.isOpened.ToString(), new Vector2(500, 90), Color.White);
            //spriteBatch.DrawString(_debugFontArial, "LoadButton.doBtnAction:" + loadButton.doBtnAction.ToString(), new Vector2(500, 110), Color.White);
            //spriteBatch.DrawString(_debugFontArial, "LoadMenu.closeBtn.doBtnAction:" + loadMenu.closeBtn.doBtnAction.ToString(), new Vector2(500, 130), Color.White);

            //spriteBatch.DrawString(_debugFontArial, "OptionsMenu.isOpened:" + optionsMenu.isOpened.ToString(), new Vector2(500, 160), Color.White);
            //spriteBatch.DrawString(_debugFontArial, "OPtionsButton.doBtnAction:" + optionsButton.doBtnAction.ToString(), new Vector2(500, 180), Color.White);
            //spriteBatch.DrawString(_debugFontArial, "OptionsqMenu.closeBtn.doBtnAction:" + optionsMenu.closeBtn.doBtnAction.ToString(), new Vector2(500, 200), Color.White);
            #endregion 
        }
        
    }
}
