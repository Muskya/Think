using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Think
{
    //Classe uniquement présente pour avoir une liste de boutons propre au Main menu. Bad practice je pense
    class MainMenuButtons : Button
    {
        //Liste accueillant tous les boutons du panel de boutons du Main Menu
        public static List<MainMenuButtons> mainMenuButtonsList = new List<MainMenuButtons>();

        public MainMenuButtons(string name, Vector2 btnPos, Texture2D normal, Texture2D pressed,
            SoundEffect clickSound, GUIMenu menuToOpen)
            :base(name, btnPos, normal, pressed, clickSound, menuToOpen)
        {
            mainMenuButtonsList.Add(this); //Ajout du bouton instancié à la liste (menus Play, Load et Options)
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);      
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void DrawFade(GameTime gameTime, SpriteBatch spriteBatch, int r, int g, int b)
        {
            base.DrawFade(gameTime, spriteBatch, r, g, b);
        }

    }
}
