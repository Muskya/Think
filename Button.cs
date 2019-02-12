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
    class Button
    {   
        public Texture2D BtnTextNormal{ get; set; }
        public Texture2D BtnTextHovered { get; set; }
        public Texture2D BtnTextPressed { get; set; }

        public Texture2D BtnTexture { get; set; }
        public Rectangle BtnRectangle { get; set; }
        public Vector2 BtnPosition { get; set; }

        public MouseState MouseState { get; set; }
        public bool btnClicked = false;

        enum BtnState
        {
            Normal,
            Hovered,
            Pressed
        }
        BtnState buttonState = BtnState.Normal;

        public Button ()
        { }

        public void BtnStateManager()
        {
            MouseState = Mouse.GetState();
            
            //Si le bouton contient le curseur
            if (BtnRectangle.Contains(MouseState.X, MouseState.Y))
            {
                //La texture est de type "hovered"
                BtnTexture = BtnTextHovered;
                buttonState = BtnState.Hovered;

                //Si on clique sur le bouton
                if (MouseState.LeftButton == ButtonState.Pressed)
                    btnClicked = true;

            } else //Si le curseur sort du bouton
            {
                //La texture est de type "normal"
                BtnTexture = BtnTextNormal;
                buttonState = BtnState.Normal;

                btnClicked = false;
            }

            //Si on a cliqué sur le bouton
            if (btnClicked)
                //La texture est de type "Pressed"
                BtnTexture = BtnTextPressed;
                buttonState = BtnState.Pressed;
        }

        public void Update(GameTime gameTime)
        {
            BtnStateManager();

            //L'initialisation du rectangle reste statique (pas dans le Update)
            //car la position du bouton est connue à l'avance, il ne bouge pas.
            BtnRectangle = new Rectangle((int)BtnPosition.X, (int)BtnPosition.Y,
                BtnTexture.Width, BtnTexture.Height);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BtnTexture, new Vector2(50, 50), Color.White);
        }
    }
}
