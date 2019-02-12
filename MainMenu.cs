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
        public Texture2D BackgroundMG { get; set; }
        //Layers displaying conditional booleans
        public bool mgFadedOut = false, mgFadeIn = false;

        public Texture2D BackgroundImage { get; set; }
        //Délai d'apparaition. 1Sec/60fps = 0.016 donc ~2 sec de délai
        private double imgFadeDelay = .010;
        //Valeurs décimales de couleur. a = alpha 
        //(0, 0, 0) = Noir && (255, 255, 255) = White. <= L'image s'affiche normalement
        private int r = 0, g = 0, b = 0;

        public Song BackgroundTheme { get; set; }
        private float themeVolume = 0.45f;
        private bool themeLooping = true;

        public SpriteFont ArialDebugFont { get; set; }

        public MainMenu()
        { }

        //Called just once before Update()
        public void BeginRun()
        {
            //Menu theme
            #region Menu theme
            MediaPlayer.IsRepeating = themeLooping; //Boucle la musique
            MediaPlayer.Volume = themeVolume; //Default 0.45f
     
            MediaPlayer.Play(BackgroundTheme);
            #endregion

            #region Menu background

            #endregion
        }

        public void Update(GameTime gameTime)
        {
            //Décrémente le délai de fade. gameTime.ElapsedGameTime =
            //intervalle de temps depuis le dernier appel de Update(). 
            //Donc ici, .TotalSeconds renverra 1 seconde / 60 frames = ~ 0.016.
            imgFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            #region FadeIn/Out MG + Menu
            if (imgFadeDelay <= 0) //
            {
                if (!mgFadeIn) //Si la mention n'est pas apparue
                {
                    //Affiche la mention MonoGame (passe au blanc)
                    r++; g++; b++;

                    if (r >= 310 && g >= 310 && b >= 310)
                        mgFadeIn = true;
                }

                if (mgFadeIn) //Si la mention est apparue
                {
                    //Fais disparaître la mention MonoGame (passe au noir)
                    r--; g--; b--;

                    if (r <= 0 && g <= 0 && b <= 0)
                    {
                        mgFadeIn = false; //On est plus en train d'afficher la mention
                        mgFadedOut = true; //La mention est terminée
                    }
                }

                if (mgFadedOut) //Si la mention est terminée
                {
                    //On repasse au blanc pour afficher le Main Menu
                    r++; g++; b++;

                    //Reset du delay
                    imgFadeDelay = 0.010;
                }
            }
            #endregion

            

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Affichage de la mention MonoGame selon les conditions de Update()
            spriteBatch.Draw(BackgroundMG, Vector2.Zero, new Color(r, g, b));

            //Si la mention MonoGame a fini d'être affichée
            if (mgFadedOut) {
                //Affiche le menu mrincipal
                spriteBatch.Draw(BackgroundImage, Vector2.Zero, new Color(r, g, b));

                //Affichage boutons, options, logos, other...
            }

            //Debug stuff
            #region Debug
            spriteBatch.DrawString(ArialDebugFont, imgFadeDelay.ToString(), Vector2.Zero, Color.Green);
            spriteBatch.DrawString(ArialDebugFont, gameTime.ElapsedGameTime.TotalSeconds.ToString(),
                new Vector2(0, 20), Color.Green);
            spriteBatch.DrawString(ArialDebugFont, String.Format("r: {0}, g: {1}, b: {2}", r, g, b),
                new Vector2(0, 40), Color.Black);
            #endregion 
        }
        
    }
}
