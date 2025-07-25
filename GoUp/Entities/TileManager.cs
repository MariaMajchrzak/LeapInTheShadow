using GoUp.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace GoUp.Entities
{
     class TileManager 
     {
        private enum TilePattern
        {
            Left,
            Right,
            Both
        }
        public TileManager(Texture2D spriteSheet , Player player, Timer timer, Score score)
        {
            _tileSprite = new Sprite(64, 63, TILE_WIDTH, TILE_HEIGHT, spriteSheet);
            _random = new Random();
            _tiles = new List<Tile>();
            _player = player;
            _timer = timer;
            _score = score;

            _player.OnPlayerGoUp += GenerateNewTiles;
            tilesInit();
        }


        public void Draw(GameTime gameTime , SpriteBatch spriteBatch)
        {
            double decreseTimeBy = Math.Min(_score.Points * TIME_DECREASE_RATE, MAX_DECRESE_TIMER);
            bool TimePassed = _timer.TimePassed > MAX_TIME_FOR_JUMP - decreseTimeBy;
            
            if (TimePassed)
            {
                _timer.Reset();
                _tiles.RemoveAll(t => t.IsShaking);
            }

            foreach (Tile tile in _tiles)
            {
                tile.Draw(gameTime, spriteBatch);
            }
            
        }
        public void Update(GameTime gameTime)
        {
            //TODO: find better way to checking the collision
            _collisionCounter = 0;

            foreach (Tile tile in _tiles)
            {
                tile.Update(gameTime);
                bool IsCollision = checkCollision(_player, tile);
                if(IsCollision)
                {
                    _collisionCounter++;
                }
                if(IsCollision && _player.PlayerState == PlayerState.Idle)
                {
                    tile.IsShaking = true;
                }
            }

            if(_collisionCounter == 0 && _player.PlayerState == PlayerState.Idle)
            {
                _player.PlayerState = PlayerState.Falling;
            }
            else if (_collisionCounter > 0 && _player.PlayerState == PlayerState.Falling)
            {
                _player.PlayerState = PlayerState.Idle;
            }

            _tiles.RemoveAll(t => t.HeightLevel <= -1);
        }
        public void GenerateNewTiles(object sender, EventArgs e)
        {
            TilePattern rnd = (TilePattern)_random.Next(3);

            if (rnd == TilePattern.Left)
            {
                _tiles.Add(new Tile(8, TileType.left, _tileSprite));
            }
            else if (rnd == TilePattern.Right)
            {
                _tiles.Add(new Tile(8, TileType.right, _tileSprite));
            }
            else 
            {
                _tiles.Add(new Tile(8, TileType.right, _tileSprite));
                _tiles.Add(new Tile(8, TileType.left, _tileSprite));
            }

            tilesDown();
        }


        private const int TILE_WIDTH = 96;
        private const int TILE_HEIGHT = 16;
        private const int AMOUNT_OF_TILE = 8;

        private const float MAX_TIME_FOR_JUMP = 10f;
        private const double TIME_DECREASE_RATE = 0.1;
        private const int MAX_DECRESE_TIMER = 9;

        private const int PLAYER_SCALE = 2;
        private const int PLAYER_WIDTH = 27 * PLAYER_SCALE;
        private const int PLAYER_HEIGHT = 28 * PLAYER_SCALE;

        private Player _player;
        private Timer _timer;
        private Score _score;

        private Random _random;
        private Sprite _tileSprite;
        private List<Tile> _tiles;
        private int _collisionCounter;

        private void tilesDown()
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
                _tiles.Add(new Tile(i, TileType.right, _tileSprite));
                _tiles.Add(new Tile(i, TileType.left, _tileSprite));
            }
        }
        private bool checkCollision(Player player, Tile tile)
        {
            
            if(
               tile.Position.Y <= player.Position.Y + PLAYER_HEIGHT &&
               tile.Position.Y + TILE_HEIGHT >= player.Position.Y + PLAYER_HEIGHT &&
               tile.Position.X <= player.Position.X &&
               tile.Position.X + TILE_WIDTH >= player.Position.X
            )
            {
                return true;
            }
            return false; 
        }

    }
}
