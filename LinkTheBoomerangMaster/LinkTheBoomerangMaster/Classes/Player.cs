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

        public const int Frames = 8;
        public const int FramesPerSec = 6;

        public bool throwMode = true;

        public AnimatedTexture throwAni;

        private _2DTexture throwIdle;

        public bool hasHitTheRoof = false;

        const float KEYBOARD_PLAYER_SPEED = 10f;

        public int BombCount = 0;
        public int ArrowCount = 10;
        public int LifeCount = 3;
        public int RupeeCount = 0;

        public Player(GameController game) : base(2f)
        {
            _game = game;

            throwAni = new AnimatedTexture(Position, 0, 2, 0.5f);
            Texture = new _2DTexture(game.Content.Load<Texture2D>("player/linkidle"), GameController.scale);
            throwIdle = new _2DTexture(game.Content.Load<Texture2D>("player/linkidlewithboom"), 2);
            Texture._scale = 2;
            Position = new Vector2((GameController.ScreenWidth / 2) - (Texture.GetWidth() / 2), GameController.ScreenHeight - game.environment.HorWallTile.GetHeight() - (int)(GameController.ScreenHeight * 0.01) - Texture.GetHeight());
            animatedTexture.Load(game.Content, "player/linkwithpaddle", Frames, FramesPerSec);
            throwAni.Load(game.Content, "player/linkwithboom", Frames, FramesPerSec);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (throwMode)
            {
                if (isMoving)
                {
                    throwAni.DrawFrame(spriteBatch, Position);
                }
                else
                {
                    animatedTexture.Reset();
                    spriteBatch.Draw(throwIdle.texture, new Rectangle((int)Position.X, (int)Position.Y, throwIdle.GetWidth(), throwIdle.GetHeight()), Color.White);
                }
            }
            else
            {
                base.Draw(spriteBatch);
            }
        }

        public void Update(GameTime time)
        {
            Move(Input.GetKeyboardInputDirection(PlayerIndex.One, _game) * KEYBOARD_PLAYER_SPEED);
            if (isMoving)
            {
                animatedTexture.UpdateFrame((float)time.ElapsedGameTime.TotalSeconds);
                throwAni.UpdateFrame((float)time.ElapsedGameTime.TotalSeconds);
            }
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