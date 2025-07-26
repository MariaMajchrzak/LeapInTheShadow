using LeapInTheSadow.Graphics;
using LeapInTheSadow.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LeapInTheSadow.Entities
{
    class Player : IEntity
    {

        public Player(Vector2 position, Texture2D spritesheet)
        {
            this.Position = position;

            _idlePlayerSprite = new Sprite(0, 0, PLAYER_WIDTH, PLAYER_HEIGHT, spritesheet, PLAYER_SCALE);
            _blinkingPlayerSprite = new Sprite(17, 0, PLAYER_WIDTH, PLAYER_HEIGHT, spritesheet, PLAYER_SCALE);
            _jumpRightPlayerSprite = new Sprite(33, 0, PLAYER_WIDTH, PLAYER_HEIGHT, spritesheet, PLAYER_SCALE);
            _jumpLeftPlayerSprite = new Sprite(49, 0, PLAYER_WIDTH, PLAYER_HEIGHT, spritesheet, PLAYER_SCALE);
            _fallingPlayerSprite = new Sprite(66, 0, PLAYER_WIDTH, PLAYER_HEIGHT, spritesheet, PLAYER_SCALE);

            _tilesPassed = 0;
            makeBlinkingAnimation();
        }

        public Vector2 Position { get; set; }
        public PlayerState PlayerState { get; set; } = PlayerState.idle;

        public event EventHandler OnPlayerGoUp;
        public event EventHandler OnPlayerScorePoint;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.PlayerState == PlayerState.idle)
            {
                _blinkingPlayer.Draw(spriteBatch, this.Position);
            }
            else if (this.PlayerState == PlayerState.Standing)
            {
                _idlePlayerSprite.Draw(spriteBatch, this.Position);
            }
            else if (this.PlayerState == PlayerState.JumpingLeft )
            {
                _jumpLeftPlayerSprite.Draw(spriteBatch, this.Position);
            }
            else if (this.PlayerState == PlayerState.JumpingRight)
            {
                _jumpRightPlayerSprite.Draw(spriteBatch, this.Position);
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
            _blinkingPlayer.Update(gameTime);
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
        private Sprite _blinkingPlayerSprite;
        private Sprite _jumpRightPlayerSprite;
        private Sprite _jumpLeftPlayerSprite;
        private Sprite _fallingPlayerSprite;

        private Animation _blinkingPlayer;

        private int _tilesPassed;

        private const int PLAYER_WIDTH = 16;
        private const int PLAYER_HEIGHT = 16;
        private const float PLAYER_SCALE = 2f;

        private const int PLAYER_RIGHT_X_POSITION = 320;
        private const int PLAYER_LEFT_X_POSITION = 50;
        private const int JUMP_VELOCITY = 1000;
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

        private void makeBlinkingAnimation()
        {
            _blinkingPlayer = new Animation();
            _blinkingPlayer.AddFrame(_idlePlayerSprite, 0f);
            _blinkingPlayer.AddFrame(_blinkingPlayerSprite, 2f);
            _blinkingPlayer.AddFrame(_idlePlayerSprite, 2.5f);
            _blinkingPlayer.Start();

        }

    }
}
