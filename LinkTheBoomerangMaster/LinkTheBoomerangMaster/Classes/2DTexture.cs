using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkTheBoomerangMaster.Classes
{
    public class _2DTexture
    {
        public _2DTexture(Texture2D _text)
        {
            texture = _text;
        }

        public Texture2D texture;

        public float scale = 1.5f;

        public int GetHeight()
        {
            return (int)(texture.Height * scale);
        }

        public int GetWidth()
        {
            return (int)(texture.Width * scale);
        }
    }
}
