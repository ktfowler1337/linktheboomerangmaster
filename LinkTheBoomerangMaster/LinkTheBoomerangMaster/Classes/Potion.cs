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
	public class Potion : GameObject
	{
		Vector2 Velocity;
		GameController _game;
		const float POTION_SPEED = 5f;
		int powerupType;
		bool Visible = true;
		int rupees;

        private SoundEffect gotItem;

		public Potion (GameController game, int powerup)
		{
			_game = game;
			powerupType = powerup;
			rupees = 5;
			Position = new Vector2(10, 10);
            if (powerupType == 1)
            {
                Texture = new _2DTexture(_game.Content.Load<Texture2D>("projectiles/arrow"), 2);
            }
            else if (powerupType == 2)
            {
                Texture = new _2DTexture(_game.Content.Load<Texture2D>("projectiles/singleBomb"), 2);
            }
            else if (powerupType == 3)
            {
                Texture = new _2DTexture(_game.Content.Load<Texture2D>("projectiles/potion"), 2);
            }
			
			game.Projectiles.Add(this);
            #if PLAY_SOUND
            gotItem = game.Content.Load<SoundEffect>("sounds/LA_Get_PowerUp");
            #endif
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
                #if PLAY_SOUND
                gotItem.Play(GameController.SoundVolume, 0, 0);
                #endif
                string textToDisplay = "You got ";
                if (powerupType == 1) {
					_game.link.ArrowCount += 3;
                    textToDisplay += "Arrows!";
				} else if (powerupType == 2) {
					_game.link.BombCount += 1;
                    textToDisplay += "Bombs!";
				} else if (powerupType == 3) {
					//put in fire code here
					foreach (Bouncerang b in _game.bouncerangs) {
						b.flipSuperang (true);
					}
                    textToDisplay += "the Superang!";
				}
                _game.gotItemText = textToDisplay;
                _game.showItemText = true;
                _game.showItemTextCounter = 0;
				_game.link.RupeeCount += this.rupees;
                _game.currentLevelPoints += this.rupees;
				_game.Projectiles.Remove(this);
			}
		}
	}
}

