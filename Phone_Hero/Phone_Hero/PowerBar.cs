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
    class PowerBar
    {
        int multiplier;

        public PowerBar()
        {
            multiplier = 1;
        }

        public int getMultiplier()
        {
            return multiplier;
        }

        public void incrementMultiplier()
        {
            if (multiplier < 5)
            {
                multiplier++;
            }
        }

        public void decrementMultiplier()
        {
            if (multiplier > 1)
            {
                multiplier--;
            }
        }

        public void loseMultiplier()
        {
            multiplier = 1;
        }
        public void Draw(SpriteBatch s)
        {
            s.Draw(ResourceManager.POWERBAR[1], new Vector2(300, 70), new Rectangle(0, 0, multiplier * 25, 56), Color.White);
            s.Draw(ResourceManager.POWERBAR[0], new Vector2(300, 70), Color.White);
        }
    }
}
