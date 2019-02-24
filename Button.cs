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

        public SoundEffect _clickSound { get; set; }
        //Gère l'instance du son. Permet de jouer le son de A à Z.
        private SoundEffectInstance _clickSoundInstance;

        public bool btnClicked = false; //Permet les transitions texturales

        enum BtnState
        {
            Normal,
            Pressed
        }
        BtnState buttonState = BtnState.Normal;

        public Button(Vector2 btnPos, Texture2D normal, Texture2D pressed,
            SoundEffect clickSound)
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

            //Définie l'instance du btnClickSound.
            _clickSound = clickSound;
            _clickSoundInstance = _clickSound.CreateInstance();
        }

        //Gère tout sur le bouton. Cela aurait pu être écrit dans 
        //le Update() tout est regroupé dans une méthode pour plus de
        //clarté dans le Update().
        public virtual void ButtonManager()
        {
            //Si le curseur est sur le bouton
            if (_rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                //Si on clique sur le bouton
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    _texture = TexturePressed;
                    btnClicked = true;
                    //_clickSoundInstance.Volume = 0.5f | Lower volume ?
                    _clickSoundInstance.Play();
                } 
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    _texture = TextureNormal;
                    btnClicked = false;
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
            ButtonManager();          
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        //Pas de fade-in / fade-out. C'est géré par l'environnement dans lequel cette méthode
        //est appelée, et donc par les r,g,b passés en paramètres.
        public virtual void DrawFade(GameTime gameTime, SpriteBatch spriteBatch, int r, int g, int b)
        {
            double fadeDelay = 0.010;
            fadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

            if (fadeDelay <= 0)
            {
                spriteBatch.Draw(_texture, _position, new Color(r, g, b));
            }
        }

    }
}
