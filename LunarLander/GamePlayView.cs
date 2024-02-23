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
        private const string MESSAGE2 = "C to continue, ESC to go to main menu";
        private const float RECTANGLE2_ROTATION_RATE = MathHelper.Pi / 4;  // radians per second

        LunarLanderLevel m_level = new LunarLanderLevel(1);
        private double positionX = 15;
        private double positionY = 50;
        private Texture2D playerTexture;
        private Rectangle playerRectangle;
        float playerX;
        float playerY;

        private bool isESCDown = false;

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
            playerTexture = contentManager.Load<Texture2D>("rocketShip");
            playerRectangle = new Rectangle(50, 50, playerTexture.Width, playerTexture.Height);
            playerX = 50f;
            playerY = 50f;



    }

    public override GameStateEnum processInput(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !isESCDown)
            {
                if (isPaused)
                {
                    return GameStateEnum.MainMenu;
                }
                isPaused = true;
                isESCDown = true;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                isESCDown = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) 
            {
                m_level.playerAngle += (RECTANGLE2_ROTATION_RATE * gameTime.ElapsedGameTime.TotalMilliseconds / 100.0f);

                /*if (m_level.playerAngle + 90< 0)
                {
                    m_level.playerAngle += 360;
                }*/
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                m_level.playerAngle -= (RECTANGLE2_ROTATION_RATE * gameTime.ElapsedGameTime.TotalMilliseconds / 100.0f);

                /*if (m_level.playerAngle + 90> 360)
                {
                    m_level.playerAngle -= 360;
                }*/
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    Console.Write("Yeah");
                }


                m_level.thrustVector.X += (float)Math.Cos(m_level.playerAngle - Math.PI / 2);
                m_level.thrustVector.Y -= (float)Math.Sin(m_level.playerAngle - Math.PI / 2);
                //positionY -= 1 * gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else 
            {
                m_level.thrustVector.Y = 0;
                m_level.thrustVector.X = 0;
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
                m_spriteBatch.Draw(
                    playerTexture,
                    new Rectangle((int)playerX,(int) playerY, playerRectangle.Width, playerRectangle.Height),
                    null, // Drawing the whole texture, not a part
                    Color.White,
                    (float)m_level.playerAngle,
                    new Vector2(playerRectangle.Width / 2, playerRectangle.Height / 2),
                    SpriteEffects.None,
                    0);
               /* Vector2 stringSize = m_font.MeasureString(MESSAGE);
                m_spriteBatch.DrawString(m_font, MESSAGE,
                    new Vector2((int)positionX,
                    (int)positionY), Color.White);*/
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
            
            m_level.playerVectorVelocity += m_level.gravityVector * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.5f;
            m_level.playerVectorVelocity += m_level.thrustVector * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.5f;
            playerX += (float)(m_level.playerVectorVelocity.X * 0.1);
            playerY -= (float)(m_level.playerVectorVelocity.Y * 0.1);

        }
    }
}
