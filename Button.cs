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
    abstract class Button
    {   
        public Texture2D TextureNormal{ get; set; }
        public Texture2D TexturePressed { get; set; }

        public Texture2D _texture { get; set; }
        public Rectangle _rectangle { get; set; }
        public Vector2 _position { get; set; }
        public bool btnClicked = false; //Permet les transitions texturales
        
        enum BtnState
        {
            Normal,
            Pressed
        }
        BtnState buttonState = BtnState.Normal; 

        public Button (Vector2 btnPos, Texture2D normal, Texture2D pressed)
        {
            //Attribution des paramètres aux propriétés
            this._position = btnPos;
            this.TextureNormal = normal;
            this.TexturePressed = pressed;
            //Displayed texture is normal by default
            this._texture = this.TextureNormal;

            //L'initialisation du rectangle se fait une fois (pas dans le Update)
            //car la position du bouton est fixe. Elle ne bouge pas.
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y,
                _texture.Width, _texture.Height);
        }

        //Gère l'état textural et logique du bouton.
        public virtual void BtnStateManager()
        {
            //Si le curseur est sur le bouton
            if (_rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                //Si on clique sur le bouton
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    _texture = TexturePressed;
                    btnClicked = true;
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    _texture = TextureNormal;
                    //btnClicked = false; A voir, quand le menu correspondant au
                    //bouton se lancera
                }
            }
            else //Si le curseur est hors du bouton
            {
                //La texture est de type "normal"
                _texture = TextureNormal;
                buttonState = BtnState.Normal;

                btnClicked = false;
            } 
        }

        public virtual void Update(GameTime gameTime)
        {
            BtnStateManager();
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
