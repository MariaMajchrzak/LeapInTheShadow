using GoUp.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace GoUp.Entities
{
    class BackgroundManager
    {
        //TODO: add bacgroundtile remover!
        public BackgroundManager(Texture2D texture)
        {
            _startBackground = new Sprite(0,0 ,BACKGROUND_WIDTH,BACKGROUND_HEIGHT,texture);
            _birdBackground = new Sprite(BACKGROUND_WIDTH,0 ,BACKGROUND_WIDTH,BACKGROUND_HEIGHT,texture);
            _catBackground = new Sprite(BACKGROUND_WIDTH * 2 ,0 ,BACKGROUND_WIDTH,BACKGROUND_HEIGHT,texture);
            _cloudBackground = new Sprite(BACKGROUND_WIDTH * 3,0 ,BACKGROUND_WIDTH,BACKGROUND_HEIGHT,texture);

            _backgroundTiles = new Queue<BackgroundTile>();

            backgroundInit();
        }

        public void Draw(GameTime gameTime ,SpriteBatch spriteBatch)
        {
            foreach( BackgroundTile tile in _backgroundTiles)
            {
                tile.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime) 
        {
        }

        public void MoveDownBackground()
        {
            foreach (BackgroundTile tile in _backgroundTiles)
            {
                tile.Position = new Vector2(tile.Position.X, tile.Position.Y + GAP_BETWEEN_TILE);
            }
            if(_backgroundTiles.Last().Position.Y >= -GAP_BETWEEN_TILE)
            {
                addNewBackground();
            }
        }
         public void addNewBackground()
         {
            _backgroundTiles.Enqueue(new BackgroundTile(_cloudBackground, new Vector2(0,-BACKGROUND_HEIGHT)) );
         }

        private void backgroundInit()
        {
            _backgroundTiles.Enqueue(new BackgroundTile(_startBackground, new Vector2(0,0)) );
            _backgroundTiles.Enqueue(new BackgroundTile(_cloudBackground, new Vector2(0,-BACKGROUND_HEIGHT)) );
        }

        private Queue<BackgroundTile> _backgroundTiles;

        private Sprite _startBackground;
        private Sprite _birdBackground;
        private Sprite _catBackground;
        private Sprite _cloudBackground;

        private Random _random;

        private const int BACKGROUND_WIDTH = 400;
        private const int BACKGROUND_HEIGHT = 800;
        private const int GAP_BETWEEN_TILE = 150;

    }
}
