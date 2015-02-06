//Leave this defined unless your name is adrian on Ubuntu lol
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
    public class Arrow : GameObject
    {
        Vector2 Velocity;
        GameController _game;
        const float ARROW_START_SPEED = 25f;

        SoundEffect LaunchSound;
        SoundEffect WallHitSound;

        bool Visible = true;

        public Arrow(GameController game)
        {
            Position = new Vector2(10, 10);
			#if PLAY_SOUND
            LaunchSound = game.Content.Load<SoundEffect>("sounds/LA_BowArrow");
            WallHitSound = game.Content.Load<SoundEffect>("sounds/boom-wallhit");
			#endif
            _game = game;
            Texture = new _2DTexture(_game.Content.Load<Texture2D>("projectiles/arrow"), 2);
            game.Projectiles.Add(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
                base.Draw(spriteBatch);
        }

        public void Update(GameTime time)
        {
            if (Visible)
                Move(Velocity);
        }

        public void Launch()
        {
            Position = new Vector2(_game.link.Position.X + (_game.link.Texture.GetWidth() /2), _game.link.Position.Y - this.Texture.GetHeight() - 1);

            Velocity.X = (float)Math.Sin(0);
            Velocity.Y = (float)Math.Cos(90);

            Velocity *= ARROW_START_SPEED;
			#if PLAY_SOUND
            LaunchSound.Play(GameController.SoundVolume, 0, 0);
#endif
        }

        

        public void CheckEnemyCollision()
        {
            foreach (Enemy e in _game.Enemies.ToList())
            {
                if (e.CheckEnemyCollision(this) != 0)
                {
                    e.KillEnemy();
                    _game.Projectiles.Remove(this);
                    break;
                }
            }
            if (this.Position.Y <= _game.scoreBoard.scoreboardHeight)
            {
				#if PLAY_SOUND
                if (Visible)

                    //WallHitSound.Play(GameController.SoundVolume, 0, 0);
				#endif
                Visible = false;
            }
        }

        public override void Move(Vector2 amount)
        {
            base.Move(amount);
            CheckEnemyCollision();
        }
    }
}
