using LunarLander.InputHandling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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
        private bool saving = false;
        private bool loading = false;
        private KeyControls m_loadedState = null;
        private Texture2D whiteBackground;

        private enum KeySelection
        {
            Up,
            Left,
            Right,
            None,
        }
        private KeySelection m_currentSelection = KeySelection.None;
        private KeySelection m_prevSelection = KeySelection.None;


        public override void loadContent(ContentManager contentManager)
        {
            backgroundImage = contentManager.Load<Texture2D>("saturnCool");
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-selected");
            behindSquare = contentManager.Load<Texture2D>("pixil-frame-0 (6)");
            whiteBackground = contentManager.Load<Texture2D>("whiteImage");

            loadKeyControls();

            up = m_loadedState.Up;
            left = m_loadedState.Left;
            right = m_loadedState.Right;
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {



            if (!m_waitForKeyRelease)
            {
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

                if (isKeySelected)
                {
                    Keys[] keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0) 
                    {
                        if (keys[0] != Keys.Escape)
                        {
                            if (m_currentSelection == KeySelection.Left)
                            {
                                left = keys[0];
                                saveKeyControls();
                            }
                            else if (m_currentSelection == KeySelection.Up)
                            {
                                up = keys[0];
                                saveKeyControls();

                            }
                            else if (m_currentSelection == KeySelection.Right)
                            {
                                right = keys[0];
                                saveKeyControls();

                            }
                        }

                        isKeySelected = false;
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

            float scale1 = m_graphics.PreferredBackBufferWidth / 1920f;

            Vector2 stringSize2 = m_fontMenu.MeasureString("Press ESC To Return") * scale1;

            m_spriteBatch.Draw(whiteBackground, new Rectangle((int)(m_graphics.PreferredBackBufferWidth / 5 - stringSize2.X / 2),
            (int)(m_graphics.PreferredBackBufferHeight / 10f - stringSize2.Y), (int)stringSize2.X, (int)stringSize2.Y), Color.White);

            m_spriteBatch.DrawString(
                           m_fontMenu,
                           "Press ESC To Return",
                           new Vector2(m_graphics.PreferredBackBufferWidth / 5 - stringSize2.X / 2,
            m_graphics.PreferredBackBufferHeight / 10f - stringSize2.Y),
                           Color.Black,
                           0,
                           Vector2.Zero,
                           scale1,
                           SpriteEffects.None,
                           0);
            // Display the current Keys and their buttons...
            float bottom = drawMenuItem(m_fontMenu, "Settings", m_graphics.PreferredBackBufferHeight / 1080f * 100f, Color.White, false);
            bottom = drawMenuItem(m_fontMenu, "Thrust     : " + up.ToString(), bottom, Color.White, m_currentSelection == KeySelection.Up);

            bottom = drawMenuItem( m_fontMenu, "Rotate Left: " + left.ToString(), bottom, Color.White, m_currentSelection == KeySelection.Left);
            bottom = drawMenuItem(m_fontMenu, "Rotate Right: " + right.ToString(), bottom, Color.White, m_currentSelection == KeySelection.Right);
            m_spriteBatch.End();

        }
        


        private float drawMenuItem(SpriteFont font, string text, float y, Color color, bool outline)
        {

            float scale = m_graphics.PreferredBackBufferWidth / 1980f;
            Vector2 stringSize = font.MeasureString(text) * scale;
            m_spriteBatch.DrawString(
                           font,
                           text,
                           new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                           color,
                           0,
                           Vector2.Zero,
                           scale,
                           SpriteEffects.None,
                           0);

            if (text == "Thrust     : " + up.ToString())
            {
                Up = new Rectangle(((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2) - 10, (int)y, (int)stringSize.X + 20, (int)stringSize.Y);
                if (outline && isKeySelected)
                    if (outline && isKeySelected)
                {
                    m_spriteBatch.Draw(behindSquare, Up,Color.White);
                }
            }
            if (text == "Rotate Left: " + left.ToString())
            {
                Left = new Rectangle(((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2) - 10, (int)y, (int)stringSize.X + 20, (int)stringSize.Y);
                if (outline && isKeySelected)
                    if (outline && isKeySelected)
                {
                    m_spriteBatch.Draw(behindSquare, Left, Color.White);

                }
            }
            if (text == "Rotate Right: " + right.ToString())
            {
                Right = new Rectangle(((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2) - 10, (int)y , (int)stringSize.X + 20, (int)stringSize.Y);
                if (outline && isKeySelected)
                {
                    m_spriteBatch.Draw(behindSquare, Right, Color.White);


                }
            }
            

            return y + stringSize.Y;
        }



        /// <summary>
        /// Demonstrates how serialize an object to storage
        /// </summary>
        private void saveKeyControls()
        {
            lock (this)
            {
                if (!this.saving)
                {
                    this.saving = true;

                    // Create something to save
                    KeyControls myState = new KeyControls(this.left, this.right, this.up);

                    // Yes, I know the result is not being saved, I dont' need it
                    finalizeSaveAsync(myState);
                }
            }
        }

        private async Task finalizeSaveAsync(KeyControls state)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("KeyControls.json", FileMode.Create))
                        {
                            if (fs != null)
                            {
                                DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(KeyControls));
                                mySerializer.WriteObject(fs, state);
                            }
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                        // Ideally show something to the user, but this is demo code :)
                    }
                }

                this.saving = false;
            });
        }

        /// <summary>
        /// Demonstrates how to deserialize an object from storage device
        /// </summary>
        private void loadKeyControls()
        {
            lock (this)
            {
                if (!this.loading)
                {
                    this.loading = true;
                    // Yes, I know the result is not being saved, I dont' need it
                    var result = finalizeLoadAsync();
                    result.Wait();

                }
            }
        }

        private async Task finalizeLoadAsync()
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        if (storage.FileExists("KeyControls.json"))
                        {
                            using (IsolatedStorageFileStream fs = storage.OpenFile("KeyControls.json", FileMode.Open))
                            {
                                if (fs != null)
                                {
                                    DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(KeyControls));
                                    m_loadedState = (KeyControls)mySerializer.ReadObject(fs);
                                }

                                
                            }
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                        // Ideally show something to the user, but this is demo code :)
                    }
                }

                this.loading = false;
            });
        }

        public override void update(GameTime gameTime)
        {
        }
    }
}
