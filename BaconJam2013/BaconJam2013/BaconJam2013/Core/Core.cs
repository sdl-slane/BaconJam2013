using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BaconJam2013
{

    public class UpdateData
        : EventArgs
    {

        private GameTime
            _gameTime;

        private float
            _frameDelta;

        public GameTime GameTime
        {
            get { return _gameTime; }
        }

        public float FrameDelta
        {
            get { return _frameDelta; }
        }

        public UpdateData(GameTime gameTime, float frameDelta)
        {
            _gameTime = gameTime;
            _frameDelta = frameDelta;
        }

    }

    public class RenderData
        : EventArgs
    {

        private SpriteBatch
            _spriteBatch;

        public SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
        }

        public RenderData(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

    }

    public delegate void UpdateEventHandler(object sender, UpdateData data);
    public delegate void RenderEventHandler(object sender, RenderData data);

    public class Core 
        : Microsoft.Xna.Framework.Game
    {

        public static event UpdateEventHandler UpdateEvent;
        public static event RenderEventHandler RenderEvent;

        private GraphicsDeviceManager 
            _graphics;

        private SpriteBatch 
            _spriteBatch;

        private Input
            _input;

        public const int
            WIDTH = 640,
            HEIGHT = 480,
            TARGET_FPS = 60;

        public float
            _currentFPS;

        public Core()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HEIGHT;

            IsFixedTimeStep = true;
            TargetElapsedTime = new TimeSpan(TimeSpan.TicksPerSecond / TARGET_FPS);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        public Texture2D attackFlowerIdle;
        public BasicUnit attackFlower;

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentFPS = 0.0f;

            _input = new Input();

            attackFlowerIdle = Content.Load<Texture2D>("sprsht_attackflower_idle");

            List<Sprite> frames = new List<Sprite>();

            for (int i = 0; i < 10; ++i)
            {
                frames.Add(new Sprite(attackFlowerIdle, new Rectangle(i * 60, 0, 60, 60)));
            }

            Animation anim = new Animation(frames, new Vector2(60), 200, true, true);

            attackFlower = new BasicUnit(anim, new Vector2(100), Color.White);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            float currentFPS = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (UpdateEvent != null)
                UpdateEvent(this, new UpdateData(gameTime, TARGET_FPS / currentFPS));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            if (RenderEvent != null)
                RenderEvent(this, new RenderData(_spriteBatch));

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}