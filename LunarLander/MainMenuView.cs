using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LunarLander
{
    public class MainMenuView : GameStateView
    {
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;
        private Rectangle gameplay = new Rectangle();
        private Rectangle about = new Rectangle();
        private Rectangle quit = new Rectangle();
        private Rectangle help = new Rectangle();
        private Rectangle highScores = new Rectangle();
        SoundEffect hover;
        bool hoverPlaying = false;
        TimeSpan hoverTimer;
        TimeSpan hoverZero;
        SoundEffectInstance soundInstance;

        private enum MenuState
        {
            NewGame,
            HighScores,
            Help,
            About,
            Quit,
            None,
        }

        private MenuState m_currentSelection = MenuState.NewGame;
        private MenuState m_prevSelection = MenuState.NewGame;
        private bool m_waitForKeyRelease = false;
        public override void loadContent(ContentManager contentManager)
        {
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-selected");
            hover = contentManager.Load<SoundEffect>("hoverSoundEffect");

            soundInstance = hover.CreateInstance();


        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (!m_waitForKeyRelease)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                { 
                    m_currentSelection++;
                    m_waitForKeyRelease = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    m_currentSelection--;
                    m_waitForKeyRelease = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == MenuState.NewGame)
                {
                    return GameStateEnum.GamePlay;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == MenuState.HighScores)
                {
                    return GameStateEnum.HighScores;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == MenuState.Help)
                {
                    return GameStateEnum.Help;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == MenuState.About)
                {
                    return GameStateEnum.About;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == MenuState.Quit)
                {
                    return GameStateEnum.Exit;
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                m_waitForKeyRelease = false;
            }



            if (gameplay.Contains(Mouse.GetState().Position))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed) 
                {
                    m_currentSelection = MenuState.None;
                    return GameStateEnum.GamePlay;
                }
                m_currentSelection = MenuState.NewGame;
                
                

            }
            else if (help.Contains(Mouse.GetState().Position))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    m_currentSelection = MenuState.None;

                    return GameStateEnum.Help;
                }
                m_currentSelection = MenuState.Help;
                
            }
            else if (about.Contains(Mouse.GetState().Position))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    m_currentSelection = MenuState.None;

                    return GameStateEnum.About;
                }
                m_currentSelection = MenuState.About;
                
            }
            else if (highScores.Contains(Mouse.GetState().Position))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    m_currentSelection = MenuState.None;

                    return GameStateEnum.HighScores;
                }
                m_currentSelection = MenuState.HighScores;
                
            }
            else if (quit.Contains(Mouse.GetState().Position))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    m_currentSelection = MenuState.None;

                    return GameStateEnum.Exit;
                }
                m_currentSelection = MenuState.Quit;
                
            }
            /*else 
            {
                m_currentSelection = MenuState.None;
            }*/

            if (m_prevSelection != m_currentSelection && m_currentSelection != MenuState.None)
            {
                if (soundInstance.State == SoundState.Playing)
                {
                    soundInstance.Stop();

                }
                soundInstance.Play();

            }
            m_prevSelection = m_currentSelection;

            return GameStateEnum.MainMenu;
        }

        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin(); 

            float bottom = drawMenuItem(m_currentSelection == MenuState.NewGame ? m_fontMenuSelect: m_fontMenu, "New Game", 100, m_currentSelection == MenuState.NewGame ? Color.White:Color.Blue);
            
            bottom = drawMenuItem(m_currentSelection == MenuState.HighScores ? m_fontMenuSelect : m_fontMenu, "High Scores", bottom, m_currentSelection == MenuState.HighScores ? Color.White : Color.Blue);
            bottom = drawMenuItem(m_currentSelection == MenuState.Help ? m_fontMenuSelect : m_fontMenu, "Help", bottom, m_currentSelection == MenuState.Help ? Color.White : Color.Blue);
            bottom = drawMenuItem(m_currentSelection == MenuState.About ? m_fontMenuSelect : m_fontMenu, "About", bottom, m_currentSelection == MenuState.About ? Color.White : Color.Blue);
            drawMenuItem(m_currentSelection == MenuState.Quit ? m_fontMenuSelect : m_fontMenu, "Quit", bottom, m_currentSelection == MenuState.Quit ? Color.White : Color.Blue);
            m_spriteBatch.End();

        }
        private float drawMenuItem(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);
            m_spriteBatch.DrawString(font, text, new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y), color );

            if (text == "New Game")
            {
                gameplay = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);
            }
            if (text == "High Scores")
            {
                highScores = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);

            }
            if (text == "Help")
            {
                help = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);

            }
            if (text == "About")
            {
                about = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);

            }
            if (text == "Quit")
            {
                quit = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);

            }
            return y + stringSize.Y;
        }
        

        public override void update(GameTime gameTime)
        {
            
        }
    }
}
