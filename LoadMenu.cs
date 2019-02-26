using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Think
{
    sealed class LoadMenu : GUIMenu
    {
        public LoadMenu()
        {

        }

        public sealed override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content); //Load les éléments de base d'un GUIMenu

            //Load les éléments spécifiques au menu de loading
        }

        public sealed override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public sealed override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public sealed override void DrawFade(GameTime gameTime, SpriteBatch spriteBatch, int r, int g, int b)
        {
            base.DrawFade(gameTime, spriteBatch, r, g, b);
        }
    }
}
