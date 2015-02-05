using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkTheBoomerangMaster.Classes
{
    public class GameMenu
    {
        Texture2D menuBackground;

        _2DTexture menuTopLeftCornerFrame;
        _2DTexture menuTopRightCornerFrame;
        _2DTexture menuBotLeftCornerFrame;
        _2DTexture menuBotRightCornerFrame;

        _2DTexture menuLeftConnectorFrame;
        _2DTexture menuRightConnectorFrame;
        _2DTexture menuTopConnectorFrame;
        _2DTexture menuBotConnectorFrame;
        GameController _game;

        public SoundEffect selectItem;
        public SoundEffect menuMove;
        public SoundEffect menuOpen;
        public SoundEffect menuClose;

        int startingX = 100, startingY = 150;
        Vector2 startingTextPosition;
        Vector2 startingInstructionsTextPosition;

        public int Selected = 1;

        public string currentMenu = "Start";

        private SpriteFont font;
        private SpriteFont fontSmaller;

        public GameMenu(GameController game)
        {
            _game = game;

            selectItem = game.Content.Load<SoundEffect>("sounds/selectOption");
            menuMove = game.Content.Load<SoundEffect>("sounds/moveCursor");
            menuOpen = game.Content.Load<SoundEffect>("sounds/menuOpen");
            menuClose = game.Content.Load<SoundEffect>("sounds/menuClose");

            menuBackground = _game.Content.Load<Texture2D>("scoreboard/blackdot");

            menuTopLeftCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameLeftTopCorner"), GameController.scale);
            menuTopRightCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameRightTopCorner"), GameController.scale);
            menuBotLeftCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameLeftBotCorner"), GameController.scale);
            menuBotRightCornerFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameRightBotCorner"), GameController.scale);

            menuLeftConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameLeftConnector"), GameController.scale);
            menuRightConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameRightConnector"), GameController.scale);
            menuTopConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameTopConnector"), GameController.scale);
            menuBotConnectorFrame = new _2DTexture(_game.Content.Load<Texture2D>("scoreboard/frameBotConnector"), GameController.scale);

            font = _game.Content.Load<SpriteFont>("fonts/font");
            fontSmaller = _game.Content.Load<SpriteFont>("fonts/font12");

            startingTextPosition = new Vector2(startingX + 115, startingY + 75);
            startingInstructionsTextPosition = new Vector2(startingX + 25, startingY + 350);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawFrame(spriteBatch);
            DrawInstructions(spriteBatch);
            if (currentMenu == "Start")
                DrawStartMenu(spriteBatch);
            else if (currentMenu == "File")
                DrawFileMenu(spriteBatch);
            else if (currentMenu == "Tools")
                DrawToolsMenu(spriteBatch);
            else if (currentMenu == "Help")
                DrawHelpMenu(spriteBatch);
            else if (currentMenu == "About")
                DrawAboutMenu(spriteBatch);
            else if (currentMenu == "Info")
                DrawInfoMenu(spriteBatch);
            else if (currentMenu == "Controls")
                DrawControlsMenu(spriteBatch);
        }


        private void DrawFrame(SpriteBatch spriteBatch)
        {
            DrawFrameCorners(spriteBatch);
            DrawFrameEdges(spriteBatch);
        }

        private void DrawFrameCorners(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //corners
            spriteBatch.Draw(menuBackground, new Rectangle(100, 150, GameController.ScreenWidth - 200, GameController.ScreenHeight - 250), Color.White);

            spriteBatch.Draw(menuTopLeftCornerFrame.texture, new Rectangle(100, 150, menuTopLeftCornerFrame.GetWidth(), menuTopLeftCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(menuTopRightCornerFrame.texture, new Rectangle(GameController.ScreenWidth - 100 - menuTopRightCornerFrame.GetWidth(), 150, menuTopRightCornerFrame.GetWidth(), menuTopRightCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(menuBotLeftCornerFrame.texture, new Rectangle(100, GameController.ScreenHeight - 100 - menuBotLeftCornerFrame.GetHeight(), menuBotLeftCornerFrame.GetWidth(), menuBotLeftCornerFrame.GetHeight()), Color.White);

            spriteBatch.Draw(menuBotRightCornerFrame.texture, new Rectangle(GameController.ScreenWidth - 100 - menuTopRightCornerFrame.GetWidth(),
                GameController.ScreenHeight - 100 - menuBotLeftCornerFrame.GetHeight(), menuBotLeftCornerFrame.GetWidth(), menuBotLeftCornerFrame.GetHeight()), Color.White);

            spriteBatch.End();
        }

        private void DrawFrameEdges(SpriteBatch spriteBatch)
        {
            //frame edges
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(menuLeftConnectorFrame.texture, new Vector2(100, 150 + menuBotRightCornerFrame.GetHeight()),
                new Rectangle(0,0, menuLeftConnectorFrame.GetWidth(), GameController.ScreenHeight - 250 -(menuBotRightCornerFrame.GetHeight() * 2)),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(menuRightConnectorFrame.texture, new Vector2(GameController.ScreenWidth - 100 - menuBotRightCornerFrame.GetWidth(), 150 + menuBotRightCornerFrame.GetHeight()),
                new Rectangle(0, 0, menuLeftConnectorFrame.GetWidth(), GameController.ScreenHeight - 250 - (menuBotRightCornerFrame.GetHeight() * 2)),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(menuBotConnectorFrame.texture, new Vector2(100 + menuTopLeftCornerFrame.GetWidth(), 150),
                new Rectangle(0, 0, GameController.ScreenWidth - 200 - menuTopRightCornerFrame.GetWidth() * 2, menuTopConnectorFrame.GetHeight()),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);

            spriteBatch.Draw(menuBotConnectorFrame.texture, new Vector2(100 + menuTopLeftCornerFrame.GetWidth(), GameController.ScreenHeight - 100 - menuTopLeftCornerFrame.GetHeight()),
                new Rectangle(0, 0, GameController.ScreenWidth - 200 - menuTopRightCornerFrame.GetWidth() * 2, menuTopConnectorFrame.GetHeight()),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();
        }

        private void DrawStartMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "File", startingTextPosition, Selected == 1 ? Color.DarkGreen : Color.White);

            spriteBatch.DrawString(font, "Tools", new Vector2(startingTextPosition.X, startingTextPosition.Y +75), Selected == 2 ? Color.DarkGreen : Color.White);

            spriteBatch.DrawString(font, "Help", new Vector2(startingTextPosition.X, startingTextPosition.Y + 150), Selected == 3 ? Color.DarkGreen : Color.White);

            spriteBatch.End();
        }

        private void DrawFileMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "New Game", startingTextPosition, Selected == 1 ? Color.DarkGreen : Color.White);

            spriteBatch.DrawString(font, "Exit", new Vector2(startingTextPosition.X, startingTextPosition.Y + 75), Selected == 2 ? Color.DarkGreen : Color.White);

            spriteBatch.End();
        }

        private void DrawToolsMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Speed: " + (GameController.GameSpeedMultiplier == 1 ? "Normal" : "Fast"), new Vector2(startingTextPosition.X - 65, startingTextPosition.Y), Selected == 1 ? Color.DarkGreen : Color.White);

            spriteBatch.DrawString(font, "Points To Win: " +  GameController.pointsToWin, new Vector2(startingTextPosition.X - 65, startingTextPosition.Y + 75), Selected == 2 ? Color.DarkGreen : Color.White);

            spriteBatch.DrawString(font, "Difficulty: " + GameController.Difficulty, new Vector2(startingTextPosition.X - 65, startingTextPosition.Y + 150), Selected == 3 ? Color.DarkGreen : Color.White);

            spriteBatch.End();
        }

        private void DrawHelpMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "About", startingTextPosition, Selected == 1 ? Color.DarkGreen : Color.White);

            spriteBatch.DrawString(font, "Info", new Vector2(startingTextPosition.X, startingTextPosition.Y + 75), Selected == 2 ? Color.DarkGreen : Color.White);

            spriteBatch.DrawString(font, "Controls", new Vector2(startingTextPosition.X, startingTextPosition.Y + 150), Selected == 3 ? Color.DarkGreen : Color.White);

            spriteBatch.End();
        }

        private void DrawAboutMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(fontSmaller, "Created By :", new Vector2(startingTextPosition.X - 65, startingTextPosition.Y - 45), Color.White);

            spriteBatch.DrawString(fontSmaller, "Kyle and Adrian", new Vector2(startingTextPosition.X - 65, startingTextPosition.Y - 10), Color.White);

            spriteBatch.DrawString(fontSmaller, "Course : Game Dev", new Vector2(startingTextPosition.X - 65, startingTextPosition.Y + 35), Color.White);

            spriteBatch.DrawString(fontSmaller, "Assignment Title :", new Vector2(startingTextPosition.X - 65, startingTextPosition.Y + 80), Color.White);

            spriteBatch.DrawString(fontSmaller, "SET Breakout", new Vector2(startingTextPosition.X - 65, startingTextPosition.Y + 115), Color.White);

            spriteBatch.End();
        }

        private void DrawInfoMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(fontSmaller, "Info :", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y - 45), Color.White);

            spriteBatch.DrawString(fontSmaller, "The objective of the game", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y - 10), Color.White);

            spriteBatch.DrawString(fontSmaller, "is to clear all the enemies", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y + 25), Color.White);

            spriteBatch.DrawString(fontSmaller, "in each level without", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y + 60), Color.White);

            spriteBatch.DrawString(fontSmaller, "losing all your lives.", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y + 95), Color.White);

            spriteBatch.DrawString(fontSmaller, "Good Luck!!!", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y + 130), Color.White);

            spriteBatch.End();
        }

        private void DrawControlsMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(fontSmaller, "Controls :", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y - 45), Color.White);

            spriteBatch.DrawString(fontSmaller, "A : Move Left", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y - 10), Color.White);

            spriteBatch.DrawString(fontSmaller, "D : Move Right", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y + 25), Color.White);

            spriteBatch.DrawString(fontSmaller, "Q : Shoot Arrows", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y + 60), Color.White);

            spriteBatch.DrawString(fontSmaller, "E : Throw Bombs", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y + 95), Color.White);

            spriteBatch.DrawString(fontSmaller, "M : Mute Music", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y + 130), Color.White);

            spriteBatch.DrawString(fontSmaller, "N : Mute Sound Effects", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y + 165), Color.White);

            spriteBatch.DrawString(fontSmaller, "Esc : Pause/Resume", new Vector2(startingTextPosition.X - 85, startingTextPosition.Y + 200), Color.White);

            spriteBatch.End();
        }

        private void DrawInstructions(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(fontSmaller, "WASD: Navigate", startingInstructionsTextPosition, Color.White);

            spriteBatch.DrawString(fontSmaller, "Enter: Select - Esc: Back", new Vector2(startingInstructionsTextPosition.X, startingInstructionsTextPosition.Y + 35), Color.White);

            spriteBatch.End();
        }

    }
}
