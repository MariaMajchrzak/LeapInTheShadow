using GoUp.Graphics;
using GoUp.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using GoUp.System;
using Timer = GoUp.Entities.Timer;

namespace GoUp;

public class GoUp : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public GoUp()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
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
        _catSpritesheet =  Content.Load<Texture2D>("catSpriteSheet");
        _backgroundSpritesheet =  Content.Load<Texture2D>("backgroundSpriteSheet");
        _numbersSpritesheet =  Content.Load<Texture2D>("numberSpritesheet");
        _defaultFont = Content.Load<SpriteFont>("DefaultFont");//TODO : better font Sprite



        _player = new Player(new Vector2(PLAYER_START_POSITION_X, PLAYER_START_POSITION_Y), _catSpritesheet, _defaultFont);
        _timer = new Timer(_player);
        _score = new Score(_player, _numbersSpritesheet);
        _tileManager = new TileManager(_tilesSpritesheet, _player, _timer, _score);
        _backgroundManager = new BackgroundManager(_backgroundSpritesheet, _player);
        _inputController = new InputController(_player);

        EntityManager.AddEntity(_player);
      
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _backgroundManager.Update(gameTime);
        EntityManager.Update(gameTime);
        _tileManager.Update(gameTime);
        _timer.Update(gameTime);

        _inputController.ControlInput();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.WhiteSmoke);

        _spriteBatch.Begin();

        _backgroundManager.Draw(gameTime, _spriteBatch);
        EntityManager.Draw(gameTime, _spriteBatch);
        _tileManager.Draw(gameTime, _spriteBatch);
        _score.Draw(gameTime, _spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }


    private const int SCREEN_WIDTH = 400;
    private const int SCREEN_HEIGHT = 800;

    private const int PLAYER_START_POSITION_X = 30;
    private const int PLAYER_START_POSITION_Y = 700;

    private Player _player;
    private TileManager _tileManager;
    private InputController _inputController;
    private BackgroundManager _backgroundManager;
    private Score _score;
    private Timer _timer;

    private Texture2D _backgroundSpritesheet;
    private Texture2D _tilesSpritesheet;
    private Texture2D _catSpritesheet;
    private Texture2D _numbersSpritesheet;
    private SpriteFont _defaultFont;

}

