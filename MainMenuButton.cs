﻿using System;
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
    class MainMenuButton : Button
    {
        public static List<MainMenuButton> menuPanel = new List<MainMenuButton>();

        public MainMenuButton(Vector2 btnPos, Texture2D normal, Texture2D pressed,
            SoundEffect clickSound)
            :base(btnPos, normal, pressed, clickSound)
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
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void DrawFadeIn(GameTime gameTime, SpriteBatch spriteBatch, int r, int g, int b)
        {
            base.DrawFadeIn(gameTime, spriteBatch, r, g, b);
        }

    }
}
