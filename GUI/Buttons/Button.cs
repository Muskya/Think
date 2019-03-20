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
        public bool btnClick = false; //True quand on clic / laisse appuyé; False quand on relâche
        public bool doBtnAction = false; //Change d'état à chaque clic sur le bouton

        //Gère les différents états de la souris pour effectuer les actions relatives au bouton une seule fois 
        public MouseState currentMouseState;
        public MouseState lastMouseState;

        public GUIMenu menuToOpen;

        public Texture2D TextureNormal{ get; set; }
        public Texture2D TexturePressed { get; set; }

        public Texture2D TextureDisplayed { get; set; }
        public Rectangle BtnRectangle { get; set; }
        public Vector2 Position { get; set; }

        private SoundEffect ClickSound { get; set; }
        //Gère l'instance du son. Permet de jouer le son de A à Z.
        public SoundEffectInstance ClickSoundInstance { get; set; }

        //Les propriétés du bouton sont définies dans le constructeur et non pas dans une
        //LoadContent() contrairement à GUIMenu par exemple, car les boutons seront très souvent
        //différents les uns des autres, alors que les GUIMenu auront toujours le même background,
        //même son d'ouverture / slide / fermeture, etc.
        public Button(string name, Vector2 btnPos, Texture2D normal, Texture2D pressed,
            SoundEffect clickSound, GUIMenu menuToOpen)
        {
            //Attribution des paramètres aux propriétés
            this.ButtonName = name;
            this.Position = btnPos;
            this.TextureNormal = normal;
            this.TexturePressed = pressed;
            //Displayed texture is normal by default
            this.TextureDisplayed = this.TextureNormal;

            //Indique au bouton quel est le menu à ouvrir lorsqu'il est cliqué
            this.menuToOpen = menuToOpen;

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
                currentMouseState = Mouse.GetState();
                
                //Si on single cli sur le bouton et que l'état change dès qu'on release
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    btnClick = true;
                } else if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    btnClick = false;
                }

                //Si on clique sur le bouton (et que l'état du bouton doit reste)
                if (currentMouseState.LeftButton == ButtonState.Pressed &&
                    lastMouseState.LeftButton == ButtonState.Released)
                {
                    //La texture du bouton affichée devient la texture Pressed
                    TextureDisplayed = TexturePressed;
                    doBtnAction = !doBtnAction; //Booléen pour certaines conditions Avant/Après (passe à false pendant le released)
                    ClickSoundInstance.Volume = 0.55f; //Set du volume du son du clic
                    ClickSoundInstance.Play(); //Play le son du clic
                }

                lastMouseState = currentMouseState; //Si on a laissé appuyé sur le bouton, lastMouseState devient
                //ButtonState.Pressed et donc on entre pas dans la première condition
            } else //Si le curseur est hors du bouton
            {
                //La texture est de type "normal"
                TextureDisplayed = TextureNormal; //Evite certains bugs / erreurs
            }

            //Si on a activé l'action du bouton
            if (doBtnAction)
            {
                TextureDisplayed = TexturePressed; //Le bouton reste enfoncé pendant que l'on intéragis avec l'action du bouton
            }
            else
            {
                TextureDisplayed = TextureNormal; //Sinon la texture redeveitn normale
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
