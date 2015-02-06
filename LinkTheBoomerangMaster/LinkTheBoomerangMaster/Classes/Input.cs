//Leave this defined unless your name is adrian on Ubuntu lol
#define PLAY_SOUND
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LinkTheBoomerangMaster.Classes;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

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
        private static bool ArrowButtonPressed = false;

        static KeyboardState currentKB, previousKB;

        public static void CheckKeyboardInput(GameMenu menu)
        {
            previousKB = currentKB;
            currentKB = Keyboard.GetState();
            if (currentKB.IsKeyUp(Keys.Escape) && previousKB.IsKeyDown(Keys.Escape))
            {
				#if PLAY_SOUND
                MediaPlayer.Stop();
                if(GameController.MusicOn)

                    MediaPlayer.Play(GameController.Menusong);
                menu.menuOpen.Play(GameController.SoundVolume,0,0);
				#endif
                GameController.Paused = !GameController.Paused;
            }

            if (currentKB.IsKeyUp(Keys.M) && previousKB.IsKeyDown(Keys.M))
            {
				#if PLAY_SOUND
                GameController.MusicOn = !GameController.MusicOn;
                if (GameController.MusicOn)
                    MediaPlayer.Play(GameController.song);
                else
                    MediaPlayer.Stop();
				#endif
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

        public static void MenuInput(GameMenu menu)
        {
            previousKB = currentKB;
            currentKB = Keyboard.GetState();
            if (currentKB.IsKeyUp(Keys.Escape) && previousKB.IsKeyDown(Keys.Escape))
            {
                if (menu.currentMenu == "Start")
                {
                    GameController.Paused = false;
                    MediaPlayer.Stop();
					#if PLAY_SOUND
                    if(GameController.MusicOn)
                        MediaPlayer.Play(GameController.song);
					#endif
                }
                else if (menu.currentMenu == "About" || menu.currentMenu == "Info" || menu.currentMenu == "Controls")
                {
                    menu.currentMenu = "Help";
                }
                else
                {
                    menu.currentMenu = "Start";
                }
				#if PLAY_SOUND
                menu.menuClose.Play(GameController.SoundVolume, 0, 0);
				#endif
                menu.Selected = 1;
            }
            else if (currentKB.IsKeyUp(Keys.Enter) && previousKB.IsKeyDown(Keys.Enter))
            {
                if (menu.currentMenu == "Start")
                {
                    if (menu.Selected == 1)
                    {
                        menu.currentMenu = "File";
                    }
                    else if (menu.Selected == 2)
                    {
                        menu.currentMenu = "Tools";
                    }
                    else
                    {
                        menu.currentMenu = "Help";
                    }
                    menu.Selected = 1;
                }
                else if (menu.currentMenu == "File")
                {
                    if (menu.Selected == 1)
                    {
                        link._game.ResetGame();
                    }
                    else if (menu.Selected == 2)
                    {
                        link._game.Exit();
                    }
                }
                else if(menu.currentMenu == "Help")
                {
                    if (menu.Selected == 1)
                    {
                        menu.currentMenu = "About";
                    }
                    else if (menu.Selected == 2)
                    {
                        menu.currentMenu = "Info";
                    }
                    else if (menu.Selected == 3)
                    {
                        menu.currentMenu = "Controls";
                    }
                    menu.Selected = 1;
                }
				#if PLAY_SOUND
                menu.selectItem.Play(GameController.SoundVolume, 0, 0);
				#endif
            }
            else if (currentKB.IsKeyUp(Keys.W) && previousKB.IsKeyDown(Keys.W))
            {
				#if PLAY_SOUND
                menu.menuMove.Play(GameController.SoundVolume, 0, 0);
				#endif
                int max = menu.currentMenu == "File" ? 2 : 3;
                if (menu.Selected != 1)
                    menu.Selected -= 1;
                else
                    menu.Selected = max;
            }
            else if (currentKB.IsKeyUp(Keys.S) && previousKB.IsKeyDown(Keys.S))
            {
				#if PLAY_SOUND
                menu.menuMove.Play(GameController.SoundVolume, 0, 0);
				#endif
                int max = menu.currentMenu == "File" ? 2 : 3;
                if (menu.Selected != max)
                    menu.Selected += 1;
                else
                    menu.Selected = 1;
            }
            else if (currentKB.IsKeyUp(Keys.A) && previousKB.IsKeyDown(Keys.A))
            {
                if(menu.currentMenu == "Tools")
                {
                    if(menu.Selected == 1)
                    {
						#if PLAY_SOUND
                        menu.selectItem.Play(GameController.SoundVolume, 0, 0);
						#endif
                        if(GameController.GameSpeedMultiplier == 1)
                            GameController.GameSpeedMultiplier = 2f;                        
                        else
                            GameController.GameSpeedMultiplier = 1f;
                    }
                    else if (menu.Selected == 2 && GameController.pointsToWin != 0)
                    {
						#if PLAY_SOUND
                        menu.selectItem.Play(GameController.SoundVolume, 0, 0);
						#endif
                        GameController.pointsToWin -= 1;
                    }
                    else if (menu.Selected == 3)
                    {
						#if PLAY_SOUND
                        menu.selectItem.Play(GameController.SoundVolume, 0, 0);
						#endif
                        if (GameController.Difficulty == "Normal")
                        {
                            GameController.Difficulty = "Hard";
                        }
                        else
                        {
                            GameController.Difficulty = "Normal";
                        }
                        link.animatedTexture.Load(link._game.Content, "player/linkwithpaddle" + (GameController.Difficulty == "Normal" && !link.hasHitTheRoof ? "" : "small"), Player.Frames, Player.FramesPerSec);
                        link.Texture = new _2DTexture(link._game.Content.Load<Texture2D>("player/linkidle" + (GameController.Difficulty == "Normal" && !link.hasHitTheRoof ? "" : "small")), 2);
                    }
                }
            }
            else if (currentKB.IsKeyUp(Keys.D) && previousKB.IsKeyDown(Keys.D))
            {
                if (menu.currentMenu == "Tools")
                {
                    if (menu.Selected == 1)
                    {
						#if PLAY_SOUND
                        menu.selectItem.Play(GameController.SoundVolume, 0, 0);
						#endif
                        if (GameController.GameSpeedMultiplier == 1)
                            GameController.GameSpeedMultiplier = 1.5f;
                        else
                            GameController.GameSpeedMultiplier = 1f;
                    }
                    else if (menu.Selected == 2)
                    {
						#if PLAY_SOUND
                        menu.selectItem.Play(GameController.SoundVolume, 0, 0);
						#endif
                        GameController.pointsToWin += 1;
                    }
                    else if (menu.Selected == 3)
                    {
						#if PLAY_SOUND
                        menu.selectItem.Play(GameController.SoundVolume, 0, 0);
						#endif
                        if (GameController.Difficulty == "Normal")
                        {
                            GameController.Difficulty = "Hard";                            
                        }
                        else
                        {
                            GameController.Difficulty = "Normal";
                        }
                        link.animatedTexture.Load(link._game.Content, "player/linkwithpaddle" + (GameController.Difficulty == "Normal" && !link.hasHitTheRoof ? "" : "small"), Player.Frames, Player.FramesPerSec);
                        link.Texture = new _2DTexture(link._game.Content.Load<Texture2D>("player/linkidle" + (GameController.Difficulty == "Normal" && !link.hasHitTheRoof ? "" : "small")), 2);
                    }
                }
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

                if (link.ArrowCount > 0)
                {

                    if (keyboardState.IsKeyDown(Keys.Q))
                    {
                        ArrowButtonPressed = true;
                    }
                    if (keyboardState.IsKeyUp(Keys.Q) && ArrowButtonPressed)
                    {
                        link.ArrowCount -= 1;
                        Arrow a = new Arrow(game);
                        a.Launch();
                        ArrowButtonPressed = false;
                    }
                }

                if (link.throwMode)
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