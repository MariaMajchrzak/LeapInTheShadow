using GoUp.Graphics;
using GoUp.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoUp.Entities
{
    class Player : IEntity
    {
        public Player(Vector2 position, Texture2D spritesheet , TileManager tileManager)
        {
            this.Position = position;
            _tileManager = tileManager;

            _idlePlayerSprite = new Sprite(4, 6, PLAYER_WIDTH, PLAYER_HEIGHT, spritesheet);
        }

        public Vector2 Position { get; set; }
        public PlayerState PlayerState { get; private set; } = PlayerState.Idle;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _idlePlayerSprite.Draw(spriteBatch, this.Position);
        }
        public void Update(GameTime gameTime) 
        {

        }

        public void GoLeft()
        {
            if (this.Position.X == PLAYER_RIGHT_X_POSITION)
            {
                this.Position = new Vector2(PLAYER_LEFT_X_POSITION, this.Position.Y);
            }
            PlayerUp();
        }

        public void GoRight()
        {
            if (this.Position.X == PLAYER_LEFT_X_POSITION)
            {
                this.Position = new Vector2(PLAYER_RIGHT_X_POSITION, this.Position.Y);
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
                _tileManager.GenerateNewTiles();
            }
        }

        private Sprite _idlePlayerSprite;
        private TileManager _tileManager;

        private const int PLAYER_WIDTH = 24;
        private const int PLAYER_HEIGHT = 23;
       
        private const int PLAYER_RIGHT_X_POSITION = 340;
        private const int PLAYER_LEFT_X_POSITION = 30;
        private const int GAP_BETWEEN_TILE = 150;
        private const int MAX_PLAYER_HEIGHT = 430;
    }
}
