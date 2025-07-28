using LeapInTheSadow.Graphics;
using LeapInTheSadow.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using LeapInTheSadow.System;
using Timer = LeapInTheSadow.Entities.Timer;
using LeapInTheShadow.Entities;
using System;
using LeapInTheShadow.System;
using Microsoft.Xna.Framework.Audio;

namespace LeapInTheSadow;

public class LeapInTheShadow : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameState _gameState;
    private enum GameState
    {
        Menu,
        Play
    }
        
    public LeapInTheShadow()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _gameState = GameState.Menu;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
        _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _tilesSpritesheet =  Content.Load<Texture2D>("tilesSpriteSheet");
        _catSpritesheet =  Content.Load<Texture2D>("catsSpriteSheett");
        _backgroundSpritesheet =  Content.Load<Texture2D>("backgroundSpriteSheet");
        _numbersSpritesheet =  Content.Load<Texture2D>("numberSpritesheet");
        _resetbuttonSpritesheet =  Content.Load<Texture2D>("buttonSpritesheet");
        _menubuttonSpritesheet =  Content.Load<Texture2D>("menuButtonSpritesheet");
        _jumpSound = Content.Load<SoundEffect>("jumpSound");



        _player = new Player(new Vector2(PLAYER_START_POSITION_X, PLAYER_START_POSITION_Y), _catSpritesheet, _jumpSound);
        _timer = new Timer(_player);
        _score = new Score(_player, _numbersSpritesheet);
        _tileManager = new TileManager(_tilesSpritesheet, _player, _timer, _score);
        _backgroundManager = new BackgroundManager(_backgroundSpritesheet, _player);
        _inputController = new InputController(_player);
        _deathPanel = new DeathPanel(_resetbuttonSpritesheet);
        _menu = new Menu(_backgroundSpritesheet, _menubuttonSpritesheet);

        _menu.onPlayButtonClicked += play;
        _deathPanel.GameReset += gameReset;

        EntityManager.AddEntity(_player);
      
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        switch (_gameState)
        {
            case GameState.Menu:

                _menu.Update(gameTime);
                break;

            case GameState.Play:

                _backgroundManager.Update(gameTime);
                EntityManager.Update(gameTime);
                _tileManager.Update(gameTime);
                _timer.Update(gameTime);
                if (_player.PlayerState == PlayerState.Dead)
                {
                    _deathPanel.Update(gameTime);
                }
                _inputController.ControlInput();
                break;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.WhiteSmoke);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        switch (_gameState)
        {
            case GameState.Menu:

                _menu.Draw(_spriteBatch);
                break;

            case GameState.Play:

                _backgroundManager.Draw(gameTime, _spriteBatch);
                EntityManager.Draw(gameTime, _spriteBatch);
                _tileManager.Draw(gameTime, _spriteBatch);
                _score.Draw(gameTime, _spriteBatch);

                if (_player.PlayerState == PlayerState.Dead)
                {
                    _deathPanel.Draw(_spriteBatch);
                }
                break;
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }


    private void gameReset(object sender, EventArgs e)
    {
        EntityManager.Clear();

        _player = new Player(new Vector2(PLAYER_START_POSITION_X, PLAYER_START_POSITION_Y), _catSpritesheet, _jumpSound);
        _timer = new Timer(_player);
        _score = new Score(_player, _numbersSpritesheet);
        _tileManager = new TileManager(_tilesSpritesheet, _player, _timer, _score);
        _backgroundManager = new BackgroundManager(_backgroundSpritesheet, _player);
        _inputController = new InputController(_player);
        _deathPanel = new DeathPanel(_resetbuttonSpritesheet);

        EntityManager.AddEntity(_player);
    }

    private void play(object sender, EventArgs e)
    {
        _gameState = GameState.Play;
    }

    private const int SCREEN_WIDTH = 400;
    private const int SCREEN_HEIGHT = 800;

    private const int PLAYER_START_POSITION_X = 30;
    private const int PLAYER_START_POSITION_Y = 718;

    private Player _player;
    private TileManager _tileManager;
    private InputController _inputController; 
    private BackgroundManager _backgroundManager;
    private Score _score;
    private Timer _timer;
    private DeathPanel _deathPanel;
    private Menu _menu;

    private Texture2D _backgroundSpritesheet;
    private Texture2D _tilesSpritesheet;
    private Texture2D _catSpritesheet;
    private Texture2D _numbersSpritesheet;
    private Texture2D _resetbuttonSpritesheet;
    private Texture2D _menubuttonSpritesheet;
    private SoundEffect _jumpSound;

}

