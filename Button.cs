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
        public Texture2D TextureNormal{ get; set; }
        public Texture2D TextureHovered { get; set; }
        public Texture2D TexturePressed { get; set; }

        public Texture2D _texture { get; set; }
        public Rectangle _rectangle { get; set; }
        public Vector2 _position { get; set; }
        public bool btnClicked = false; //Permet les transitions texturales
        
        enum BtnState
        {
            Normal,
            Hovered,
            Pressed
        }
        BtnState buttonState = BtnState.Normal;

        public Button (Vector2 btnPos, Texture2D normal, Texture2D hovered,
               Texture2D pressed)
        {
            //Attribution des paramètres aux propriétés
            this._position = btnPos;
            this.TextureNormal = normal;
            this.TextureHovered = hovered;
            this.TexturePressed = pressed;

            //L'initialisation du rectangle se fait une fois (pas dans le Update)
            //car la position du bouton est fixe. Elle ne bouge pas.
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y,
                _texture.Width, _texture.Height);
        }

        //Gère l'état textural et logique du bouton.
        public void BtnStateManager()
        {
            //Si le bouton contient le curseur
            if (_rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                //La texture est de type "hovered"
                _texture = TextureHovered;
                buttonState = BtnState.Hovered;

                //Si on clique sur le bouton
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    btnClicked = true;

            } else //Si le curseur sort du bouton
            {
                //La texture est de type "normal"
                _texture = TextureNormal;
                buttonState = BtnState.Normal;

                btnClicked = false;
            }

            //Si on a cliqué sur le bouton
            if (btnClicked)
                //La texture est de type "Pressed"
                _texture = TexturePressed;
                buttonState = BtnState.Pressed;    
        }

        public void Update(GameTime gameTime)
        {
            BtnStateManager();

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
