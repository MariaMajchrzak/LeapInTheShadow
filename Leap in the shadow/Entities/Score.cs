using LeapInTheSadow.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace LeapInTheSadow.Entities
{
    class Score
    {
        public Score(Player player, Texture2D texture)
        {
            _player = player;
            _texture = texture;

            _player.OnPlayerScorePoint += AddPoint;
        }

        public int Points { get; private set; } = 0;


        public Vector2 Position { get; set; } = new Vector2(SCORE_DRAW_X_POSITION, SCORE_DRAW_Y_POSITION);

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            if(Points == 0)
            {
                _numberSprite = new Sprite(0, 0, NUMBER_WIDTH, NUMBER_HEIGHT, _texture, NUMBER_SCALE);
                _numberSprite.Draw(spriteBatch, Position);

                return;
            }
            
            int score = this.Points;
            int numbersCounter = 0;

            while (score > 0)
            {
                int number = score % 10;
                _numberSprite = new Sprite(0 + number * NUMBER_WIDTH, 0, NUMBER_WIDTH, NUMBER_HEIGHT, _texture,NUMBER_SCALE);

                _numberSprite.Draw(spriteBatch, new Vector2(SCORE_DRAW_X_POSITION - numbersCounter * NUMBER_WIDTH * NUMBER_SCALE ,SCORE_DRAW_Y_POSITION));

                score = score / 10;
                numbersCounter++;
            }
        }

        private void AddPoint(object sender, EventArgs e)
        {
            Points++;
        }

        private Player _player;
        private Texture2D _texture;
        private Sprite _numberSprite;
        

        private const int NUMBER_WIDTH = 16;
        private const int NUMBER_HEIGHT = 16;
        private const float NUMBER_SCALE = 1.5f;

        private const int SCORE_DRAW_X_POSITION = 370;
        private const int SCORE_DRAW_Y_POSITION = 10;


    }
}
