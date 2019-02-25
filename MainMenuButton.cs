using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Think
{
    class MainMenuButton : Button
    {
        public static List<MainMenuButton> menuPanel = new List<MainMenuButton>();

        public MainMenuButton(string name, Vector2 btnPos, Texture2D normal, Texture2D pressed,
            SoundEffect clickSound)
            :base(name, btnPos, normal, pressed, clickSound)
        {
            menuPanel.Add(this);
        }

        public override void ButtonManager()
        {
            base.ButtonManager();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.btnClicked)
            {
                switch (this._buttonName)
                {
                    case "playbtn":

                }
            }
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void DrawFade(GameTime gameTime, SpriteBatch spriteBatch, int r, int g, int b)
        {
            base.DrawFade(gameTime, spriteBatch, r, g, b);
        }

    }
}
