using Microsoft.Xna.Framework;
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
    public class GamePlayView : GameStateView
    {
        private SpriteFont m_font;
        private const string MESSAGE = "I wrote this amazing game";
        private const string MESSAGE2 = "Paused... C to continue, ESC to go to main menu";

        LunarLanderLevel m_level = new LunarLanderLevel(1);
        private double positionX = 15;
        private double positionY = 50;

        public enum Level
        {
            LEVELONE,
            LEVELTWO,

            
        }

        public enum Stage
        {
            PLAYING,
            COMPLETED,
        }

        private Level currentLevel = Level.LEVELONE;
        private Stage currentStage = Stage.PLAYING;

        bool isPaused = false;
        public override void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (isPaused)
                {
                    return GameStateEnum.MainMenu;
                }
                isPaused = true;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    Console.Write("Yeah");
                }
                m_level.thrustVector.Y += 0.1f;
                //positionY -= 1 * gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else 
            {
                m_level.thrustVector.Y = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                //positionY += 1 * gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (isPaused)
            {
                
                if (Keyboard.GetState().IsKeyDown(Keys.C))
                { 
                    isPaused = false;
                }

            }




            return GameStateEnum.GamePlay;
        }

        public override void render(GameTime gameTime)
        {
            if (currentLevel == Level.LEVELONE)
            {
                
            }
            else if (currentLevel == Level.LEVELTWO) 
            { 
                
            }
            if (!isPaused)
            {
                m_spriteBatch.Begin();
                Vector2 stringSize = m_font.MeasureString(MESSAGE);
                m_spriteBatch.DrawString(m_font, MESSAGE,
                    new Vector2((int)positionX,
                    (int)positionY), Color.White);
                m_spriteBatch.End();
            }
            else 
            {
                m_spriteBatch.Begin();
                Vector2 stringSize = m_font.MeasureString(MESSAGE2);
                m_spriteBatch.DrawString(m_font, MESSAGE2,
                    new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X - 2,
                m_graphics.PreferredBackBufferHeight / 2 - stringSize.Y), Color.White);
                m_spriteBatch.End();
            }

        }

        public override void update(GameTime gameTime)
        {

            if (currentLevel == Level.LEVELONE && currentStage == Stage.COMPLETED)
            {
                currentLevel = Level.LEVELTWO;
                currentStage = Stage.PLAYING;
            }
            
            m_level.playerVectorVelocity += m_level.gravityVector;
            m_level.playerVectorVelocity += m_level.thrustVector;
            positionX -= m_level.playerVectorVelocity.X * gameTime.ElapsedGameTime.TotalMinutes;
            positionY -= m_level.playerVectorVelocity.Y * gameTime.ElapsedGameTime.TotalMinutes;

        }
    }
}
