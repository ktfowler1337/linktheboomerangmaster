#region Using Statements
using LinkTheBoomerangMaster.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        private _2DTexture background;
        public _2DTexture cornerWallBlock;
        public _2DTexture HorWallTile;
        public _2DTexture VertWallTile;

        public int scoreboardHeight = (int)(ScreenHeight * 0.2);

        //scoreboard
        private Texture2D scoreboardBackground;
        //corners
        private _2DTexture scoreboardTopLeftCornerFrame;
        private _2DTexture scoreboardTopRightCornerFrame;
        private _2DTexture scoreboardBotLeftCornerFrame;
        private _2DTexture scoreboardBotRightCornerFrame;
        //connectors
        private _2DTexture scoreboardLeftConnectorFrame;
        private _2DTexture scoreboardRightConnectorFrame;
        private _2DTexture scoreboardTopConnectorFrame;
        private _2DTexture scoreboardBotConnectorFrame;

        const float BALL_START_SPEED = 8f;

        private Bouncerang boomerang1;

        public GameController()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            background = new _2DTexture(Content.Load<Texture2D>("ground"));
            cornerWallBlock = new _2DTexture(Content.Load<Texture2D>("cornerBlockBot"));
            HorWallTile = new _2DTexture(Content.Load<Texture2D>("wall-H"));
            VertWallTile = new _2DTexture(Content.Load<Texture2D>("wall-V-2"));

            //scoreboard
            scoreboardBackground = Content.Load<Texture2D>("blackdot");

            scoreboardTopLeftCornerFrame = new _2DTexture(Content.Load<Texture2D>("frameLeftTopCorner"));
            scoreboardTopRightCornerFrame = new _2DTexture(Content.Load<Texture2D>("frameRightTopCorner"));
            scoreboardBotLeftCornerFrame = new _2DTexture(Content.Load<Texture2D>("frameLeftBotCorner"));
            scoreboardBotRightCornerFrame = new _2DTexture(Content.Load<Texture2D>("frameRightBotCorner"));

            scoreboardLeftConnectorFrame = new _2DTexture(Content.Load<Texture2D>("frameLeftConnector"));
            scoreboardRightConnectorFrame = new _2DTexture(Content.Load<Texture2D>("frameRightConnector")); 
            scoreboardTopConnectorFrame = new _2DTexture(Content.Load<Texture2D>("frameTopConnector"));
            scoreboardBotConnectorFrame = new _2DTexture(Content.Load<Texture2D>("frameBotConnector"));

            boomerang1.Texture = Content.Load<Texture2D>("rupee");
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
            scoreboardHeight = (int)(ScreenHeight * 0.2);

            boomerang1.Move(boomerang1.Velocity);

            if (!boomerang1.launched)
            {
                boomerang1.Launch(BALL_START_SPEED);
                boomerang1.launched = true;
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
            //background
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(background.texture, Vector2.Zero, new Rectangle(0, 0, 800, 480), Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
            
            spriteBatch.End();

            // bottom wall
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(HorWallTile.texture, new Vector2(HorWallTile.GetWidth(), ScreenHeight - HorWallTile.GetHeight()),
                new Rectangle(0, 0, ScreenWidth - HorWallTile.GetWidth() * 2, HorWallTile.GetHeight()), 
                Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
            spriteBatch.End();

            //left wall
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(VertWallTile.texture, new Vector2(0, scoreboardHeight),
                new Rectangle(0, 0, VertWallTile.texture.Width, ScreenHeight - scoreboardHeight - cornerWallBlock.GetHeight()),
                Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);

            spriteBatch.End();

            //right wall
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(VertWallTile.texture, new Vector2(ScreenWidth - VertWallTile.GetWidth(), scoreboardHeight),
                new Rectangle(0, 0, VertWallTile.texture.Width, ScreenHeight - scoreboardHeight - cornerWallBlock.GetHeight()),
                Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);

            spriteBatch.End();

            // corner blocks
            spriteBatch.Begin();

            spriteBatch.Draw(cornerWallBlock.texture, new Rectangle(0, ScreenHeight - cornerWallBlock.GetHeight(), cornerWallBlock.GetWidth(), cornerWallBlock.GetHeight()), Color.White);
            spriteBatch.Draw(cornerWallBlock.texture, new Rectangle(ScreenWidth - cornerWallBlock.GetWidth(), ScreenHeight - cornerWallBlock.GetHeight(), cornerWallBlock.GetWidth(), cornerWallBlock.GetHeight()), Color.White);
            spriteBatch.End();

            drawScoreboard();

            spriteBatch.Begin();

            boomerang1.Draw(spriteBatch);

            spriteBatch.End();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void drawScoreboard()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(scoreboardBackground, new Rectangle(0, 0, ScreenWidth, scoreboardHeight), Color.White);

            spriteBatch.Draw(scoreboardTopLeftCornerFrame.texture, new Rectangle(0, 0, scoreboardTopLeftCornerFrame.GetWidth(), scoreboardTopLeftCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(scoreboardTopRightCornerFrame.texture, new Rectangle(ScreenWidth - scoreboardTopRightCornerFrame.GetWidth(), 0, scoreboardTopRightCornerFrame.GetWidth(), scoreboardTopRightCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(scoreboardBotLeftCornerFrame.texture, new Rectangle(0, scoreboardHeight - scoreboardBotLeftCornerFrame.GetHeight(), scoreboardBotLeftCornerFrame.GetWidth(), scoreboardBotLeftCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(scoreboardBotRightCornerFrame.texture, new Rectangle(ScreenWidth - scoreboardBotRightCornerFrame.GetWidth(), scoreboardHeight - scoreboardBotLeftCornerFrame.GetHeight(), scoreboardBotLeftCornerFrame.GetWidth(), scoreboardBotLeftCornerFrame.GetHeight()), Color.White);


            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(scoreboardLeftConnectorFrame.texture, new Vector2(0, scoreboardBotRightCornerFrame.GetHeight()),
                new Rectangle(0, 0, scoreboardLeftConnectorFrame.GetWidth(), scoreboardHeight - scoreboardBotRightCornerFrame.GetHeight() * 2),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(scoreboardRightConnectorFrame.texture, new Vector2(ScreenWidth - scoreboardRightConnectorFrame.GetWidth(), scoreboardBotRightCornerFrame.GetHeight()),
                new Rectangle(0, 0, scoreboardLeftConnectorFrame.GetWidth(), scoreboardHeight - scoreboardBotRightCornerFrame.GetHeight() * 2),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(scoreboardBotConnectorFrame.texture, new Vector2(scoreboardRightConnectorFrame.GetWidth(), 0),
                new Rectangle(0, 0, ScreenWidth - scoreboardTopRightCornerFrame.GetWidth() * 2, scoreboardTopConnectorFrame.GetHeight()),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(scoreboardBotConnectorFrame.texture, new Vector2(scoreboardRightConnectorFrame.GetWidth(), scoreboardHeight - scoreboardBotConnectorFrame.GetHeight()),
                new Rectangle(0, 0, ScreenWidth - scoreboardTopRightCornerFrame.GetWidth() * 2, scoreboardTopConnectorFrame.GetHeight()),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
