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
    public class Main : Game
    {
        //Relatif au moteur
        #region Engine Relative
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        #endregion

        #region Propriétés de jeu
        float screenHeight, screenWidth;
        #endregion

        //Références
        MainMenu mainMenu;
        Button btnMenu;

        //Enumération des différents états du jeu
        GameState gameState;
        public enum GameState
        {
            MainMenu,
            GameRunning,
            GameOver
        }

        public Main()
        {
            gameState = GameState.MainMenu;
            graphics = new GraphicsDeviceManager(this);
            mainMenu = new MainMenu();
            btnMenu = new Button();
            
            //Content related stuff
            Content.RootDirectory = "Content";
        }

        //BASE.INITIALIZE() CALLS LOADCONTENT() && UNLOADCONTENT()
        protected override void Initialize()
        {
            //Screen relative
            #region Screen relative

            //GPU 
            graphics.HardwareModeSwitch = false;

            //Game window
            //Graphics.ToggleFullScreen();
            screenHeight = 720; screenWidth = 1280;
            graphics.PreferredBackBufferHeight = (int)screenHeight;
            graphics.PreferredBackBufferWidth = (int)screenWidth;
            Window.Title = "THINK ";
            Window.IsBorderless = false;
            Window.AllowUserResizing = true;
            Window.AllowAltF4 = true;

            //Others
            IsMouseVisible = true;

            graphics.ApplyChanges();
            #endregion

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Main Menu
            mainMenu.BackgroundImage = Content.Load<Texture2D>("menu_background");
            mainMenu.BackgroundTheme = Content.Load<Song>("menu_theme");
            mainMenu.BackgroundMG = Content.Load<Texture2D>("monogame_screen");
            mainMenu.ArialDebugFont = Content.Load<SpriteFont>("ariaFont");
            #endregion

            #region Main Menu Buttons
            
            #endregion
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        //Called just once before Update()
        protected override void BeginRun()
        {
            mainMenu.BeginRun(); //BeginRun du MainMenu (background fade, theme..)
        }

        protected override void Update(GameTime gameTime)
        {
            #region Quit Game Shortcuts
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            #endregion

            mainMenu.Update(gameTime);
            btnMenu.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //Draw le MainMenu. (Mention Monogame + Title Screen
            mainMenu.Draw(gameTime, spriteBatch);
            if (mainMenu.mgFadedOut)
            {
                //Draw les boutons lorsque la mention Monogame est terminée
                btnMenu.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
