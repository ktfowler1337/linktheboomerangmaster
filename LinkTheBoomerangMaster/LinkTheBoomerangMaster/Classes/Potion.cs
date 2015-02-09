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
	public class Potion : GameObject
	{
		Vector2 Velocity;
		GameController _game;
		const float POTION_SPEED = 5f;
		bool Visible = true;
		int rupees;

		public Potion (GameController game)
		{
			_game = game;
			rupees = 5;
			Position = new Vector2(10, 10);
			Texture = new _2DTexture(_game.Content.Load<Texture2D>("projectiles/potion"), 2);
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

		public void DropPotion(float X, float Y, int width, int height)
		{
			Position = new Vector2(X + (width / 2), Y + height + 1);

			Velocity.X = (float)Math.Sin(0);
			Velocity.Y = (float)Math.Cos(90)*(float)-1;

			Velocity *= POTION_SPEED;
		}

		public void CheckState(Player link)
		{
			int result = link.CheckLinkCollision(this);
			if (result != -1) {
				//add shit
				_game.link.RupeeCount += this.rupees;
				_game.Projectiles.Remove(this);
			}
		}
	}
}

