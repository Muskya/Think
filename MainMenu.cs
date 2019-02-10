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
    class MainMenu
    {
        private Texture2D _backgroundImage;
        private SpriteFont _mainFont;

        ContentHandler _mainMenuContent;

        public MainMenu(ContentHandler content)
        {
            //Charge le nouveau content
            _mainMenuContent = content;

            _backgroundImage = _mainMenuContent.Load<Texture2D>("menu_background");
            _mainFont = _mainMenuContent.Load<SpriteFont>("ariaFont");
        }

        public void Update(GameTime gt)
        {

        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(_backgroundImage, new Vector2(0, 0), Color.White);
            sb.DrawString(_mainFont, "THINK.",
                new Vector2(_backgroundImage.Width / 2, (_backgroundImage.Height / 2 - 50)),
                Color.White);

        }
    }
}
