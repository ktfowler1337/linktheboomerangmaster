using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LinkTheBoomerangMaster.Classes;
using Microsoft.Xna.Framework.Media;

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

        static KeyboardState currentKB, previousKB;

        public static void CheckKeyboardInput()
        {
            previousKB = currentKB;
            currentKB = Keyboard.GetState();
            if (currentKB.IsKeyUp(Keys.Escape) && previousKB.IsKeyDown(Keys.Escape))
            {
                GameController.Paused = !GameController.Paused;
                if(GameController.Paused)
                {
                    // do stuff
                }
                else
                {
                    //do stuff
                }
            }

            if (currentKB.IsKeyUp(Keys.M) && previousKB.IsKeyDown(Keys.M))
            {
                GameController.MusicOn = !GameController.MusicOn;
                if (GameController.MusicOn)
                    MediaPlayer.Resume();
                else
                    MediaPlayer.Pause();
            }

            if (currentKB.IsKeyUp(Keys.N) && previousKB.IsKeyDown(Keys.N))
            {
                GameController.SoundOn = !GameController.SoundOn;
                if (GameController.SoundOn)
                    GameController.SoundVolume = 0.8f;
                else
                    GameController.SoundVolume = 0;
            }
        }


        public static Vector2 GetKeyboardInputDirection(PlayerIndex playerIndex, GameController game)
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

                if(link.ArrowCount > 0)
                {
                    if (keyboardState.IsKeyDown(Keys.Q))
                    {
                        link.ArrowCount -= 1;
                        Arrow a = new Arrow(game);
                        a.Launch();
                    }
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