using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LinkTheBoomerangMaster
{
    public static class Input
    {
        static Input()
        {

        }
        public static Player link;
        public static Vector2 GetKeyboardInputDirection(PlayerIndex playerIndex)
        {
            Vector2 direction = Vector2.Zero;
            KeyboardState keyboardState = Keyboard.GetState();

            if (playerIndex == PlayerIndex.One)
            {
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    direction.X += -1;
                    link.isMoving = true;
                }
                else if (keyboardState.IsKeyDown(Keys.D))
                {
                    direction.X += 1;
                    link.isMoving = true;
                }
                else
                {
                    link.isMoving = false;
                }
            }

            return direction;
        }
    }
}