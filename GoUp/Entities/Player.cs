using GoUp.Graphics;
using GoUp.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GoUp.Entities
{
    class Player : IEntity
    {

        public Player(Vector2 position, Texture2D spritesheet , TileManager tileManager , BackgroundManager backgroundManager)
        {
            this.Position = position;
            _tileManager = tileManager;
            _backgroundManager = backgroundManager;

            _idlePlayerSprite = new Sprite(2, 2, PLAYER_WIDTH, PLAYER_HEIGHT, spritesheet);
            _jumpPlayerSprite = new Sprite(0, 36, PLAYER_JUMPING_WIDTH, PLAYER_JUMPING_HEIGHT, spritesheet);
        }

        public Vector2 Position { get; set; }
        public PlayerState PlayerState { get; set; } = PlayerState.Idle;

        public event EventHandler OnPlayerGoUp;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (this.PlayerState == PlayerState.Idle)
            {
                _idlePlayerSprite.Draw(spriteBatch, this.Position);
            }
            else if(this.PlayerState == PlayerState.JumpingLeft || this.PlayerState == PlayerState.JumpingRight)
            {
                _jumpPlayerSprite.Draw(spriteBatch, this.Position);
            }
        }
        public void Update(GameTime gameTime) 
        {
            if (PlayerState == PlayerState.JumpingRight && this.Position.X < PLAYER_RIGHT_X_POSITION)
            {
                this.Position = new Vector2(this.Position.X + JUMP_VELOCITY * (float)gameTime.ElapsedGameTime.TotalSeconds , this.Position.Y);
            }
            else if(PlayerState == PlayerState.JumpingLeft && this.Position.X > PLAYER_LEFT_X_POSITION)
            {
                this.Position = new Vector2(this.Position.X - JUMP_VELOCITY * (float)gameTime.ElapsedGameTime.TotalSeconds, this.Position.Y);
            }
            else if(PlayerState == PlayerState.Falling)
            {

            }
            else
            {
                PlayerState = PlayerState.Idle;
            }
        } 

        public void GoLeft()
        {
            if (this.Position.X >= PLAYER_RIGHT_X_POSITION)
            { 
               this.PlayerState = PlayerState.JumpingLeft;
            }
            PlayerUp();
        }

        public void GoRight()
        {
            if (this.Position.X <= PLAYER_LEFT_X_POSITION)
            {
                this.PlayerState = PlayerState.JumpingRight;
            }
            PlayerUp();
        }

        private void PlayerUp()
        {
            if(this.Position.Y > MAX_PLAYER_HEIGHT)
            {
                this.Position = new Vector2(this.Position.X, this.Position.Y - GAP_BETWEEN_TILE);
            }
            else
            {
                OnPlayerGoUp?.Invoke(this ,EventArgs.Empty);
            }
        }

        private Sprite _idlePlayerSprite;
        private Sprite _jumpPlayerSprite;
        private TileManager _tileManager;
        private BackgroundManager _backgroundManager;

        private const int PLAYER_WIDTH = 27;
        private const int PLAYER_HEIGHT = 30;
        private const int PLAYER_JUMPING_WIDTH = 30;
        private const int PLAYER_JUMPING_HEIGHT = 64;
       
        private const int PLAYER_RIGHT_X_POSITION = 325;
        private const int PLAYER_LEFT_X_POSITION = 30;
        private const int JUMP_VELOCITY = 2500;

        private const int GAP_BETWEEN_TILE = 150;
        private const int MAX_PLAYER_HEIGHT = 430;
    }
}
