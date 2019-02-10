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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Logique d'éxecution
        ContentHandler content;
        MainMenu mainMenu;

        //Enumération des différents états du jeu
        GameState gameState;
        public enum GameState
        {
            GameOver,
            MainMenu,
            GameRunning
        }

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);

            //Content related stuff
            Content.RootDirectory = "Content";
            content = new ContentHandler(Content.ServiceProvider, Content.RootDirectory);
        }

        protected override void Initialize()
        {
            //Initialisation des objets / classes
            gameState = GameState.MainMenu;
            mainMenu = new MainMenu(content);

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

    
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch(gameState)
            {
                case GameState.MainMenu:
                    
                    break;
                case GameState.GameRunning:

                    break;
                case GameState.GameOver:

                    break;

            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            mainMenu.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
