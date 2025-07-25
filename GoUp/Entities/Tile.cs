using GoUp.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoUp.Entities
{
    enum TileType
    {
        left,
        right,
    }
    class Tile:IEntity
    {

        public Tile(int height ,TileType type, Sprite sprite)
        {
            this.HeightLevel = height;
            this.Sprite = sprite;
            this.TileType = type;

            random = new Random();
        }

        public Sprite Sprite { get; set; }
        public int HeightLevel { get; set; }
        public TileType TileType { get; set; }
        public Vector2 Position { get; private set; }
        public bool IsShaking { get; set; } = false;

        public void Draw(GameTime gameTime , SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position);
        }
        public void Update(GameTime gameTime)
        {
            if(this.TileType == TileType.left)
            {
                Position = new Vector2(LEFT_TILE_X_POSITION , Position.Y);
            }
            else
            {
                Position = new Vector2(RIGHT_TILE_X_POSITION, Position.Y);
            }
            Position = new Vector2( Position.X , FIRST_TILE_Y_POSITION - HeightLevel * GAP_BETWEEN_TILE); 

            if(this.IsShaking)
            {
                int tileShift = random.Next(MIN_TILE_SHIFT, MAX_TILE_SHIFT);
                Position = new Vector2(this.Position.X + tileShift, this.Position.Y + tileShift);
            }

        }

        private const int RIGHT_TILE_X_POSITION = 304;
        private const int LEFT_TILE_X_POSITION = 0;
        private const int GAP_BETWEEN_TILE = 150;
        private const int FIRST_TILE_Y_POSITION = 750;

        private const int MIN_TILE_SHIFT = -3;
        private const int MAX_TILE_SHIFT = 4;

        private Random random;
    }
}
