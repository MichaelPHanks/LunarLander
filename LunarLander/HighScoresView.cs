using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace LunarLander
{
    public class HighScoresView : GameStateView
    {

        private bool saving = false;
        private bool loading = false;
        private HighScoresState m_loadedState = null;
        private SpriteFont m_fontMenu;

        List<HighScoresState> m_highScores = new List<HighScoresState>();
        public override void loadContent(ContentManager contentManager)
        {
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");

            loadHighScores();
        }

        private void loadHighScores()
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
                            using (IsolatedStorageFileStream fs = storage.OpenFile("HighScores.json", FileMode.Open))
                            {
                                if (fs != null)
                                {
                                    DataContractJsonSerializer mySerializer = new DataContractJsonSerializer(typeof(HighScoresState));
                                    m_loadedState = (HighScoresState)mySerializer.ReadObject(fs);
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

        public override GameStateEnum processInput(GameTime gameTime)
        {
            return GameStateEnum.HighScores;
        }

        public override void render(GameTime gameTime)
        {
            // Draw something bro
            m_spriteBatch.Begin();
            //m_spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight), Color.Gray);
            float bottom = drawMenuItem(m_fontMenu, "High Scores!", 100, Color.OrangeRed);

            foreach (var state in m_loadedState.getHighScores())
            {
                bottom = drawMenuItem(m_fontMenu, state.Item1.ToString() + " --- " + state.Item2.ToString(), bottom, Color.White);
            }
            
            m_spriteBatch.End();


        }

        private float drawMenuItem(SpriteFont font, string text, float y, Color color)
        {

            float scale = m_graphics.PreferredBackBufferWidth / 1920f;
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

            return y + stringSize.Y;
        }

        public override void update(GameTime gameTime)
        {
            // Likely going to be nothing in here...
        }
    }
}
