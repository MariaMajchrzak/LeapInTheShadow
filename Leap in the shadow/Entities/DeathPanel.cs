using LeapInTheSadow.Entities;
using LeapInTheShadow.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Button = LeapInTheShadow.System.Button;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace LeapInTheShadow.Entities
{
    class DeathPanel
    {
        public DeathPanel(Texture2D spritesheet)
        {
            _resetButton = new Button(spritesheet, new Microsoft.Xna.Framework.Rectangle(BUTTON_POS_X, BUTTON_POS_Y, BUTTON_WIDTH, BUTTON_HEIGHT), BUTTON_SCALE);
        }
        public EventHandler GameReset;
        public void Draw(SpriteBatch spriteBatch)
        {
            _resetButton.Draw(spriteBatch);
        }
        public void Update(GameTime gametime)
        {
            _resetButton.Update(gametime);
            if(_resetButton.IsClicked)
            {
                GameReset?.Invoke(this, EventArgs.Empty );
            }
        }

        private Button _resetButton;

        private const int BUTTON_POS_X = 184;
        private const int BUTTON_POS_Y = 700;
        private const int BUTTON_WIDTH = 16 * (int)BUTTON_SCALE;
        private const int BUTTON_HEIGHT = 16 * (int)BUTTON_SCALE;
        private const float BUTTON_SCALE = 2f;


    }
}
