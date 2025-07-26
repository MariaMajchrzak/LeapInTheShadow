using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapInTheSadow.Entities
{
    class Timer
    {
        public Timer(Player player)
        {
            IsRunning =  true;
            player.OnPlayerScorePoint += Reset;
        }
        public bool IsRunning { get; set; } = false;
        public float TimePassed { get; set; }
        public void Update(GameTime gameTime)
        {
            if (IsRunning)
            {
                this.TimePassed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        public void Reset()
        {
            TimePassed = 0f;
        }
        private void Reset(object sender , EventArgs e)
        {
            TimePassed = 0f;
        }

    }
}
