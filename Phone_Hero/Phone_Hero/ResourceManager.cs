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
    static class ResourceManager
    {        
        static public Texture2D[] BUTTON = new Texture2D[6];
        static public Texture2D[] PRESSED = new Texture2D[6];
        static public Texture2D[] RING = new Texture2D[6];
        static public String[] SONGFILES = new String[10];
        static public String[] SONGTITLES = new String[1];
        static public Song[] MUSICFILES = new Song[1];
        static public Texture2D[] POWERBAR = new Texture2D[2];
        static public SpriteFont SCOREFONT;
        static public SpriteFont SELECTFONT;
        static public Texture2D BACKGROUND;
        static public Texture2D MENU;
        static public Texture2D SONG_SELECT;
        
        static public void Load(ContentManager c)
        {
            SONGFILES[0] = "Music/Get_Jinxed";

            SONGTITLES[0] = "Get Jinxed - Riot Games";

            SCOREFONT = c.Load<SpriteFont>("gamefont");
            SELECTFONT = c.Load < SpriteFont>("selectfont");

            BACKGROUND = c.Load<Texture2D>("background");
            SONG_SELECT = c.Load<Texture2D>("song_selection");
            MENU = c.Load<Texture2D>("menu");
        }
        public static void LoadGameAssets(ContentManager c)
        {
            BUTTON[0] = c.Load<Texture2D>("Buttons/button_orange");
            BUTTON[1] = c.Load<Texture2D>("Buttons/button_purple");
            BUTTON[2] = c.Load<Texture2D>("Buttons/button_blue");
            BUTTON[3] = c.Load<Texture2D>("Buttons/button_yellow");
            BUTTON[4] = c.Load<Texture2D>("Buttons/button_red");
            BUTTON[5] = c.Load<Texture2D>("Buttons/button_green");

            PRESSED[0] = c.Load<Texture2D>("Buttons/button_orange_pressed");
            PRESSED[1] = c.Load<Texture2D>("Buttons/button_purple_pressed");
            PRESSED[2] = c.Load<Texture2D>("Buttons/button_blue_pressed");
            PRESSED[3] = c.Load<Texture2D>("Buttons/button_yellow_pressed");
            PRESSED[4] = c.Load<Texture2D>("Buttons/button_red_pressed");
            PRESSED[5] = c.Load<Texture2D>("Buttons/button_green_pressed");

            RING[0] = c.Load<Texture2D>("Rings/orange_ring");
            RING[1] = c.Load<Texture2D>("Rings/purple_ring");
            RING[2] = c.Load<Texture2D>("Rings/blue_ring");
            RING[3] = c.Load<Texture2D>("Rings/yellow_ring");
            RING[4] = c.Load<Texture2D>("Rings/red_ring");
            RING[5] = c.Load<Texture2D>("Rings/green_ring");

            POWERBAR[0] = c.Load<Texture2D>("barborder");
            POWERBAR[1] = c.Load<Texture2D>("barfill");
        }
        public static void LoadMusicFile(int choice, ContentManager c)
        {
            MUSICFILES[choice] = c.Load<Song>(SONGFILES[choice]);
        }
    }
}
