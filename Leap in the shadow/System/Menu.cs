using LeapInTheSadow.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapInTheShadow.System
{
    class Menu
    {
        public Menu(Texture2D spritesheetBackground, Texture2D spritesheetButton)
        {
            _backgroundSprite = new Sprite(BACKGROUND_WIDTH * 2, 0, BACKGROUND_WIDTH, BACKGROUND_HEIGHT, spritesheetBackground, BACKGROUND_SCALE);
            _playButton = new Button(spritesheetButton, new Rectangle(BUTTON_POS_X,  BUTTON_POS_Y, BUTTON_WIDTH, BUTTON_HEIGHT), BUTTON_SCALE);
            
        }

        public EventHandler onPlayButtonClicked;

        public void Draw(SpriteBatch spriteBatch)
        {
            _backgroundSprite.Draw(spriteBatch, new Microsoft.Xna.Framework.Vector2(0, 0));
            _playButton.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            _playButton.Update(gameTime);
            if (_playButton.IsClicked)
            {
                onPlayButtonClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        private Sprite _backgroundSprite;
        private Button _playButton;

        private const int BACKGROUND_WIDTH = 100;
        private const int BACKGROUND_HEIGHT = 200;
        private const float BACKGROUND_SCALE = 4f;

        private const int BUTTON_POS_X = 160;
        private const int BUTTON_POS_Y = 360;
        private const int BUTTON_WIDTH = 16 * (int)BUTTON_SCALE ;
        private const int BUTTON_HEIGHT = 16 * (int)BUTTON_SCALE;
        private const float BUTTON_SCALE = 5f;






    }
}
