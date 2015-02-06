#define PLAY_SOUND
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace LinkTheBoomerangMaster.Classes
{
    public class Bouncerang : GameObject
    {
        public Vector2 Velocity;
        public Random random;
        public bool launched = false;
        private GameController _game;

        private SoundEffect LaunchSound;
        private SoundEffect WallHitSound;
        private SoundEffect EnemyHitSound;
        private SoundEffect ShieldHitSound;

        private const int Frames = 4;
        private const int FramesPerSec = 14;

        public Bouncerang(GameController game)
        {
            Position = new Vector2(0, 0);
            random = new Random();
            _game = game;
            animatedTexture.Load(game.Content, "projectiles/boom", Frames, FramesPerSec);
			#if PLAY_SOUND
            LaunchSound = game.Content.Load<SoundEffect>("sounds/boom-throw");
            WallHitSound = game.Content.Load<SoundEffect>("sounds/boom-wallhit");
            EnemyHitSound = game.Content.Load<SoundEffect>("sounds/LA_BowArrow");
            ShieldHitSound = game.Content.Load<SoundEffect>("sounds/LA_Shield_Deflect");
			#endif
            isMoving = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(launched)
                base.Draw(spriteBatch);            
        }

        public void Update(GameTime time)
        {
            if (launched)
            {
                Move((Velocity * GameController.GameSpeedMultiplier) * GameController.GameSpeedMultiplier2);
                animatedTexture.UpdateFrame((float)time.ElapsedGameTime.TotalSeconds);
            }
        }

        public void CheckState(Player link)
        {
            if (GameObject.CheckLinkBoomCollision(link, this))
            {
				#if PLAY_SOUND
                ShieldHitSound.Play(GameController.SoundVolume, 0, 0);
				#endif
                this.Velocity.Y = Math.Abs(this.Velocity.Y) * -1;
            }

			foreach (Enemy e in _game.Enemies.ToList()) {
                int collisionResult = e.CheckEnemyCollision(this);
                if(collisionResult != 0)
                {
					#if PLAY_SOUND
                    EnemyHitSound.Play(GameController.SoundVolume, 0, 0);
					#endif
                    e.KillEnemy();
                    if(collisionResult == 1)
                    {
                        this.Velocity.X *= -1;
                    }
                    else
                    {
                        this.Velocity.Y *= -1;
                    }
                    break;
                }
			}


            if (this.Position.Y > GameController.ScreenHeight)
            {
                link.throwMode = true;
                this.launched = false;
                this.Position = new Vector2(500, 500);
                link.LifeCount -= 1;
                _game.level.ArcherHit = false;
                _game.level.WizardHit = false;
                _game.link.EnemyDestroyCount = 0;
                
                GameController.GameSpeedMultiplier2 = 1f;
            }
        }

        public void Launch(float speed)
        {
            Position = new Vector2(_game.link.Position.X, _game.link.Position.Y - this.animatedTexture.myTexture.Height - 1);
            // get a random + or - 60 degrees angle to the right
            //float rotation = (float)(Math.PI / 2 + (random.NextDouble() * (Math.PI / 1.5f) - Math.PI / 3));

            Velocity.X = (float)Math.Sin(45);
            Velocity.Y = (float)Math.Cos(90);

            // 50% chance whether it launches left or right
            if (random.Next(2) == 1)
            {
                Velocity.X *= -1; //launch to the left
            }

            Velocity *= (speed );
			#if PLAY_SOUND
            LaunchSound.Play(GameController.SoundVolume, 0, 0);
			#endif
        }

        public void CheckWallCollision()
        {
            bool collided = false;
            if (Position.Y < _game.scoreBoard.scoreboardHeight)
            {

                if (!_game.link.hasHitTheRoof && launched)
                {
                    _game.link.Texture = new _2DTexture(_game.Content.Load<Texture2D>("player/linkidlesmall"), 2);
                    _game.link.animatedTexture.Load(_game.Content, "player/linkwithpaddlesmall", Player.Frames, Player.FramesPerSec);
                    _game.link.hasHitTheRoof = true;
                }
                Position.Y = _game.scoreBoard.scoreboardHeight;
                Velocity.Y *= -1;
                collided = true;
            }
            if (Position.X < _game.environment.VertWallTile.GetWidth())
            {
                Position.X = _game.environment.VertWallTile.GetWidth();
                Velocity.X *= -1;
                collided = true;
            }
            //if (Position.Y + Texture.GetHeight() > GameController.ScreenHeight - _game.environment.HorWallTile.GetHeight())
            //{
            //    Position.Y = GameController.ScreenHeight - _game.environment.HorWallTile.GetHeight() - Texture.GetHeight();
            //    Velocity.Y *= -1;
            //}
            if (Position.X + (animatedTexture.myTexture.Width*GameController.scale) / 4 > GameController.ScreenWidth - _game.environment.VertWallTile.GetWidth())
            {
                Position.X = GameController.ScreenWidth - _game.environment.VertWallTile.GetWidth() - (animatedTexture.myTexture.Width*GameController.scale) / 4;
                Velocity.X *= -1;
                collided = true;
            }
			#if PLAY_SOUND
            if (collided && launched)
                WallHitSound.Play(GameController.SoundVolume, 0, 0);
			#endif
        }

        public override void Move(Vector2 amount)
        {
            base.Move(amount);
            CheckWallCollision();
        }
    }
}
