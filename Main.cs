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
        float screenHeight, screenWidth;
        #endregion

        //Logique d'éxecution
        MainMenu mainMenu;

        //Content
        #region Main Menu Content
        private Texture2D _menuBackground;
        private Song _menuTheme;
        #endregion

        //Enumération des différents états du jeu
        GameState gameState;
        public enum GameState
        {
            GameOver,
            MainMenu,
            GameRunning
        }

        //LE PROGRAMME VA LA EN PREMIER ENCULE
        public Main()
        {
            //Initialisation des objets / classes
            gameState = GameState.MainMenu;
            graphics = new GraphicsDeviceManager(this);
            
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
            _menuBackground = Content.Load<Texture2D>("menu_background");
            _menuTheme = Content.Load<Song>("menu_theme");
            mainMenu = new MainMenu(_menuBackground, _menuTheme);
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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
