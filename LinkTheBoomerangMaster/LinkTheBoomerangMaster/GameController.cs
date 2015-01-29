#region Using Statements
using LinkTheBoomerangMaster.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
#endregion

namespace LinkTheBoomerangMaster
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameController : Game
    {
        protected Song song;
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public GameEnvironment environment;

        public Scoreboard scoreBoard;

        Player link;

        public static float scale = 1.5f;

        const float BALL_START_SPEED = 8f;

        const float KEYBOARD_PLAYER_SPEED = 10f;

        private Bouncerang boomerang1;


        public GameController()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 750;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 750;   // set this value to the desired height of your window
            graphics.ApplyChanges();


            // Set device frame rate to 30 fps.
            TargetElapsedTime = TimeSpan.FromSeconds(1 / 30.0);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;
            boomerang1 = new Bouncerang(this);
            environment = new GameEnvironment(this);
            scoreBoard = new Scoreboard(this);
            link = new Player(this);
            Input.link = link;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            boomerang1.Texture = new _2DTexture(Content.Load<Texture2D>("rupee"),scale);
            song = Content.Load<Song>("metal-zelda");
            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(song);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;

            boomerang1.Move(boomerang1.Velocity);

            Vector2 player1Velocity = Input.GetKeyboardInputDirection(PlayerIndex.One) * KEYBOARD_PLAYER_SPEED;

            link.Move(player1Velocity);

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your game logic here.
            if(link.isMoving)
                link.animatedTexture.UpdateFrame(elapsed);
            boomerang1.animatedTexture.UpdateFrame(elapsed);

            if (GameObject.CheckLinkBoomCollision(link, boomerang1))
            {
                boomerang1.Velocity.Y = Math.Abs(boomerang1.Velocity.Y) *-1;
            }

            if (!boomerang1.launched)
            {
                boomerang1.Launch(BALL_START_SPEED);
                boomerang1.launched = true;
            }

            if (boomerang1.Position.Y > ScreenHeight)
            {
                boomerang1.Launch(BALL_START_SPEED);
            }
                
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            environment.Draw(spriteBatch);
            scoreBoard.Draw(spriteBatch);

            spriteBatch.Begin();

            boomerang1.Draw(spriteBatch);
            
            link.Draw(spriteBatch);

            spriteBatch.End();
            
            // TODO: Add your drawing code here    

            base.Draw(gameTime);
        }
    }
}
