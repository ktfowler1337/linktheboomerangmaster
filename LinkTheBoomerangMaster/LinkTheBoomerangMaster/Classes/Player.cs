using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LinkTheBoomerangMaster.Classes;
using Microsoft.Xna.Framework.Graphics;

namespace LinkTheBoomerangMaster
{
    public class Player : GameObject
    {
        public GameController _game;

        private const int Frames = 8;
        private const int FramesPerSec = 6;

        public Player(GameController game) : base(1.5f)
        {
            _game = game;
            Texture = new _2DTexture(game.Content.Load<Texture2D>("linkidle"), GameController.scale);
            Position = new Vector2((GameController.ScreenWidth / 2) - (Texture.GetWidth() / 2), GameController.ScreenHeight - game.environment.HorWallTile.GetHeight() - (int)(GameController.ScreenHeight * 0.01) - Texture.GetHeight());
            animatedTexture.Load(game.Content, "link", Frames, FramesPerSec);
        }

        public override void Move(Vector2 amount)
        {
            base.Move(amount);
            if (Position.X <= _game.environment.VertWallTile.GetWidth())
                Position.X = _game.environment.VertWallTile.GetWidth();
            if (Position.X + Texture.GetWidth() >= GameController.ScreenWidth - _game.environment.VertWallTile.GetWidth())
                Position.X = GameController.ScreenWidth - Texture.GetWidth() - _game.environment.VertWallTile.GetWidth();
        }
    }
}