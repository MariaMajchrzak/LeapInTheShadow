using LeapInTheSadow.Entities;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapInTheSadow.System
{
    class InputController
    {
        public InputController(Player player)
        { 
            _player = player;
        }

        public void ControlInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if ( keyboardState.IsKeyDown(Keys.Right) && !_previousKeyboardState.IsKeyDown(Keys.Right) && 
                (_player.PlayerState == PlayerState.Standing || _player.PlayerState == PlayerState.idle))
            {
                _player.GoRight();
            }
            else if(keyboardState.IsKeyDown(Keys.Left) && !_previousKeyboardState.IsKeyDown(Keys.Left) &&
                ( _player.PlayerState == PlayerState.Standing || _player.PlayerState == PlayerState.idle))
            {
                _player.GoLeft();
            }

            _previousKeyboardState = keyboardState;

        }

        private Player _player;
        private KeyboardState _previousKeyboardState;
        
    }
}
