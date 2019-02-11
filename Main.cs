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

        //Logique d'éxecution
        MainMenu mainMenu;

        //Enumération des différents états du jeu
        GameState gameState;
        public enum GameState
        {
            MainMenu,
            GameRunning,
            GameOver
        }

        //LE PROGRAMME VA LA EN PREMIER ENCULE
        public Main()
        {
            gameState = GameState.MainMenu;
            graphics = new GraphicsDeviceManager(this);
            mainMenu = new MainMenu();
            
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
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            mainMenu.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
