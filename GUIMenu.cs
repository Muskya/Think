using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Think
{
    abstract class GUIMenu
    {
        //Graphics
        private Texture2D _menuBackground;

        //SFX
        private SoundEffect _menuOpeningSound, _menuClosingSound, _menuSlidingSound;

        public bool isDisplayed = false;
        
        public GUIMenu()
        {
            
        }

        //Load le content du menu de base. 
        public virtual void LoadContent(ContentManager Content)
        {
            this._menuBackground = Content.Load<Texture2D>("Graphics/menu_background");

            this._menuOpeningSound = Content.Load<SoundEffect>("SFX/gui_menu_opening_1");
            this._menuClosingSound = Content.Load<SoundEffect>("SFX/gui_menu_closing_1");
            this._menuSlidingSound = Content.Load<SoundEffect>("SFX/gui_menu_sliding_1");
        }

        //A élaborer
        protected virtual void SwitchMenu()
        {

        }

        //Les méthodes de XNA (Load, Update, Draw etc) doivent toujours être en public. Si je veux les mettre
        //en protected parce-que j'ai une classe fille de GUIMenu, je pourrai pas dans une autre
        //classe utiliser cette méthode sur une instance de la classe mère ou fille.
        //Sachant que ça arrive tout le temps d'appeler les Update() et Draw() de l'instance
        //d'une classe depuis d'autres classes, je dois les laisser en public.
        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._menuBackground, Vector2.Zero, Color.White);
        }
    }
}
