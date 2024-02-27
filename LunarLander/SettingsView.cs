using LunarLander.InputHandling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLander
{
    public class SettingsView : GameStateView
    {
        public Keys up = Keys.W;
        public Keys left = Keys.A;
        public Keys right = Keys.D;
        public GameStateEnum prevState = GameStateEnum.MainMenu;
        private KeySelection keySelected;
        private Texture2D backgroundImage;
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;
        private bool isKeySelected = false;
        private bool m_waitForKeyRelease = false;
        private Rectangle Up = new Rectangle();
        private Rectangle Left = new Rectangle();
        private Rectangle Right = new Rectangle();
        private Texture2D behindSquare;
        private enum KeySelection
        { 
            Left,
            Right,
            Up,
            None,
        }
        private KeySelection m_currentSelection = KeySelection.None;
        private KeySelection m_prevSelection = KeySelection.None;


        public override void loadContent(ContentManager contentManager)
        {
            backgroundImage = contentManager.Load<Texture2D>("53072881464_d0a95851f1_k");
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-selected");
            behindSquare = contentManager.Load<Texture2D>("pixil-frame-0 (6)");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {



            if (!m_waitForKeyRelease)
            {

                if (isKeySelected)
                {
                    Keys[] keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0) 
                    {
                        if (m_currentSelection == KeySelection.Left)
                        {
                            left = keys[0];
                        }
                        else if (m_currentSelection == KeySelection.Up)
                        {
                            up = keys[0];
                        }
                        else if (m_currentSelection == KeySelection.Right)
                        {
                            right = keys[0];
                        }

                        isKeySelected = false;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {

                    if (isKeySelected)
                    {
                        isKeySelected = false;
                        m_waitForKeyRelease = true;

                    }
                    else
                    {
                        return prevState;
                    }
                }
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



                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == KeySelection.Left)
                {
                    isKeySelected = true;
                    m_currentSelection = KeySelection.Left;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == KeySelection.Right)
                {
                    isKeySelected = true;
                    m_currentSelection = KeySelection.Right;

                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && m_currentSelection == KeySelection.Up)
                {
                    isKeySelected = true;
                    m_currentSelection = KeySelection.Up;

                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                m_waitForKeyRelease = false;
            }
            



                if (Up.Contains(Mouse.GetState().Position))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        m_currentSelection = KeySelection.Up;
                        isKeySelected = true;

                    }
                }
                else if (Left.Contains(Mouse.GetState().Position))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        m_currentSelection = KeySelection.Left;
                        isKeySelected = true;

                    }
                }
                else if (Right.Contains(Mouse.GetState().Position))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        m_currentSelection = KeySelection.Right;
                        isKeySelected = true;

                    }
                }




                /*else 
                {
                    m_currentSelection = MenuState.None;
                }*/

                
            
                return GameStateEnum.Settings;
            
        }

        public override void render(GameTime gameTime)
        {
            // Render background image...
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight), Color.Gray);

            // Display the current Keys and their buttons...
            float bottom = drawMenuItem(m_fontMenu, "Settings", 100, Color.White, false);
            bottom = drawMenuItem(m_fontMenu, "Up:    " + up.ToString(), bottom, Color.White, m_currentSelection == KeySelection.Up);

            bottom = drawMenuItem( m_fontMenu, "Left:  " + left.ToString(), bottom, Color.White, m_currentSelection == KeySelection.Left);
            bottom = drawMenuItem(m_fontMenu, "Right: " + right.ToString(), bottom, Color.White, m_currentSelection == KeySelection.Right);
            m_spriteBatch.End();

        }
        


        private float drawMenuItem(SpriteFont font, string text, float y, Color color, bool outline)
        {
            Vector2 stringSize = font.MeasureString(text);
            m_spriteBatch.DrawString(font, text, new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y), color);
            
            if (text == "Up:    " + up.ToString())
            {
                Up = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);
                if (outline && isKeySelected)
                {
                    m_spriteBatch.Draw(behindSquare, Up,Color.White);
                }
            }
            if (text == "Left:  " + left.ToString())
            {
                Left = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);
                if (outline && isKeySelected)
                {
                    m_spriteBatch.Draw(behindSquare, Left, Color.White);

                }
            }
            if (text == "Right: " + right.ToString())
            {
                Right = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);
                if (outline && isKeySelected)
                {
                    m_spriteBatch.Draw(behindSquare, Right, Color.White);


                }
            }
            

            return y + stringSize.Y;
        }

        public override void update(GameTime gameTime)
        {
        }
    }
}
