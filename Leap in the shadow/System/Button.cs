using LeapInTheSadow.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapInTheShadow.System
{
    class Button
    {
        private enum ButtonState
        {
            Normal,
            Hover,
        }

        public Button(Texture2D spritesheet, Rectangle rectangle)
        {
            _state = ButtonState.Normal;
            _spritesheet = spritesheet;

            _normalButtonSprite = new Sprite(0, 0,BUTTON_WIDTH, BUTTON_HEIGHT, _spritesheet,BUTTON_SCALE);
            _hoverButtonSprite = new Sprite(BUTTON_WIDTH, 0,BUTTON_WIDTH, BUTTON_HEIGHT, _spritesheet,BUTTON_SCALE);

            Rectangle = rectangle;
            _position = new Vector2(rectangle.X,rectangle.Y);
        }

        public Rectangle Rectangle { get; set;}
        public bool IsClicked { get; set; } = false;

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_state == ButtonState.Normal)
            {
                _normalButtonSprite.Draw(spriteBatch, _position);
            }
            else if (_state == ButtonState.Hover)
            {
                _hoverButtonSprite.Draw(spriteBatch, _position);
            }
        }
        public void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();


            if(
                Rectangle.Contains(_mouseState.Position) && 
                _mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && 
                _prevMouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed
            )
            {
                IsClicked = true;
            }
            else if (Rectangle.Contains(_mouseState.Position))
            {
                _state = ButtonState.Hover;
            }
            else
            {
                _state = ButtonState.Normal;
            }
            _prevMouseState = _mouseState;
        }
    


        private ButtonState _state;
        private Texture2D _spritesheet;
        private Vector2 _position;

        private Sprite _normalButtonSprite;
        private Sprite _hoverButtonSprite;

        private MouseState _mouseState;
        private MouseState _prevMouseState;

        private const int BUTTON_WIDTH = 16;
        private const int BUTTON_HEIGHT = 16;
        private const float BUTTON_SCALE = 2f;
    }
}
