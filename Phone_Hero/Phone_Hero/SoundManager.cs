using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Phone_Hero
{
    class SoundManager
    {
        ///<summary>
        ///Track key:
        ///0 = Get jinxed
        ///</summary>
        public SoundManager()
        {
        }
 
        public void Play(int trackNum)
        {
            if (MediaPlayer.GameHasControl)
            {
                MediaPlayer.IsRepeating = false;
 
                if (MediaPlayer.State == MediaState.Stopped)
                {
                        MediaPlayer.Play(ResourceManager.MUSICFILES[trackNum]);
                }
            }
        }      
 
        public void Stop()
        {
            if (MediaPlayer.GameHasControl)
            {
                MediaPlayer.Stop();
            }
        }       
    }
}
