using System;
using System.Collections.Generic;
using System.Text;

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
        //Permet les actions sous condition (si le bouton est le bouton options.. etc)
        public string ButtonName { get; set; }
        public bool btnClick = false; //Booléen de condition rapide (actions pendant clic, avant que l'utilisateur relève le doigt du clic)
        public bool btnClicked = false; //Booléen de condition persistante (reste dans l'état après le clic)

        public Texture2D TextureNormal{ get; set; }
        public Texture2D TexturePressed { get; set; }

        public Texture2D TextureDisplayed { get; set; }
        public Rectangle BtnRectangle { get; set; }
        public Vector2 Position { get; set; }

        private SoundEffect ClickSound { get; set; }
        //Gère l'instance du son. Permet de jouer le son de A à Z.
        public SoundEffectInstance ClickSoundInstance { get; set; }

        public bool btnContainsMouse = false;

        //Les propriétés du bouton sont définies dans le constructeur et non pas dans une
        //LoadContent() contrairement à GUIMenu par exemple, car les boutons seront très souvent
        //différents les uns des autres, alors que les GUIMenu auront toujours le même background,
        //même son d'ouverture / slide / fermeture, etc.
        public Button(string name, Vector2 btnPos, Texture2D normal, Texture2D pressed,
            SoundEffect clickSound)
        {
            //Attribution des paramètres aux propriétés
            this.ButtonName = name;
            this.Position = btnPos;
            this.TextureNormal = normal;
            this.TexturePressed = pressed;
            //Displayed texture is normal by default
            this.TextureDisplayed = this.TextureNormal;

            //L'initialisation du rectangle se fait une fois (pas dans le Update)
            //car la position du bouton est fixe. Elle ne bouge pas.
            BtnRectangle = new Rectangle((int)Position.X, (int)Position.Y,
                TextureDisplayed.Width, TextureDisplayed.Height);

            //Définie l'instance du BtnClickSound.
            ClickSound = clickSound;
            ClickSoundInstance = ClickSound.CreateInstance();
        }

        //Méthode LoadContent inutilisée pour les Boutons car les boutons sont 
        public virtual void LoadContent(ContentManager Content)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            //Si le curseur est sur le bouton
            if (BtnRectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                btnContainsMouse = true;

                //Si on clique sur le bouton
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    //La texture du bouton affichée devient la texture Pressed
                    TextureDisplayed = TexturePressed;
                    btnClick = true; //Booléen pour certaines conditions
                    btnClicked = true;
                    ClickSoundInstance.Volume = 0.55f; //Set du volume du son du clic
                    ClickSoundInstance.Play(); //Play le son du clic
                }
                if (Mouse.GetState().LeftButton == ButtonState.Released) //On relâche le bouton (post clic)
                {
                    TextureDisplayed = TextureNormal; //La texture redevient normale
                    btnClick = false; //Booléen passé à faux
                }
            }
            else //Si le curseur est hors du bouton
            {
                btnContainsMouse = false;
                //La texture est de type "normal"
                TextureDisplayed = TextureNormal; //Evite certains bugs / erreurs
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureDisplayed, Position, Color.White);
        }

        //Pas de DrawFadeIn / DrawFadeOut. C'est géré par l'environnement dans lequel cette méthode
        //est appelée, et donc par les r,g,b passés en paramètres.
        public virtual void DrawFade(GameTime gameTime, SpriteBatch spriteBatch, int r, int g, int b)
        {
            double delayBeforeFade = 0.010; //2 secondes de délai car .ElapsedGameTime = temps depuis le dernier appel de Update();
            //et 1 seconde / 60 frames ~= 0.016 => 0.016 * 2 ~= 0.033
            delayBeforeFade -= gameTime.ElapsedGameTime.TotalSeconds;

            if (delayBeforeFade <= 0) //Quand le délai atteint 0 (ou est inférieur à 0, pour éviter les erreurs)
            {
                spriteBatch.Draw(TextureDisplayed, Position, new Color(r, g, b));
            }
        }

    }
}
