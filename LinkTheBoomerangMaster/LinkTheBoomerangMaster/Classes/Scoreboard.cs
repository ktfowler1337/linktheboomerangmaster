using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkTheBoomerangMaster.Classes
{
    public class Scoreboard
    {

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

        public int scoreboardHeight;

        private GameController _game;
        public Scoreboard(GameController game)
        {
            _game = game;
            scoreboardHeight = (int)(GameController.ScreenHeight * 0.2);
            //scoreboard
            scoreboardBackground = _game.Content.Load<Texture2D>("blackdot");

            scoreboardTopLeftCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("frameLeftTopCorner"), GameController.scale);
            scoreboardTopRightCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("frameRightTopCorner"), GameController.scale);
            scoreboardBotLeftCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("frameLeftBotCorner"), GameController.scale);
            scoreboardBotRightCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("frameRightBotCorner"), GameController.scale);

            scoreboardLeftConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("frameLeftConnector"), GameController.scale);
            scoreboardRightConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("frameRightConnector"), GameController.scale);
            scoreboardTopConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("frameTopConnector"), GameController.scale);
            scoreboardBotConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("frameBotConnector"), GameController.scale);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(scoreboardBackground, new Rectangle(0, 0, GameController.ScreenWidth, scoreboardHeight), Color.White);

            spriteBatch.Draw(scoreboardTopLeftCornerFrame.texture, new Rectangle(0, 0, scoreboardTopLeftCornerFrame.GetWidth(), scoreboardTopLeftCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(scoreboardTopRightCornerFrame.texture, new Rectangle(GameController.ScreenWidth - scoreboardTopRightCornerFrame.GetWidth(), 0, scoreboardTopRightCornerFrame.GetWidth(), scoreboardTopRightCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(scoreboardBotLeftCornerFrame.texture, new Rectangle(0, scoreboardHeight - scoreboardBotLeftCornerFrame.GetHeight(), scoreboardBotLeftCornerFrame.GetWidth(), scoreboardBotLeftCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(scoreboardBotRightCornerFrame.texture, new Rectangle(GameController.ScreenWidth - scoreboardBotRightCornerFrame.GetWidth(), scoreboardHeight - scoreboardBotLeftCornerFrame.GetHeight(), scoreboardBotLeftCornerFrame.GetWidth(), scoreboardBotLeftCornerFrame.GetHeight()), Color.White);


            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(scoreboardLeftConnectorFrame.texture, new Vector2(0, scoreboardBotRightCornerFrame.GetHeight()),
                new Rectangle(0, 0, scoreboardLeftConnectorFrame.GetWidth(), scoreboardHeight - scoreboardBotRightCornerFrame.GetHeight() * 2),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(scoreboardRightConnectorFrame.texture, new Vector2(GameController.ScreenWidth - scoreboardRightConnectorFrame.GetWidth(), scoreboardBotRightCornerFrame.GetHeight()),
                new Rectangle(0, 0, scoreboardLeftConnectorFrame.GetWidth(), scoreboardHeight - scoreboardBotRightCornerFrame.GetHeight() * 2),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(scoreboardBotConnectorFrame.texture, new Vector2(scoreboardRightConnectorFrame.GetWidth(), 0),
                new Rectangle(0, 0, GameController.ScreenWidth - scoreboardTopRightCornerFrame.GetWidth() * 2, scoreboardTopConnectorFrame.GetHeight()),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(scoreboardBotConnectorFrame.texture, new Vector2(scoreboardRightConnectorFrame.GetWidth(), scoreboardHeight - scoreboardBotConnectorFrame.GetHeight()),
                new Rectangle(0, 0, GameController.ScreenWidth - scoreboardTopRightCornerFrame.GetWidth() * 2, scoreboardTopConnectorFrame.GetHeight()),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
