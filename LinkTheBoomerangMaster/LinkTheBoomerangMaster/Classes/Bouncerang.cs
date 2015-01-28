using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LinkTheBoomerangMaster.Classes
{
    class Bouncerang : GameObject
    {
        public Vector2 Velocity;
        public Random random;
        public bool launched = false;
        private GameController _game;

        private const int Frames = 4;
        private const int FramesPerSec = 14;

        public Bouncerang(GameController game)
        {
            random = new Random();
            _game = game;
            animatedTexture.Load(game.Content, "boom", Frames, FramesPerSec);
            isMoving = true;
        }

        public void Launch(float speed)
        {
            Position = new Vector2(GameController.ScreenWidth / 2 - Texture.GetWidth() / 2, GameController.ScreenHeight / 2 - Texture.GetHeight() / 2);
            // get a random + or - 60 degrees angle to the right
            float rotation = (float)(Math.PI / 2 + (random.NextDouble() * (Math.PI / 1.5f) - Math.PI / 3));

            Velocity.X = (float)Math.Sin(rotation);
            Velocity.Y = (float)Math.Cos(rotation);

            // 50% chance whether it launches left or right
            if (random.Next(2) == 1)
            {
                Velocity.X *= -1; //launch to the left
            }

            Velocity *= speed;
        }

        public void CheckWallCollision()
        {
            if (Position.Y < _game.scoreBoard.scoreboardHeight)
            {
                Position.Y = _game.scoreBoard.scoreboardHeight;
                Velocity.Y *= -1;
            }
            if (Position.X < _game.environment.VertWallTile.GetWidth())
            {
                Position.X = _game.environment.VertWallTile.GetWidth();
                Velocity.X *= -1;
            }
            if (Position.Y + Texture.GetHeight() > GameController.ScreenHeight - _game.environment.HorWallTile.GetHeight())
            {
                Position.Y = GameController.ScreenHeight - _game.environment.HorWallTile.GetHeight() - Texture.GetHeight();
                Velocity.Y *= -1;
            }
            if (Position.X + (animatedTexture.myTexture.Width*GameController.scale) / 4 > GameController.ScreenWidth - _game.environment.VertWallTile.GetWidth())
            {
                Position.X = GameController.ScreenWidth - _game.environment.VertWallTile.GetWidth() - (animatedTexture.myTexture.Width*GameController.scale) / 4;
                Velocity.X *= -1;
            }
        }

        public override void Move(Vector2 amount)
        {
            base.Move(amount);
            CheckWallCollision();
        }
    }
}
