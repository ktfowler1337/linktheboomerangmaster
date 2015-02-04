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
        //icons
        private _2DTexture bomb;
        private _2DTexture arrow;
        private _2DTexture potion;
        private _2DTexture heart;
        private _2DTexture heartEmpty;
        private _2DTexture rupee;

        private SpriteFont font;
        private SpriteFont fontSmaller;

        public int scoreboardHeight;

        private GameController _game;
        public Scoreboard(GameController game)
        {
            _game = game;
            scoreboardHeight = 120;
            //scoreboard
            scoreboardBackground = _game.Content.Load<Texture2D>("scoreboard/blackdot");

            scoreboardTopLeftCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameLeftTopCorner"), GameController.scale);
            scoreboardTopRightCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameRightTopCorner"), GameController.scale);
            scoreboardBotLeftCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameLeftBotCorner"), GameController.scale);
            scoreboardBotRightCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameRightBotCorner"), GameController.scale);

            scoreboardLeftConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameLeftConnector"), GameController.scale);
            scoreboardRightConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameRightConnector"), GameController.scale);
            scoreboardTopConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameTopConnector"), GameController.scale);
            scoreboardBotConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameBotConnector"), GameController.scale);

            bomb = new _2DTexture(_game.Content.Load<Texture2D>("icon/singleBomb"), 2);
            arrow = new _2DTexture(_game.Content.Load<Texture2D>("icon/singleArrow"), 1.5f);
            potion = new _2DTexture(_game.Content.Load<Texture2D>("projectiles/potion"), GameController.scale);
            heart = new _2DTexture(_game.Content.Load<Texture2D>("icon/heartFULL"), 2.5f);
            heartEmpty = new _2DTexture(_game.Content.Load<Texture2D>("icon/heartEMPTY"), 2.5f);
            rupee = new _2DTexture(_game.Content.Load<Texture2D>("icon/rupee"), 1.3f);

            font = _game.Content.Load<SpriteFont>("fonts/font");
            fontSmaller = _game.Content.Load<SpriteFont>("fonts/font12");
        }

        public void Draw(SpriteBatch spriteBatch)
        {               
            DrawFrame(spriteBatch);
            DrawIcons(spriteBatch);
            DrawText(spriteBatch);
        }

        private void DrawText(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //Life Text
            spriteBatch.DrawString(font, "-Life-", new Vector2((GameController.ScreenWidth / 2) - 53, -5), Color.White);

            //Bomb Count Text
            spriteBatch.DrawString(fontSmaller, "x " + _game.link.BombCount.ToString("D3"), new Vector2(GameController.ScreenWidth - 85, -6), Color.White);

            //Arrow Count Text
            spriteBatch.DrawString(fontSmaller, "x " + _game.link.ArrowCount.ToString("D3"), new Vector2(GameController.ScreenWidth - 85, 30), Color.White);

            //Rupee Count Text
            spriteBatch.DrawString(fontSmaller, "x " + _game.link.RupeeCount.ToString("D3"), new Vector2(GameController.ScreenWidth - 85, 65), Color.White);

            //Music ON/OFF Text
            spriteBatch.DrawString(fontSmaller, "Music: "  + (GameController.MusicOn ? "ON" : "OFF") , new Vector2(scoreboardLeftConnectorFrame.GetWidth() + 5, (int)(scoreboardHeight * 0.10)), Color.White);

            //Sound ON/OFF Text
            spriteBatch.DrawString(fontSmaller, "Sound: " + (GameController.SoundOn ? "ON" : "OFF"), new Vector2(scoreboardLeftConnectorFrame.GetWidth() + 5, (int)(scoreboardHeight * 0.45)), Color.White);

            spriteBatch.End();
        }

        private void DrawIcons(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //Bomb Icon
            spriteBatch.Draw(bomb.texture, new Rectangle(
                GameController.ScreenWidth - scoreboardRightConnectorFrame.GetWidth() - (int)(GameController.ScreenWidth * 0.17) -3,
                 (int)(scoreboardHeight *0.1),
                bomb.GetWidth(), bomb.GetHeight()), Color.White);

            //Arrow Icon
            spriteBatch.Draw(arrow.texture, new Rectangle(
                GameController.ScreenWidth - scoreboardRightConnectorFrame.GetWidth() - (int)(GameController.ScreenWidth * 0.17) -3,
                 (int)(scoreboardHeight * 0.4 - 2),
                arrow.GetWidth(), arrow.GetHeight()), Color.White);

            //Rupee Icon
            spriteBatch.Draw(rupee.texture, new Rectangle(
                GameController.ScreenWidth - scoreboardRightConnectorFrame.GetWidth() - (int)(GameController.ScreenWidth * 0.17),
                 (int)(scoreboardHeight * 0.7),
                rupee.GetWidth(), rupee.GetHeight()), Color.White);

            //Heart1 Icon
            spriteBatch.Draw((_game.link.LifeCount > 0 ? heart.texture : heartEmpty.texture), new Rectangle(
                (GameController.ScreenWidth / 2) - (int)((heart.GetWidth() * 1.5f) + 5) - heart.GetWidth(),
                 (int)(scoreboardHeight * 0.5) - (heart.GetHeight() /2),
                heart.GetWidth(), heart.GetHeight()), Color.White);

            //Heart3 Icon
            spriteBatch.Draw((_game.link.LifeCount > 2 ? heart.texture : heartEmpty.texture), new Rectangle(
                (GameController.ScreenWidth / 2) + (int)((heart.GetWidth() * 1.5f) + 5) - heart.GetWidth(),
                 (int)(scoreboardHeight * 0.5) - (heart.GetHeight() / 2),
                heart.GetWidth(), heart.GetHeight()), Color.White);

            //Heart2 Icon
            spriteBatch.Draw((_game.link.LifeCount > 1 ? heart.texture : heartEmpty.texture), new Rectangle(
                (GameController.ScreenWidth / 2) - heart.GetWidth(),
                 (int)(scoreboardHeight * 0.5) - (heart.GetHeight() / 2),
                heart.GetWidth(), heart.GetHeight()), Color.White);

            spriteBatch.End();
        }

        private void DrawFrame(SpriteBatch spriteBatch)
        {
            DrawFrameCorners(spriteBatch);

            DrawFrameEdges(spriteBatch);
        }

        private void DrawFrameCorners(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //corners
            spriteBatch.Draw(scoreboardBackground, new Rectangle(0, 0, GameController.ScreenWidth, scoreboardHeight), Color.White);

            spriteBatch.Draw(scoreboardTopLeftCornerFrame.texture, new Rectangle(0, 0, scoreboardTopLeftCornerFrame.GetWidth(), scoreboardTopLeftCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(scoreboardTopRightCornerFrame.texture, new Rectangle(GameController.ScreenWidth - scoreboardTopRightCornerFrame.GetWidth(), 0, scoreboardTopRightCornerFrame.GetWidth(), scoreboardTopRightCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(scoreboardBotLeftCornerFrame.texture, new Rectangle(0, scoreboardHeight - scoreboardBotLeftCornerFrame.GetHeight(), scoreboardBotLeftCornerFrame.GetWidth(), scoreboardBotLeftCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(scoreboardBotRightCornerFrame.texture, new Rectangle(GameController.ScreenWidth - scoreboardBotRightCornerFrame.GetWidth(), scoreboardHeight - scoreboardBotLeftCornerFrame.GetHeight(), scoreboardBotLeftCornerFrame.GetWidth(), scoreboardBotLeftCornerFrame.GetHeight()), Color.White);


            spriteBatch.End();
        }

        private void DrawFrameEdges(SpriteBatch spriteBatch)
        {
            //frame edges
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
