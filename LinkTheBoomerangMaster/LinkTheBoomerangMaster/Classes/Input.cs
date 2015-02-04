using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LinkTheBoomerangMaster.Classes;

namespace LinkTheBoomerangMaster
{
    public static class Input
    {
        static Input()
        {

        }
        public static Player link;
        public static Bouncerang boom;
        const float BALL_START_SPEED = 8f;

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

                if(link.throwMode)
                {
                    if (keyboardState.IsKeyDown(Keys.Space))
                    {
                        link.throwMode = false;
                        boom.launched = true;
                        boom.Launch(BALL_START_SPEED);
                    }
                }
            }

            return direction;
        }
    }
}