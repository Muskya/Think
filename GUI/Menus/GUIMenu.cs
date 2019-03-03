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
        //Internal private and sealed class just to handle the automatic
        //add of buttons to a button list (use for update() & draw()) when
        //the creation of a new button happens
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
        private Texture2D GUIMenuBackground;
        private Vector2 GUIMenuBackgroundPosition;

        //SFX
        private SoundEffect GUIMenuOpeningSound, GUIMenuClosingSound, GUIMenuSlidingSound;
        public SoundEffectInstance GUIMenuOpeningInstance, GUIMenuClosingInstance, GUIMenuSlidingInstance;
        public bool isOpening = false, isClosing = false, isOpened = false; //isOpened passe à true lorsque l'utilisatuer
        //clique sur le bouton d'ouverture du menu (cela peut être depuis le main menu, ou depuis ingame)

        //Buttons
        //Liste incrémentée dès qu'un GUIMenuButton est créé (constructeur)
        public static List<Button> guiMenuBtnPanel = new List<Button>();
        public Button CloseButton; //Bouton de fermeture du menu (présent dans tous les menus)

        public GUIMenu()
        {
            
        }

        //Load le content du menu de base. 
        public virtual void LoadContent(ContentManager Content)
        {
            //GUI Menu main background for almost any GUI Menu
            this.GUIMenuBackground = Content.Load<Texture2D>("Graphics/guimenu_background_darksand");
            //Centers the background on the screen
            GUIMenuBackgroundPosition = RandomManager.CenterElementOnScreen(GUIMenuBackground,
                ref GUIMenuBackgroundPosition);

            //Load les SFX de Gui Menus
            this.GUIMenuOpeningSound = Content.Load<SoundEffect>("SFX/gui_menu_opening_1");
            this.GUIMenuClosingSound = Content.Load<SoundEffect>("SFX/gui_menu_closing_1");
            this.GUIMenuSlidingSound = Content.Load<SoundEffect>("SFX/gui_menu_sliding_1");
            //Initialise les instances de SFX
            GUIMenuOpeningInstance = GUIMenuOpeningSound.CreateInstance();
            GUIMenuClosingInstance = GUIMenuClosingSound.CreateInstance();
            GUIMenuSlidingInstance = GUIMenuSlidingSound.CreateInstance();

            //Initialisation des boutons propres à n'importe quel GUI Menu
            //Close, autres ...
            CloseButton = new GUIMenuButtons("close", new Vector2(
                ((this.GUIMenuBackground.Width - 200) + ((Main.screenWidth - this.GUIMenuBackground.Width))/2), //Position générique du bouton par rapport au menu
                ((this.GUIMenuBackground.Height - 60) + ((Main.screenHeight - this.GUIMenuBackground.Height))/2)), //Position générique du bouton par rapport au menu
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
            //Le menu n'est update que si il est ouvert
            //(S'ouvre par exemple à la suite d'un clic sur le bouton dans le main menu)
            if (this.isOpened)
            {
                //Update tous les boutons du Gui Menu
                //(ajoutés depuis la classe interne GuiMenuButtons présente dans cette classe)
                for (int i = 0; i < guiMenuBtnPanel.Count; i++)
                {
                    guiMenuBtnPanel[i].Update(gameTime);
                }

                if (this.CloseButton.btnClick) //Si on a cliqué sur le bouton Close
                {
                    this.isOpened = false; //isOpened passe à false, donc plus d'Update()/Draw()
                }
            }
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw le background du GUI Menu (le même pour tous les menus),
            //et draw chaque bouton correspondant à ce menu
            if (this.isOpened) //Si le menu doit s'ouvrir 
            {
                spriteBatch.Draw(GUIMenuBackground, GUIMenuBackgroundPosition, Color.White);
                for (int i = 0; i < guiMenuBtnPanel.Count; i++)
                {
                    guiMenuBtnPanel[i].Draw(gameTime, spriteBatch);
                }
            } 
        }

        public virtual void DrawFade(GameTime gameTime, SpriteBatch spriteBatch, int r, int g, int b)
        {
            double delayBeforeFade = 0.010; // ~< 1seconde
            delayBeforeFade -= gameTime.ElapsedGameTime.TotalSeconds;

            if (delayBeforeFade <= 0)
            {
                spriteBatch.Draw(GUIMenuBackground, GUIMenuBackgroundPosition, new Color(r, g, b));
            }
        }
    }
}
