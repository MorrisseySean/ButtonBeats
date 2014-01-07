using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
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
    /// <summary>
    /// Class to controls all of the game elements
    /// Key:
    /// 0 = Orange; 
    /// 1 = Purple; 
    /// 2 = Blue; 
    /// 3 = Yellow; 
    /// 4 = Red; 
    /// 5 = Green;
    /// </summary>
    class Board
    {
        Button[] buttons;
        PowerBar powerBar;
        String data;
        String beatMap;
        bool songComplete;
        int score;
        int time;
        int multiplier;
        int song;
        //Variables to be removed before live
        bool creation;
        Rectangle save;
        String savefile;

        public Board()
        {
            buttons = new Button[6];
            powerBar = new PowerBar();
            for (int i = 0; i < 6; i++)
            {
                buttons[i] = new Button();
            }           
        }
        
        public void LoadBoard(int songSelection, bool type)
        {           
            songComplete = false;
            time = 20;
            multiplier = 0;
            score = 0;
            creation = type;
            song = songSelection;
            //load the 6 buttons
            buttons[0].Load(0, new Vector2(135, 255));
            buttons[1].Load(1, new Vector2(345, 255));
            buttons[2].Load(2, new Vector2(135, 435));
            buttons[3].Load(3, new Vector2(345, 435));
            buttons[4].Load(4, new Vector2(135, 615));
            buttons[5].Load(5, new Vector2(345, 615));
            if (creation == false)
            {
                beatMap = Beatmaps.maps[song];
            }
            else
            {
                save = new Rectangle(0, 0, 100, 100);
                savefile = ResourceManager.SONGFILES[song];
            }
        }

        public void Update()
        {
            time++;
            if (creation == false)
            {
                if (time < beatMap.Length && time > -1)
                {
                    int num = Convert.ToInt32(beatMap[time]) - 48;
                    if (num < 5)
                    {
                        buttons[num].Ping(num, false);
                    }
                }
                else if (time > beatMap.Length + 100)
                {
                    songComplete = true;
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (buttons[i].Update())
                {
                    powerBar.loseMultiplier();
                }
            }
            TouchDetection();
        }

        public int getScore()
        {
            return score;
        }

        public bool Completed()
        {
            return songComplete;
        }

        void TouchDetection()
        {
            bool pressed = false;
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            {
                if (tl.State == TouchLocationState.Pressed)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        buttons[i].isPressed(tl.Position);
                        if (buttons[i].checkPressed(tl.Position))
                        {
                            if (creation == false)
                            {
                                int temp = buttons[i].getScore();
                                if (temp == 0)
                                {
                                    powerBar.loseMultiplier();
                                }
                                else if (temp == 10)
                                {
                                    powerBar.decrementMultiplier();
                                }
                                else
                                {
                                    multiplier++;
                                    if (multiplier > 4)
                                    {
                                        multiplier = 0;
                                        powerBar.incrementMultiplier();
                                    }
                                }
                                score += (temp * powerBar.getMultiplier());
                            }
                            else
                            {
                                data += Convert.ToString(i);
                                buttons[i].Ping(i, true);
                                pressed = true;
                            }
                        }
                    }
                    if (save.Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        Save();
                    }
                }                
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        buttons[i].isPressed(new Vector2(0, 0));
                    }                    
                }                
            }
            if (creation == true)
            {
                if (pressed == false)
                {
                    data += Convert.ToString(9);
                }
            }
        }

        

        public void Draw(SpriteBatch s)
        {
            for (int i = 0; i < 6; i++)
            {
                buttons[i].Draw(s);
            }
            for (int i = 0; i < 6; i++)
            {
                buttons[i].DrawRings(s);
            }
            s.DrawString(ResourceManager.SELECTFONT, ResourceManager.SONGTITLES[song], new Vector2(50, 25), Color.Red);
            if (creation == true)
            {
                s.Draw(ResourceManager.BUTTON[0], save, Color.HotPink);
            }
            else
            {
                powerBar.Draw(s);
            }
            
        }

        //Creation mode methods
        void Save()
        {
            // Save the game state (in this case, the high score).
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();
            // open isolated storage, and write the savefile.
            IsolatedStorageFileStream fs = null;
            List<byte> bytes = new List<byte>(0);
            fs = null;
            using (fs = savegameStorage.CreateFile(savefile))
            {
                if (fs != null)
                {
                    bytes = new List<byte>(0);
                    for (int j = 0; j < data.Length; j++)
                    {
                        bytes.Add(Convert.ToByte(data[j]));
                    }
                    fs.Write(bytes.ToArray(), 0, bytes.Count);
                }
            }
        }
        public string Load()
        {
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();
            if (savegameStorage.FileExists("testing.sav"))
            {
                using (IsolatedStorageFileStream fs = savegameStorage.OpenFile("testing.sav", System.IO.FileMode.Open))
                {
                    if (fs != null)
                    {
                        // Reload the saved high-score data.
                        String returnValue = "";
                        byte[] saveBytes = new byte[fs.Length];
                        fs.Read(saveBytes, 0, (int)fs.Length);
                        for (int i = 0; i < saveBytes.Length; i++)
                        {                            
                           returnValue += Convert.ToChar(saveBytes[i]);                            
                        }
                        //String returnVal = System.BitConverter.ToString(saveBytes, 0);
                        return returnValue; 
                    }
                }
            }
            return "0";
        }
        
        
    }
}
