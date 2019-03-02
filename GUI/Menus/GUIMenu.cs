﻿using System;
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
        //Internal private and sealed class just to handle the automatic
        //add of buttons to a button list (use for update() & draw()) when
        //the creation of a new one happens
        private sealed class GUIMenuButtons : Button
        {            
            public GUIMenuButtons(string name, Vector2 btnPos, Texture2D normal, Texture2D pressed,
                SoundEffect clickSound)
                : base(name, btnPos, normal, pressed, clickSound)
            {
                guiMenuBtnPanel.Add(this);
            }
        }

        //Graphics
        private Texture2D _menuBackground;
        private Vector2 _menuBackgroundPosition;

        //SFX
        private SoundEffect _menuOpeningSound, _menuClosingSound, _menuSlidingSound;
        public SoundEffectInstance _openingInstance, _closingInstance, _slidingInstance;
        public bool isOpening = false, isOpened = false;

        //Menu buttons
        
        //Liste incrémentée dès qu'un GUIMenuButton est créé (constructeur)
        public static List<Button> guiMenuBtnPanel = new List<Button>();
        public Button closeBtn;

        public GUIMenu()
        {
            
        }

        //Load le content du menu de base. 
        public virtual void LoadContent(ContentManager Content)
        {
            //GUI Menu main background for almost any GUI Menu
            this._menuBackground = Content.Load<Texture2D>("Graphics/guimenu_background_darksand");
            //Centers the background on the screen
            _menuBackgroundPosition = RandomManager.CenterElementOnScreen(_menuBackground,
                ref _menuBackgroundPosition);

            //Load les SFX de Gui Menus
            this._menuOpeningSound = Content.Load<SoundEffect>("SFX/gui_menu_opening_1");
            this._menuClosingSound = Content.Load<SoundEffect>("SFX/gui_menu_closing_1");
            this._menuSlidingSound = Content.Load<SoundEffect>("SFX/gui_menu_sliding_1");
            //Initialise les instances de SFX
            _openingInstance = _menuOpeningSound.CreateInstance();
            _closingInstance = _menuClosingSound.CreateInstance();
            _slidingInstance = _menuSlidingSound.CreateInstance();

            //Initialisation des boutons propres à n'importe quel GUI Menu
            //Close, autres ...
            closeBtn = new GUIMenuButtons("close", new Vector2(
                ((this._menuBackground.Width - 200) + ((Main.screenWidth - this._menuBackground.Width))/2),
                ((this._menuBackground.Height - 60) + ((Main.screenHeight - this._menuBackground.Height))/2)),
                Content.Load<Texture2D>("Graphics/Buttons/small_closeBtnNormal"),
                Content.Load<Texture2D>("Graphics/Buttons/small_closeBtnPressed"),
                Content.Load<SoundEffect>("SFX/gui_button_close_1"));
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
            //Update autre chose du menu ? Sliders, autres choix idk...

            //Update tous les boutons du Gui Menu
            for (int i = 0; i < guiMenuBtnPanel.Count; i++)
            {
                guiMenuBtnPanel[i].Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw le background du GUI Menu (le même pour tous les menus),
            //et draw chaque bouton correspondant à ce menu
            spriteBatch.Draw(_menuBackground, _menuBackgroundPosition, Color.White);
            for (int i = 0; i < guiMenuBtnPanel.Count; i++)
            {
                guiMenuBtnPanel[i].Draw(gameTime, spriteBatch);
            }
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