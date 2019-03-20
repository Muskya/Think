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
        //Relatif au framework
        #region Monogame Relative
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        #endregion

        #region Propriétés de jeu
        public static float screenHeight, screenWidth;
        #endregion

        GameManager.GameState gameState;

        //Références
        MainMenu mainMenu;
        Level levelOne;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);

            //Class references
            mainMenu = new MainMenu();
            levelOne = new LevelOne();

            //Content related stuff
            Content.RootDirectory = "Content";
        }

        //BASE.INITIALIZE() CALLS LOADCONTENT() && UNLOADCONTENT()
        protected override void Initialize()
        {
            //Engine relative
            gameState = GameManager.GameState.MainMenu;

            //Screen relative
            #region Screen relative

            //GPU 
            graphics.HardwareModeSwitch = false;

            //Game window
            //Graphics.ToggleFullScreen();
            screenHeight = 720; screenWidth = 1280;
            graphics.PreferredBackBufferHeight = (int)screenHeight;
            graphics.PreferredBackBufferWidth = (int)screenWidth;
            Window.Title = "THINK";
            Window.IsBorderless = false;
            Window.AllowUserResizing = false;
            Window.AllowAltF4 = true;
            //Others
            IsMouseVisible = true;

            //Needed for any modification
            graphics.ApplyChanges();
            #endregion

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load le content du main menu
            if (gameState == GameManager.GameState.MainMenu)
                mainMenu.LoadContent(this.Content);

            //Load le content des levels uniquement quand c'est nécessaire ?
            //(Lazy Loading)

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

            switch (gameState)
            {
                case GameManager.GameState.MainMenu:
                    //Draw le MainMenu. (Mention Monogame + Title Screen)
                    mainMenu.Update(gameTime);
                    break;
                case GameManager.GameState.GameRunning:
                    levelOne.Update(gameTime);
                    break;
                case GameManager.GameState.GameOver:
                    mainMenu.Update(gameTime); //à modifier, on va pas reDraw le main menu à chaque mort.
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            switch (gameState)
            {
                case GameManager.GameState.MainMenu:
                    //Draw le MainMenu. (Mention Monogame + Title Screen)
                    mainMenu.Draw(gameTime, spriteBatch);
                    break;
                case GameManager.GameState.GameRunning:
                    levelOne.Draw(gameTime, spriteBatch);
                    break;
                case GameManager.GameState.GameOver:
                    mainMenu.Draw(gameTime, spriteBatch); //à modifier, on va pas reDraw le main menu à chaque mort.
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
