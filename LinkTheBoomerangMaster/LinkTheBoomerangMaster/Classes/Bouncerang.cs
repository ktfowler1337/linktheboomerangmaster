#define PLAY_SOUND
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

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

        public bool isSuperang = false;

        private int angleAdjustment = 15;

        private const int Frames = 4;
        private const int FramesPerSec = 14;

        private double angleOfReflection = 45;

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

        public void flipSuperang(bool state)
        {
            if (state)
            {
                animatedTexture.Load(_game.Content, "projectiles/superang", Frames, FramesPerSec);
            }
            else
            {
                animatedTexture.Load(_game.Content, "projectiles/boom", Frames, FramesPerSec);
            }
            isSuperang = state;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (launched)
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
            int result = link.CheckLinkCollision(this);
            if (result != -1)
            {
#if PLAY_SOUND
                ShieldHitSound.Play(GameController.SoundVolume, 0, 0);
#endif
                //centre
                Boolean flipX = false;
                double hyp = Math.Sqrt((this.Velocity.Y * this.Velocity.Y + this.Velocity.X * this.Velocity.X));
                if (result == 0)
                {
                    this.Velocity.Y = Math.Abs(this.Velocity.Y) * -1;
                }
                else if (result == 1)
                {
                    //left

                    if (this.Velocity.X > 0)
                    {
                        angleOfReflection += angleAdjustment;
                    }
                    else
                    {
                        if (angleOfReflection > 25)
                        {
                            angleOfReflection -= angleAdjustment;
                            flipX = true;
                        }
                    }

                    this.Velocity.Y = getNewY(hyp, angleOfReflection);
                    this.Velocity.X = getNewX(hyp, angleOfReflection);
                    this.Velocity.Y = this.Velocity.Y * -1;
                    if (flipX)
                        this.Velocity.X *= -1;
                }
                else if (result == 2)
                {

                    if (this.Velocity.X > 0)
                    {
                        if (angleOfReflection > 25)
                        {
                            angleOfReflection -= angleAdjustment;
                        }
                    }
                    else
                    {

                        angleOfReflection += angleAdjustment;
                        flipX = true;
                    }

                    this.Velocity.Y = getNewY(hyp, angleOfReflection);
                    this.Velocity.X = getNewX(hyp, angleOfReflection);
                    this.Velocity.Y = this.Velocity.Y * -1;
                    if (flipX)
                        this.Velocity.X *= -1;
                }

            }

            foreach (Enemy e in _game.Enemies.ToList())
            {
                int collisionResult = e.CheckEnemyCollision(this);
                if (collisionResult != 0)
                {
#if PLAY_SOUND
                    EnemyHitSound.Play(GameController.SoundVolume, 0, 0);
#endif
                    e.KillEnemy();
                    if (!isSuperang)
                    {
                        if (collisionResult == 1)
                        {
                            this.Velocity.X *= -1;
                        }
                        else
                        {
                            this.Velocity.Y *= -1;
                        }
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
                flipSuperang(false);
                _game.level.ArcherHit = false;
                _game.level.WizardHit = false;
                _game.link.EnemyDestroyCount = 0;

                GameController.GameSpeedMultiplier2 = 1f;
                if (link.LifeCount <= 0)
                {
                    _game.isGameOver = true;
                    _game.menu.currentMenu = "File";
                    GameController.Paused = true;
                    #if PLAY_SOUND
                    MediaPlayer.Stop();
                    if (GameController.MusicOn)
                        MediaPlayer.Play(GameController.Menusong);
                    #endif

                }
            }
        }

        private float getNewY(double hyp, double angleOfReflection)
        {
            return (float)(hyp * Math.Sin(angleOfReflection * (Math.PI / 180)));
        }

        private float getNewX(double hyp, double angleOfReflection)
        {
            return (float)(hyp * Math.Cos(angleOfReflection * (Math.PI / 180)));
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

            Velocity *= (speed);
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
            if (Position.X + (animatedTexture.myTexture.Width * GameController.scale) / 4 > GameController.ScreenWidth - _game.environment.VertWallTile.GetWidth())
            {
                Position.X = GameController.ScreenWidth - _game.environment.VertWallTile.GetWidth() - (animatedTexture.myTexture.Width * GameController.scale) / 4;
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
