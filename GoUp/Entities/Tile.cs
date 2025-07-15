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
        }

        public Sprite Sprite { get; set; }
        public int HeightLevel { get; set; }
        public TileType TileType { get; set; }
        public void Draw(GameTime gameTime , SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, _position);
        }
        public void Update(GameTime gameTime)
        {
            if(this.TileType == TileType.left)
            {
                _position.X = LEFT_TILE_X_POSITION;
            }
            else
            {
                _position.X = RIGHT_TILE_X_POSITION;
            }

            _position.Y = FIRST_TILE_Y_POSITION - HeightLevel * GAP_BETWEEN_TILE; 

        }


        private Vector2 _position;

        private const int RIGHT_TILE_X_POSITION = 304;
        private const int LEFT_TILE_X_POSITION = 0;
        private const int GAP_BETWEEN_TILE = 150;

        private const int FIRST_TILE_Y_POSITION = 750;
    }
}
