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
    class Ring
    {
        Texture2D image;
        Vector2 position;
        float scale;
        bool creation;
        
        public Ring(Vector2 pos, int num, bool type)
        {
            image = ResourceManager.RING[num];
            position = pos;
            scale = 3.0f;
            if (type == true)
            {
                scale = 0.5f;
            }            
            creation = type;
        }

        public bool Update()
        {
            if (creation == false)
            {
                scale -= 0.1f;
                if (scale < 0.5f)
                {
                    return false;
                }
                return true;
            }
            else 
            {
                scale += 0.1f;
                if (scale > 3.0f)
                {
                    return false;
                }
                return true;
            }
        }

        public int getScore()
        {
            int score = 0;
            if (scale > 0.9 && scale < 1.1)
            {
                score = 100;
            }
            else if (scale > 0.7 && scale < 1.3)
            {
                score = 50;
            }
            else if (scale > 0.5 && scale < 1.5)
            {
                score = 10;
            }
            else
            {
                score = 0;
            }
            return score;
        }

        public void Draw(SpriteBatch s)
        {
            s.Draw(image, position, null, Color.White, 0.0f, new Vector2(75, 75), scale, SpriteEffects.None, 0);
        }
    }
}
