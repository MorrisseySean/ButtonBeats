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
    class Button
    {
        Vector2 position;
        Vector2 centre;

        Texture2D texture;
        Texture2D pressed;
        Texture2D ring;
        Texture2D active;
        
        Rectangle bounds;
        List<Ring> rings = new List<Ring>();

        public void Load(int num, Vector2 pos)
        {
            texture = ResourceManager.BUTTON[num];
            pressed = ResourceManager.PRESSED[num];
            ring = ResourceManager.RING[num];
            position = pos;
            centre = new Vector2(texture.Width / 2, texture.Height / 2);
            bounds = new Rectangle((int)(position.X - centre.X), (int)(position.Y - centre.Y), texture.Width, texture.Height);
            active = texture;
        }

        public bool Update()
        {
            bool missed = false;
            for (int i = rings.Count - 1; i >= 0; i--)
            {
                if (rings[i].Update() == false)
                {
                    rings.RemoveAt(i);
                    missed = true;
                }
            }
            return missed;
        }

        public void isPressed(Vector2 loc)
        {
            if (bounds.Contains(new Point((int)loc.X, (int)loc.Y)))
            {
                active = pressed;
            }
            else
            {
                active = texture;
            }
        }
        public bool checkPressed(Vector2 loc)
        {
            if (bounds.Contains(new Point((int)loc.X, (int)loc.Y)))
            {                
                return true;
            }
            else
            {
                return false;
            }
        }
        public int getScore()
        {
            if (rings.Count > 0)
            {
                int score = rings[0].getScore();
                rings.RemoveAt(0);
                return score;
            }
            return 0;
        }
        public void Ping(int num, bool creation)
        {
            Ring r = new Ring(position, num, creation);
            rings.Add(r);
        }
        public void Draw(SpriteBatch s)
        {
            s.Draw(active, position, null, Color.White, 0.0f, centre, 1.0f, SpriteEffects.None, 0);
                                   
        }
        public void DrawRings(SpriteBatch s)
        {
            //Draw methods need to be seperate to keep all rings above the buttons
            foreach (Ring r in rings)
            {
                r.Draw(s);
            } 
        }
    }
}
