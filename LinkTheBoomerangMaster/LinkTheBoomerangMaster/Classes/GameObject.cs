using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LinkTheBoomerangMaster.Classes;
using Microsoft.Xna.Framework.Media;

namespace LinkTheBoomerangMaster
{
    public class GameObject
    {
        public Vector2 Position;
        public _2DTexture Texture;
        public bool isMoving = false;
        public AnimatedTexture animatedTexture;

        private float scaleOveride = 0;

        public GameObject(float scale = 0)
        {
            if (scale == 0)
                scale = GameController.scale;
            animatedTexture = new AnimatedTexture(Position, 0, scale, 0.5f);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isMoving)
            {
                animatedTexture.DrawFrame(spriteBatch, Position);
            }
            else
            {
                animatedTexture.Reset();
                spriteBatch.Draw(Texture.texture, new Rectangle((int)Position.X, (int)Position.Y, Texture.GetWidth(), Texture.GetHeight()), Color.White);
            }                
        }

        public virtual void Move(Vector2 amount)
        {
            Position += amount;
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.GetWidth(), Texture.GetHeight()); }
        }

        public static bool CheckLinkBoomCollision(Player player, Bouncerang boom)
        {
            if (player.Bounds.Intersects(boom.Bounds))
                return true;
            return false;
        }
    }
    
}
