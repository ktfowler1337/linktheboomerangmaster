using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LinkTheBoomerangMaster.Classes;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Audio;

namespace LinkTheBoomerangMaster
{
    public class Enemy : GameObject
    {
        public GameController _game;
        //Vars
        public int Powerup = 0;
        public int rupees;
        Random _random;
        Rectangle top;
        Rectangle bot;
        Rectangle left;
        Rectangle right;
        string Type = "";



        public Enemy(GameController game, int X, int Y, string type, Random random)
        {

            _random = random;
            _game = game;
            Type = type;
            if (type == "soldier")
            {
                Texture = new _2DTexture(game.Content.Load<Texture2D>("enemies/soldier"), GameController.scale);
                rupees = 1;
            }
            else if (type == "archer")
            {
                Texture = new _2DTexture(game.Content.Load<Texture2D>("enemies/archer"), GameController.scale);
                rupees = 3;
            }
            else if (type == "wizard")
            {
                Texture = new _2DTexture(game.Content.Load<Texture2D>("enemies/firewiz"), GameController.scale);
                rupees = 5;
            }
            Texture._scale = 2;
            Position = new Vector2(X, Y);
            game.Enemies.Add(this);
            Powerup = GetPowerup();
            top = new Rectangle(X, Y, Texture.GetWidth(), 1);
            bot = new Rectangle(X, Y + (Texture.GetHeight() - 1), Texture.GetWidth(), 1);
            left = new Rectangle(X, Y, 1, Texture.GetHeight());
            bot = new Rectangle(X + (Texture.GetWidth() - 1), Y, 1, Texture.GetHeight());
        }

        private int GetPowerup()
        {
            int powerup = _random.Next(1, 30);

            if (powerup > 3)
            {
                return 0;
            }

            return powerup;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        // 1 = x , 2 = y, 0 = none
        public int CheckEnemyCollision(GameObject boom)
        {
            if (top.Intersects(boom.Bounds) || bot.Intersects(boom.Bounds))
                return 2;
            else if (left.Intersects(boom.Bounds) || right.Intersects(boom.Bounds))
                return 1;
            return 0;
        }

        public bool CheckEnemyCollisionBasicBounds(Rectangle v)
        {
            if (this.Bounds.Intersects(v))
                return true;
            return false;
        }

		private void DropPotion()
		{
			if (Powerup != 0)
			{
				Potion myPotion = new Potion (_game,Powerup);
				myPotion.DropPotion (this.Position.X, this.Position.Y, this.Texture.GetWidth (), this.Texture.GetHeight ());
			}
		}

        public void KillEnemy()
        {
            //powerup drop
			this.DropPotion ();
            _game.link.UpdateKillCount();
            _game.link.RupeeCount += this.rupees;
            _game.currentLevelPoints += this.rupees;
            _game.Enemies.Remove(this);
            if (Type == "archer" && !_game.level.ArcherHit)
            {
                GameController.GameSpeedMultiplier2 += 0.2f;
                _game.level.ArcherHit = true;
            }
            else if (Type == "wizard" && !_game.level.WizardHit)
            {
                GameController.GameSpeedMultiplier2 += 0.2f;
                _game.level.WizardHit = true;
            }

        }
    }
}

