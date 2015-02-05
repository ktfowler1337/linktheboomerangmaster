using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LinkTheBoomerangMaster.Classes;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LinkTheBoomerangMaster
{
	public class Enemy : GameObject
	{
		public GameController _game;
		//Vars
		public int Powerup = 0;
		public int rupees;
		Random _random;

		public Enemy (GameController game, int X, int Y, string type)
		{
			_random = new Random();
			_game = game;
			if (type == "soldier") {
				Texture = new _2DTexture (game.Content.Load<Texture2D> ("enemies/soldier"), GameController.scale);
				rupees = 1;
			} else if (type == "archer") {
				Texture = new _2DTexture (game.Content.Load<Texture2D> ("enemies/archer"), GameController.scale);
				rupees = 3;
			} else if (type == "wizard") {
				Texture = new _2DTexture (game.Content.Load<Texture2D> ("enemies/firewiz"), GameController.scale);
				rupees = 5;
			}
			Texture._scale = 2;
			Position = new Vector2(X, Y);
			game.Enemies.Add(this);
			Powerup = GetPowerup ();
		}

		private int GetPowerup()
		{
			int powerup = _random.Next(1,30);

			if (powerup > 3) {
				return 0;
			}

			return powerup;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}

		public bool CheckEnemyCollision(Bouncerang boom)
		{
			if (this.Bounds.Intersects(boom.Bounds))
				return true;
			return false;
		}

		public void KillEnemy()
		{
			//powerup drop
			//add rupees
			_game.Enemies.Remove (this);
		}
	}
}

