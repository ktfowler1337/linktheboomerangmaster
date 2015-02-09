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

        public int EnemyDestroyCount = 0;

        public const int Frames = 8;
        public const int FramesPerSec = 6;

        public bool throwMode = true;

        public AnimatedTexture throwAni;

        private _2DTexture throwIdle;

        public bool hasHitTheRoof = false;

        const float KEYBOARD_PLAYER_SPEED = 10f;

        public int BombCount = 10;
        public int ArrowCount = 10;
        public int LifeCount = 3;
        public int RupeeCount = 0;

        public Rectangle shield;

        public Player(GameController game, int rupees, int arrowCount = 0, int bombCount =0) : base(2f)
        {
            _game = game;
            ArrowCount = arrowCount;
            BombCount = bombCount;
            RupeeCount = rupees;
            throwAni = new AnimatedTexture(Position, 0, 2, 0.5f);
            Texture = new _2DTexture(game.Content.Load<Texture2D>("player/linkidle" + (GameController.Difficulty == "Normal" ? "" : "small")), GameController.scale);
            throwIdle = new _2DTexture(game.Content.Load<Texture2D>("player/linkidlewithboom"), 2);
            Texture._scale = 2;
            Position = new Vector2((GameController.ScreenWidth / 2) - (Texture.GetWidth() / 2), GameController.ScreenHeight - game.environment.HorWallTile.GetHeight() - (int)(GameController.ScreenHeight * 0.01) - Texture.GetHeight());
            animatedTexture.Load(game.Content, "player/linkwithpaddle"+ (GameController.Difficulty == "Normal" ? "" :"small" ), Frames, FramesPerSec);
            throwAni.Load(game.Content, "player/linkwithboom", Frames, FramesPerSec);
            shield = new Rectangle((int)Position.X, (int)Position.Y, Texture.GetWidth(), 8);
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
            shield = new Rectangle((int)Position.X, (int)Position.Y, Texture.GetWidth(), 8);
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

        internal void UpdateKillCount()
        {
            EnemyDestroyCount++;
            if (EnemyDestroyCount == 4)
                GameController.GameSpeedMultiplier2 = 1.2f;
            else if(EnemyDestroyCount == 12)
                GameController.GameSpeedMultiplier2 = 1.5f;
        }

		public Rectangle GetLeftShield ()
		{
			return new Rectangle(shield.X,shield.Y,shield.Width/3,shield.Height);
		}

		public Rectangle GetRightShield ()
		{

			return new Rectangle(shield.X+48,shield.Y,shield.Width/3,shield.Height);
		}

		public int CheckLinkCollision( GameObject boom)
		{
			if (this.shield.Intersects (boom.Bounds)) {
				Rectangle leftSide = this.GetLeftShield ();
				Rectangle rightSide = this.GetRightShield ();

				//if contants left side
				if (leftSide.Intersects (boom.Bounds)) {
					return 1;
				}

				//if contants right side
				if (rightSide.Intersects (boom.Bounds)) {
					return 2;
				}
				//else middle
				return 0;
			}
			return -1;
		}
    }
}