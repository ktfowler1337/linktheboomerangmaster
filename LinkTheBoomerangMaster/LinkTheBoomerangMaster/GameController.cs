//Leave this defined unless your name is adrian on Ubuntu lol
#define PLAY_SOUND
#region Using Statements
using LinkTheBoomerangMaster.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
		#if PLAY_SOUND
        static public Song song;
        static public Song Menusong;
		#endif
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        GameMenu menu;

        public GameEnvironment environment;

		public Level level;

        public Scoreboard scoreBoard;

        public Player link;

        public static uint pointsToWin = 25;

        public static string Difficulty = "Normal";

        public static bool Paused = false;

        public static bool SoundOn = true;
        public static float SoundVolume = 0.8f;
        public static bool MusicOn = true;

        public static float scale = 1.5f;

        public static float GameSpeedMultiplier = 1f;

        public static float GameSpeedMultiplier2 = 1f;

        private List<Bouncerang> bouncerangs;

        public List<GameObject> Projectiles;

		public List<Enemy> Enemies;

        public GameController()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            SoundEffect.MasterVolume = 1f;
            graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 700;   // set this value to the desired height of your window
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
            menu = new GameMenu(this);
            bouncerangs = new List<Bouncerang>();
            Projectiles = new List<GameObject>();
			Enemies = new List<Enemy>();
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;
            Bouncerang boomerang1 = new Bouncerang(this);
            boomerang1.Texture = new _2DTexture(Content.Load<Texture2D>("projectiles/boom"), scale);
            bouncerangs.Add(boomerang1);
            environment = new GameEnvironment(this);
			level = new Level (this);
			level.Generate_Level (1);
            scoreBoard = new Scoreboard(this);
            link = new Player(this);
            Input.link = link;
            Input.boom = boomerang1;

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
            #if PLAY_SOUND
                song = Content.Load<Song>("music/metal-zelda");
                Menusong = Content.Load<Song>("music/menu-theme");
                MediaPlayer.IsRepeating = true;
                try
                {
                    MediaPlayer.Play(song);
                    if (!MusicOn)
                        MediaPlayer.Pause();
                    if (!SoundOn)
                        SoundVolume = 0f;
                }
                catch
                {

                }
            #endif
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Paused = !Paused;

            
            if (!Paused)
            {
                Input.CheckKeyboardInput(menu);
                ScreenWidth = GraphicsDevice.Viewport.Width;
                ScreenHeight = GraphicsDevice.Viewport.Height;

                link.Update(gameTime);
                foreach (Bouncerang b in bouncerangs)
                {
                    b.Update(gameTime);
                    b.CheckState(link);
                }
                for(int x = 0; x < Projectiles.Count; x++) 
                {
                    if (Projectiles[x] is Arrow)
                    {
                        Arrow a = (Arrow)Projectiles[x];
                        a.Update(gameTime);
                    }
                }

                base.Update(gameTime);
            }
            else
            {
                Input.MenuInput(menu);
            }
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

            foreach (Bouncerang b in bouncerangs)
            {
                b.Draw(spriteBatch);
            }

            foreach (GameObject p in Projectiles)
            {
                if (p is Arrow)
                {
                    Arrow a = (Arrow)p;
                    a.Draw(spriteBatch);
                }
			}
			foreach (Enemy e in Enemies) {

				e.Draw (spriteBatch);
			}


            link.Draw(spriteBatch);

            spriteBatch.End();
            if (Paused)
                menu.Draw(spriteBatch);

            // TODO: Add your drawing code here    

            base.Draw(gameTime);

        }

        internal void ResetGame()
        {
            Initialize();
            LoadContent();
        }
    }
}
