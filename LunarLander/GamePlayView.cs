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
    public class GamePlayView : GameStateView
    {
        private SpriteFont m_font;
        private const string MESSAGE = "I wrote this amazing game";
        private const string MESSAGE2 = "C to continue, ESC to go to main menu";
        private const float RECTANGLE2_ROTATION_RATE = MathHelper.Pi / 4;  // radians per second
        private Texture2D backgroundImage;
        LunarLanderLevel m_level = new LunarLanderLevel(1);
        private double positionX = 15;
        private double positionY = 50;
        private Texture2D playerTexture;
        private Rectangle playerRectangle;
        float playerX;
        float playerY;
        double playerFuel = 20d;
        private bool isESCDown = true;

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
            backgroundImage = contentManager.Load<Texture2D>("53072881464_d0a95851f1_k");
            playerRectangle = new Rectangle(50, 50, playerTexture.Width, playerTexture.Height);
            playerX = 50f;
            playerY = 50f;



    }

    public override GameStateEnum processInput(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !isESCDown)
            {
                isPaused = true;
                isESCDown = true;

                return GameStateEnum.Paused;
                
                
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                isESCDown = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) 
            {
                m_level.playerAngle += (RECTANGLE2_ROTATION_RATE * gameTime.ElapsedGameTime.TotalMilliseconds / 250.0f);

                /*if (m_level.playerAngle + 90< 0)
                {
                    m_level.playerAngle += 360;
                }*/
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                m_level.playerAngle -= (RECTANGLE2_ROTATION_RATE * gameTime.ElapsedGameTime.TotalMilliseconds / 250.0f);

                /*if (m_level.playerAngle + 90> 360)
                {
                    m_level.playerAngle -= 360;
                }*/
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {


                if (playerFuel > 0)
                {
                    m_level.thrustVector.X += (float)Math.Cos(m_level.playerAngle - Math.PI / 2);
                    m_level.thrustVector.Y -= (float)Math.Sin(m_level.playerAngle - Math.PI / 2);
                    playerFuel -= 0.002 * gameTime.ElapsedGameTime.TotalMilliseconds;
                }
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

            // Render the background:
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight), Color.White);

            if (currentLevel == Level.LEVELONE)
            {
                
            }
            else if (currentLevel == Level.LEVELTWO) 
            { 
                
            }
            if (!isPaused)
            {
                
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
            }
            else 
            {
                Vector2 stringSize = m_font.MeasureString(MESSAGE2);
                m_spriteBatch.DrawString(m_font, MESSAGE2,
                    new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2,
                m_graphics.PreferredBackBufferHeight / 2 - stringSize.Y), Color.White);
            }


            // Render the fuel, speed, and angle.

            Vector2 stringSize1 = m_font.MeasureString("Fuel   : " + string.Format("{0:0.00}", playerFuel) + " s");
            m_spriteBatch.DrawString(m_font, "Fuel   : " + string.Format("{0:0.00}", playerFuel) + " s",
                new Vector2(m_graphics.PreferredBackBufferWidth * 0.75f - stringSize1.X / 2,
            m_graphics.PreferredBackBufferHeight / 4f - stringSize1.Y), Color.White);


             stringSize1 = m_font.MeasureString("Speed  : " + string.Format("{0:0.00}", Math.Abs(m_level.playerVectorVelocity.Y)) + " m/s");
            m_spriteBatch.DrawString(m_font, "Speed  : " + string.Format("{0:0.00}", Math.Abs(m_level.playerVectorVelocity.Y)) + " m/s",
                new Vector2(m_graphics.PreferredBackBufferWidth * 0.75f - stringSize1.X / 2,
            m_graphics.PreferredBackBufferHeight / 4f - stringSize1.Y + stringSize1.Y), Color.White);


             stringSize1 = m_font.MeasureString("Angle  : " + m_level.playerAngle + "");
            m_spriteBatch.DrawString(m_font, "Angle  : " + m_level.playerAngle + "",
                new Vector2(m_graphics.PreferredBackBufferWidth * 0.75f - stringSize1.X / 2,
            m_graphics.PreferredBackBufferHeight / 4f - stringSize1.Y + 2*stringSize1.Y), Color.White);

            m_spriteBatch.End();

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
