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
using Microsoft.Xna.Framework.Input;

namespace Think
{
    abstract class GUIMenu
    {
        //Graphics
        private Texture2D _menuBackground;
        private Vector2 _menuBackgroundPosition;

        //SFX
        private SoundEffect _menuOpeningSound, _menuClosingSound, _menuSlidingSound;
        public SoundEffectInstance _openingInstance, _closingInstance, _slidingInstance;
        public bool isOpening = false, isOpened = false;
        
        public GUIMenu()
        {

        }

        //Load le content du menu de base. 
        public virtual void LoadContent(ContentManager Content)
        {
            this._menuBackground = Content.Load<Texture2D>("Graphics/guimenu_background_darksand");
            //Centers the background on the screen
            _menuBackgroundPosition = RandomManager.CenterTextureOnScreen(_menuBackground,
                ref _menuBackgroundPosition);

            //Load les sons
            this._menuOpeningSound = Content.Load<SoundEffect>("SFX/gui_menu_opening_1");
            this._menuClosingSound = Content.Load<SoundEffect>("SFX/gui_menu_closing_1");
            this._menuSlidingSound = Content.Load<SoundEffect>("SFX/gui_menu_sliding_1");
            //Initialise les instances de SFX
            _openingInstance = _menuOpeningSound.CreateInstance();
            _closingInstance = _menuClosingSound.CreateInstance();
            _slidingInstance = _menuSlidingSound.CreateInstance();
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
            spriteBatch.Draw(_menuBackground, _menuBackgroundPosition, Color.White);
        }

        public virtual void DrawFade(GameTime gameTime, SpriteBatch spriteBatch, int r, int g, int b)
        {
            double fadeDelay = 0.010;
            fadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

            if (fadeDelay <= 0)
            {
                spriteBatch.Draw(_menuBackground, _menuBackgroundPosition, new Color(r, g, b));
            }
        }
    }
}
