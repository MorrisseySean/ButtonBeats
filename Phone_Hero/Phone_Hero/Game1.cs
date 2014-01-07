using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Phone_Hero
{    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SoundManager sound;
        SpriteBatch spriteBatch;
        Texture2D background;
        Board board;
        Menu menu;
        enum GameState
        {
            Mainmenu,
            Game,
            Creation
        }

        GameState gamestate;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            sound = new SoundManager();
            Content.RootDirectory = "Content";
            gamestate = GameState.Mainmenu;

            //set orientation to portrait
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;    
            
            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);           
            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }
        
        protected override void Initialize()
        {
            //Enable required gestures
            TouchPanel.EnabledGestures = GestureType.VerticalDrag | GestureType.DragComplete;
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            board = new Board();
            menu = new Menu();
            ResourceManager.Load(Content);           
            background = ResourceManager.BACKGROUND;
        }

        void LoadGame(int songSelection)
        {
            ResourceManager.LoadGameAssets(Content);
            ResourceManager.LoadMusicFile(songSelection, Content);
            Beatmaps.Load();
            board.LoadBoard(songSelection, false);            
            sound.Play(songSelection);
        }

        void LoadCreation(int songSelection)
        {
            ResourceManager.LoadGameAssets(Content);
            ResourceManager.LoadMusicFile(songSelection - 100, Content);
            //Beatmaps.Load();
            board.LoadBoard(songSelection - 100, true);
            sound.Play(songSelection - 100);
        }

        protected override void UnloadContent()
        {
        }        
        
        protected override void Update(GameTime gameTime)
        {
            // Deals with the player pressing the back button
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (gamestate == GameState.Mainmenu)
                {
                    if (menu.GoBack() == true)
                    {
                        this.Exit();
                    }
                }
                else if (gamestate == GameState.Game)
                {
                    sound.Stop();
                    gamestate = GameState.Mainmenu;                    
                }
            }            

            //Deals with menu options
            if (gamestate == GameState.Mainmenu)
            {
                int selected = menu.Update();
                if (selected == -2)
                {
                    this.Exit();
                }
                else if (selected > 99)
                {
                    gamestate = GameState.Creation;
                    LoadCreation(selected);
                }
                else if (selected > -1)
                {
                    gamestate= GameState.Game;
                    LoadGame(selected);
                }
                while (TouchPanel.IsGestureAvailable)
                {
                    menu.UpdateScroll(TouchPanel.ReadGesture());
                }
            }
            else if (gamestate == GameState.Game)
            {
                board.Update();
                if (board.Completed() == true)
                {
                    //TO DO:Save score
                    gamestate = GameState.Mainmenu;
                }
            }
            else if (gamestate == GameState.Creation)
            {
                board.Update();
            }
            base.Update(gameTime);
        }
       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            if (gamestate == GameState.Mainmenu)
            {
                menu.Draw(spriteBatch);
            }
            else if (gamestate == GameState.Game||gamestate == GameState.Creation)
            {
                spriteBatch.DrawString(ResourceManager.SCOREFONT, Convert.ToString(board.getScore()), new Vector2(40, 80), Color.Yellow);
                board.Draw(spriteBatch);
            }           
            spriteBatch.End();
            base.Draw(gameTime);
        }        
    }
}
