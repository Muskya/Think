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
        #region Engine Relative
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        #endregion

        #region Propriétés de jeu
        float screenHeight, screenWidth;
        #endregion

        //Références
        MainMenu mainMenu;

        //Game States (more to come ?)
        GameState gameState;
        public enum GameState
        {
            MainMenu,
            GameRunning,
            GameOver
        }

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);

            //Class references
            mainMenu = new MainMenu();
            
            //Content related stuff
            Content.RootDirectory = "Content";
        }

        //BASE.INITIALIZE() CALLS LOADCONTENT() && UNLOADCONTENT()
        protected override void Initialize()
        {
            //Screen relative
            #region Screen relative

            //Engine relative
            gameState = GameState.MainMenu;

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

            //Load le content du main menu
            mainMenu.LoadContent(this.Content);
            
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

            //Draw le MainMenu. (Mention Monogame + Title Screen)
            mainMenu.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
