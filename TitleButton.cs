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
    class TitleButton : Button
    {
        public static List<TitleButton> menuPanel = new List<TitleButton>();

        public TitleButton(Vector2 btnPos, Texture2D normal, Texture2D pressed)
            :base(btnPos, normal, pressed)
        {
            menuPanel.Add(this);
        }

        public override void BtnStateManager()
        {
            base.BtnStateManager();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
