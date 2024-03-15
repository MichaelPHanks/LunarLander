﻿using Microsoft.Xna.Framework;
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
    public class AboutView : GameStateView
    {
        // Adding a fun comment to the about view...
        private Texture2D backgroundImage;
        private Rectangle backgroundRect;
        private SpriteFont m_font;
        private Texture2D whiteBackground;
        public override void loadContent(ContentManager contentManager)
        {
            backgroundImage = contentManager.Load<Texture2D>("flbb_3udr_220615");
            backgroundRect = new Rectangle(0, 0, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight);
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");
            whiteBackground = contentManager.Load<Texture2D>("whiteImage");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }
            return GameStateEnum.About;
        }

        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(backgroundImage, backgroundRect, Color.White);

            float scale1 = m_graphics.PreferredBackBufferWidth / 1920f;

            Vector2 stringSize2 = m_font.MeasureString("Press ESC To Return") * scale1;

            m_spriteBatch.Draw(whiteBackground, new Rectangle((int)(m_graphics.PreferredBackBufferWidth / 5 - stringSize2.X / 2),
            (int)(m_graphics.PreferredBackBufferHeight / 10f - stringSize2.Y), (int)stringSize2.X, (int)stringSize2.Y), Color.White);

            m_spriteBatch.DrawString(
                           m_font,
                           "Press ESC To Return",
                           new Vector2(m_graphics.PreferredBackBufferWidth / 5 - stringSize2.X / 2,
            m_graphics.PreferredBackBufferHeight / 10f - stringSize2.Y),
                           Color.Black,
                           0,
                           Vector2.Zero,
                           scale1,
                           SpriteEffects.None,
                           0);

            // This one shows the credits!

            float bottom = drawMenuItem(m_font, "Credits", m_graphics.PreferredBackBufferHeight / 1080f * 100f, Color.LightGray);

            bottom = drawMenuItem(m_font, "Programming - Miketa", bottom + stringSize2.Y, Color.LightGray);
            bottom = drawMenuItem(m_font, "Sound Effects - Miketa (Also the internet)", bottom + stringSize2.Y, Color.LightGray);
            bottom = drawMenuItem(m_font, "Images - The Internet", bottom + stringSize2.Y, Color.LightGray);
            bottom = drawMenuItem(m_font, "Rocket Ship - Miketa (Internet Inspired)", bottom + stringSize2.Y, Color.LightGray);
            bottom = drawMenuItem(m_font, "I made this game because I had to", bottom + stringSize2.Y, Color.LightGray);
            bottom = drawMenuItem(m_font, "It was really fun making it tho tbh", bottom + stringSize2.Y, Color.LightGray);





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
        }
    }
}
