using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkTheBoomerangMaster.Classes
{
    public class GameEnvironment
    {
        private GameController _game;
        private _2DTexture background;
        public _2DTexture cornerWallBlock;
        public _2DTexture HorWallTile;
        public _2DTexture VertWallTile;

        public GameEnvironment(GameController game)
        {
            _game = game;
            background = new _2DTexture(game.Content.Load<Texture2D>("map/ground"), GameController.scale);
            cornerWallBlock = new _2DTexture(game.Content.Load<Texture2D>("map/cornerBlockBot"), GameController.scale);
            HorWallTile = new _2DTexture(game.Content.Load<Texture2D>("map/wall-H"), GameController.scale);
            VertWallTile = new _2DTexture(game.Content.Load<Texture2D>("map/wall-V-2"), GameController.scale);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //background
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(background.texture, Vector2.Zero, new Rectangle(0, 0, GameController.ScreenWidth, GameController.ScreenHeight), Color.White, 0, Vector2.Zero, GameController.scale, SpriteEffects.None, 0);

            spriteBatch.End();

            // bottom wall
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(HorWallTile.texture, new Vector2(HorWallTile.GetWidth(), GameController.ScreenHeight - HorWallTile.GetHeight()),
                new Rectangle(0, 0, GameController.ScreenWidth - HorWallTile.GetWidth() * 2, HorWallTile.GetHeight()),
                Color.White, 0, Vector2.Zero, GameController.scale, SpriteEffects.None, 0);
            spriteBatch.End();

            //left wall
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(VertWallTile.texture, new Vector2(0, _game.scoreBoard.scoreboardHeight),
                new Rectangle(0, 0, VertWallTile.texture.Width, GameController.ScreenHeight - _game.scoreBoard.scoreboardHeight - cornerWallBlock.GetHeight()),
                Color.White, 0, Vector2.Zero, GameController.scale, SpriteEffects.None, 0);

            spriteBatch.End();

            //right wall
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(VertWallTile.texture, new Vector2(GameController.ScreenWidth - VertWallTile.GetWidth(), _game.scoreBoard.scoreboardHeight),
                new Rectangle(0, 0, VertWallTile.texture.Width, GameController.ScreenHeight - _game.scoreBoard.scoreboardHeight - cornerWallBlock.GetHeight()),
                Color.White, 0, Vector2.Zero, GameController.scale, SpriteEffects.None, 0);

            spriteBatch.End();

            // corner blocks
            spriteBatch.Begin();

            spriteBatch.Draw(cornerWallBlock.texture, new Rectangle(0, GameController.ScreenHeight - cornerWallBlock.GetHeight(), cornerWallBlock.GetWidth(), cornerWallBlock.GetHeight()), Color.White);
            spriteBatch.Draw(cornerWallBlock.texture, new Rectangle(GameController.ScreenWidth - cornerWallBlock.GetWidth(), GameController.ScreenHeight - cornerWallBlock.GetHeight(), cornerWallBlock.GetWidth(), cornerWallBlock.GetHeight()), Color.White);
            spriteBatch.End();
        }
    }
}
