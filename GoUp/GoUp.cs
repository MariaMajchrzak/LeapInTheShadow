using GoUp.Graphics;
using GoUp.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using GoUp.System;

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

        _tilesSpriteSheet =  Content.Load<Texture2D>("tilesSpriteSheet");
        _catSpriteSheet =  Content.Load<Texture2D>("catSpriteSheet");
        _backgroundSpriteSheet =  Content.Load<Texture2D>("backgroundSpriteSheet");

        _tileManager = new TileManager(_tilesSpriteSheet);
        _backgroundManager = new BackgroundManager(_backgroundSpriteSheet);
        _player = new Player(new Vector2(PLAYER_START_POSITION_X, PLAYER_START_POSITION_Y), _catSpriteSheet , _tileManager , _backgroundManager);
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

        _spriteBatch.End();

        base.Draw(gameTime);
    }


    private const int SCREEN_WIDTH = 400;
    private const int SCREEN_HEIGHT = 800;

    private const int PLAYER_START_POSITION_X = 30;
    private const int PLAYER_START_POSITION_Y = 730;

    private Player _player;
    private TileManager _tileManager;
    private InputController _inputController;
    private BackgroundManager _backgroundManager;

    private Texture2D _backgroundSpriteSheet;
    private Texture2D _tilesSpriteSheet;
    private Texture2D _catSpriteSheet;

}

