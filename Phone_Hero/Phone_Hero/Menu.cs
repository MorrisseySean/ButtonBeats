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
    class Menu
    {
        enum Menutype
        {
            Main,
            Songselect,
            Creationselect
        }
        Menutype current;
        Rectangle[] mainSelections = new Rectangle[3];
        Rectangle[] selectSelections = new Rectangle[1];

        //Variables to have menu selection move with scroll
        int yVar, yLock;        

        public Menu()
        {
            yVar = 0;
            current = Menutype.Main;

            //Create initial rectangles for touch detection
            mainSelections[0] = new Rectangle(65, 220, 90, 90);
            mainSelections[1] = new Rectangle(65, 390, 90, 90);
            mainSelections[2] = new Rectangle(65, 555, 90, 90);

            selectSelections[0] = new Rectangle(95, 180, 290, 35);           
        }

        public int Update()
            //Checks for touch input and returns accordingly
        {
            if (current == Menutype.Main)
            {
                TouchCollection touchCollection = TouchPanel.GetState();
                foreach (TouchLocation tl in touchCollection)
                {
                    if (mainSelections[0].Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        current = Menutype.Songselect;
                    }
                    else if (mainSelections[1].Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        current = Menutype.Creationselect;
                    }
                    else if (mainSelections[0].Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        return -2;
                    }
                }
            }
            else if (current == Menutype.Songselect)
            {
                TouchCollection touchCollection = TouchPanel.GetState();
                foreach (TouchLocation tl in touchCollection)
                {
                    for (int i = 0; i < selectSelections.Length; i++)
                    {
                        if (selectSelections[i].Contains((int)tl.Position.X, (int)tl.Position.Y))
                        {
                            return i;
                        }
                    }
                }
            }
            else if (current == Menutype.Creationselect)
            {
                TouchCollection touchCollection = TouchPanel.GetState();
                foreach (TouchLocation tl in touchCollection)
                {
                    for (int i = 0; i < selectSelections.Length; i++)
                    {
                        if (selectSelections[i].Contains((int)tl.Position.X, (int)tl.Position.Y))
                        {
                            return i + 100;
                        }
                    }
                }
            }
            return -1;
        }

        public void UpdateScroll(GestureSample gs)
            //Deals with scrolling of menu selections
        {            
            yVar += (int)gs.Delta.Y;
            if (gs.GestureType == GestureType.DragComplete)
            {
                for (int i = 0; i < 3; i++)
                {
                    //For main menu scrolling when implemented
                }
                for (int i = 0; i < 1; i++)
                {
                    selectSelections[i].Y += yVar;
                }                
            }
        }

        public bool GoBack()
        {
            if (current != Menutype.Main)
            {
                current = Menutype.Main;
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Draw(SpriteBatch s)
        {
            if (current == Menutype.Main)
            {
                s.Draw(ResourceManager.MENU, Vector2.Zero, Color.White);
            }
            else if (current == Menutype.Songselect)
            {
                s.Draw(ResourceManager.SONG_SELECT, Vector2.Zero, Color.White);
                s.DrawString(ResourceManager.SELECTFONT, ResourceManager.SONGTITLES[0], new Vector2(95, 180 + yVar), Color.Red);
            }
            else if (current == Menutype.Creationselect)
            {
                s.Draw(ResourceManager.SONG_SELECT, Vector2.Zero, Color.White);
                s.DrawString(ResourceManager.SELECTFONT, ResourceManager.SONGTITLES[0], new Vector2(95, 180 + yVar), Color.Red);
            }
        }
    }
}
