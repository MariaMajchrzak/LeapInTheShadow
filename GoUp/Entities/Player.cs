using GoUp.Graphics;
using GoUp.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GoUp.Entities
{
    class Player : IEntity
    {

        public Player(Vector2 position, Texture2D spritesheet, SpriteFont font)
        {
            this.Position = position;

            _idlePlayerSprite = new Sprite(19, 2, PLAYER_WIDTH, PLAYER_HEIGHT, spritesheet, PLAYER_SCALE);
            _jumpPlayerSprite = new Sprite(15, 32, PLAYER_JUMPING_WIDTH, PLAYER_JUMPING_HEIGHT, spritesheet, PLAYER_SCALE);
            _fallingPlayerSprite = new Sprite(21, 64, PLAYER_FALLING_WIDTH, PLAYER_FALLING_HEIGHT, spritesheet, PLAYER_SCALE);
            _font = font;

            _tilesPassed = 0;
            PlayerState = PlayerState.idle;
        }

        public Vector2 Position { get; set; }
        public PlayerState PlayerState { get; set; } = PlayerState.idle;

        public event EventHandler OnPlayerGoUp;
        public event EventHandler OnPlayerScorePoint;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.PlayerState == PlayerState.idle)
            {
                _idlePlayerSprite.Draw(spriteBatch, this.Position);
                spriteBatch.DrawString(_font, "  press \n  <-- or -->", new Vector2(130, 400), Color.Black);
            }
            else if (this.PlayerState == PlayerState.Standing)
            {
               _idlePlayerSprite.Draw(spriteBatch, this.Position);
            }
            else if (this.PlayerState == PlayerState.JumpingLeft || this.PlayerState == PlayerState.JumpingRight)
            {
                _jumpPlayerSprite.Draw(spriteBatch, this.Position);
            }
            else if (this.PlayerState == PlayerState.Falling)
            {
                _fallingPlayerSprite.Draw(spriteBatch, this.Position);
            }

        }
        public void Update(GameTime gameTime)
        {
            if (PlayerState == PlayerState.JumpingRight && this.Position.X < PLAYER_RIGHT_X_POSITION)
            {
                this.Position = new Vector2(this.Position.X + JUMP_VELOCITY * (float)gameTime.ElapsedGameTime.TotalSeconds, this.Position.Y);
            }
            else if (PlayerState == PlayerState.JumpingLeft && this.Position.X > PLAYER_LEFT_X_POSITION)
            {
                this.Position = new Vector2(this.Position.X - JUMP_VELOCITY * (float)gameTime.ElapsedGameTime.TotalSeconds, this.Position.Y);
            }
            else if (PlayerState == PlayerState.Falling)
            {
                this.Position = new Vector2(this.Position.X, this.Position.Y + FALLING_VELOCITY * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if(PlayerState != PlayerState.idle)
            {
                PlayerState = PlayerState.Standing;
            }
            _previousPlayerState = PlayerState;
        }

        public void GoLeft()
        {
            this.PlayerState = PlayerState.JumpingLeft;
            PlayerUp();
        }

        public void GoRight()
        {
            this.PlayerState = PlayerState.JumpingRight;
            PlayerUp();
        }


        private Sprite _idlePlayerSprite;
        private Sprite _jumpPlayerSprite;
        private Sprite _fallingPlayerSprite;

        private PlayerState _previousPlayerState;
        private int _tilesPassed;

        private SpriteFont _font;

        private const int PLAYER_WIDTH = 23;
        private const int PLAYER_HEIGHT = 28;
        private const int PLAYER_JUMPING_WIDTH = 46;
        private const int PLAYER_JUMPING_HEIGHT = 23;
        private const int PLAYER_FALLING_WIDTH = 22;
        private const int PLAYER_FALLING_HEIGHT = 31;
        private const float PLAYER_SCALE = 2f;

        private const int PLAYER_RIGHT_X_POSITION = 300;
        private const int PLAYER_LEFT_X_POSITION = 30;
        private const int JUMP_VELOCITY = 2500;
        private const int FALLING_VELOCITY = 400;

        private const int GAP_BETWEEN_TILE = 150;
        private const int MAX_PLAYER_HEIGHT = 430;
        private void PlayerUp()
        {
            if (this.Position.Y > MAX_PLAYER_HEIGHT)
            {
                this.Position = new Vector2(this.Position.X, this.Position.Y - GAP_BETWEEN_TILE);

                if (_tilesPassed < 3)
                {
                    OnPlayerScorePoint?.Invoke(this, EventArgs.Empty);
                }
                _tilesPassed++;
            }
            else
            {
                OnPlayerGoUp?.Invoke(this, EventArgs.Empty);
                OnPlayerScorePoint?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}
