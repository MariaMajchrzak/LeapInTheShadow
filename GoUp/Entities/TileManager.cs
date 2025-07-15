using GoUp.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace GoUp.Entities
{
     class TileManager 
     {
        //TODO: do tiles remover
        private enum TilePattern
        {
            Left,
            Right,
            Both
        }
        public TileManager(Texture2D spriteSheet)
        {
            _tileSprite = new Sprite(64, 63, TILE_WIDTH, TILE_HEIGHT, spriteSheet);
            _random = new Random();
            _tiles = new Queue<Tile>();

            tilesInit();

        }
        public void Draw(GameTime gameTime , SpriteBatch spriteBatch)
        {
            foreach(Tile tile in _tiles)
            {
                tile.Draw(gameTime, spriteBatch);
            }
        }
        public void Update(GameTime gameTime)
        {
            foreach (Tile tile in _tiles)
            {
                tile.Update(gameTime);
            }
        }
        public void GenerateNewTiles()
        {
            TilePattern rnd = (TilePattern)_random.Next(3);

            if (rnd == TilePattern.Left)
            {
                _tiles.Enqueue(new Tile(8, TileType.left, _tileSprite));
            }
            else if (rnd == TilePattern.Right)
            {
                _tiles.Enqueue(new Tile(8, TileType.right, _tileSprite));
            }
            else 
            {
                _tiles.Enqueue(new Tile(8, TileType.right, _tileSprite));
                _tiles.Enqueue(new Tile(8, TileType.left, _tileSprite));
            }

            TilesDown();
        }


        private const int TILE_WIDTH = 96;
        private const int TILE_HEIGHT = 16;
        private const int AMOUNT_OF_TILE = 8;


        private Random _random;
        private Sprite _tileSprite;
        private Queue<Tile> _tiles;

        private void TilesDown()
        {
            foreach (Tile tile in _tiles)
            {
                tile.HeightLevel--;
            }
        }
        private void tilesInit()
        {
            for (int i = 0; i < AMOUNT_OF_TILE ; i++)
            {
                _tiles.Enqueue(new Tile(i, TileType.right, _tileSprite));
                _tiles.Enqueue(new Tile(i, TileType.left, _tileSprite));
            }
        }
    }
}
