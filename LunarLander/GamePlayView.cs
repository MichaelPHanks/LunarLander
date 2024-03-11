using LunarLander.InputHandling;
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
        private Texture2D backgroundImage;
        LunarLanderLevel m_level;
        private double positionX = 15;
        private double positionY = 50;
        private Texture2D playerTexture;
        private Rectangle playerRectangle;
        float playerX;
        float playerY;
        double playerFuel = 20d;
        private bool isESCDown = true;
        private KeyboardInput keyboardInput;
        private bool isUpPressed = false;
        private Keys up;
        private Keys left;
        private Keys right;
        private const double PLAYERWIDTH = 1920;
        private const double PLAYERHEIGHT = 1080;
        private BasicEffect m_effect;
        Texture2D t; //base for the line texture

        private Circle playerCircle;
        



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
            // create 1x1 texture for line drawing
            t = new Texture2D(m_graphics.GraphicsDevice, 1, 1);
            t.SetData<Color>(
                new Color[] { Color.White });
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");
            playerTexture = contentManager.Load<Texture2D>("rocketShip");
            backgroundImage = contentManager.Load<Texture2D>("53072881464_d0a95851f1_k");
            playerRectangle = new Rectangle(50, 50, playerTexture.Width, playerTexture.Height);
            playerX = 50f;
            playerY = 50f;
            keyboardInput = new KeyboardInput();
            keyboardInput.registerCommand(Keys.W, false, new IInputDevice.CommandDelegate(onMoveUp));
            keyboardInput.registerCommand(Keys.A, false, new IInputDevice.CommandDelegate(onMoveLeft));
            keyboardInput.registerCommand(Keys.D, false, new IInputDevice.CommandDelegate(onMoveRight));
            up = Keys.W;
            left = Keys.A;
            right = Keys.D;
            m_level =  new LunarLanderLevel(1, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight);

            m_graphics.GraphicsDevice.RasterizerState = new RasterizerState
            {
                FillMode = FillMode.Solid,
                CullMode = CullMode.CullCounterClockwiseFace,   // CullMode.None If you want to not worry about triangle winding order
                MultiSampleAntiAlias = true,
            };
            m_effect = new BasicEffect(m_graphics.GraphicsDevice)
            {
                VertexColorEnabled = true,
                View = Matrix.CreateLookAt(new Vector3(0, 0, 1), Vector3.Zero, Vector3.Up),

                Projection = Matrix.CreateOrthographicOffCenter(
                    0, m_graphics.GraphicsDevice.Viewport.Width,
                    m_graphics.GraphicsDevice.Viewport.Height, 0,   // doing this to get it to match the default of upper left of (0, 0)
                    0.1f, 2)
            };

            playerCircle = new Circle(new Tuple<double,double>(75, 75), Math.Max(playerTexture.Width / 2, playerTexture.Height / 2));

        }

        private void onMoveRight(GameTime gameTime)
        {
            m_level.playerAngle += (RECTANGLE2_ROTATION_RATE * gameTime.ElapsedGameTime.TotalMilliseconds / 250.0f);
            if (m_level.playerAngle > 2 * Math.PI)
            {
                m_level.playerAngle -= 2 * Math.PI;
            }
            /*if (m_level.playerAngle + 90< 0)
            {
                m_level.playerAngle += 360;
            }*/
        }

        private void onMoveLeft(GameTime gameTime)
        {
            m_level.playerAngle -= (RECTANGLE2_ROTATION_RATE * gameTime.ElapsedGameTime.TotalMilliseconds / 250.0f);
            if (m_level.playerAngle < 0)
            {
                m_level.playerAngle += 2 * Math.PI;
            }

            /*if (m_level.playerAngle + 90> 360)
            {
                m_level.playerAngle -= 360;
            }*/
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            keyboardInput.Update(gameTime);

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
            
            

            
            if (!isUpPressed) 
            {
                m_level.thrustVector.Y = 0;
                m_level.thrustVector.X = 0;
            }
            isUpPressed = false;
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
            /*if (!isPaused)
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
               *//* Vector2 stringSize = m_font.MeasureString(MESSAGE);
                m_spriteBatch.DrawString(m_font, MESSAGE,
                    new Vector2((int)positionX,
                    (int)positionY), Color.White);*//*
            }
            else 
            {
                Vector2 stringSize = m_font.MeasureString(MESSAGE2);
                m_spriteBatch.DrawString(m_font, MESSAGE2,
                    new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2,
                m_graphics.PreferredBackBufferHeight / 2 - stringSize.Y), Color.White);
            }*/


            // Render the fuel, speed, and angle.


            // Draw the rectangle behind the player:



            // Draw the edge of the mountains:

            foreach (Line line in m_level.lines)
            {
                Vector2 start = new Vector2(line.x1, line.y1); 
                Vector2 end = new Vector2(line.x2, line.y2); 

                Vector2 edge = end - start;
                float angle =
                    (float)Math.Atan2(edge.Y, edge.X);


                m_spriteBatch.Draw(t,
                    new Rectangle(
                        (int)start.X,
                        (int)start.Y - 2,
                        (int)edge.Length() + 1, 
                        4),
                    null,
                    Color.White, 
                    angle,     
                    new Vector2(0, 0), 
                    SpriteEffects.None,
                    0);
                

            }



            
            m_spriteBatch.Draw(
                    playerTexture,
                    new Rectangle((int)playerX, (int)playerY, playerRectangle.Width, playerRectangle.Height),
                    null, // Drawing the whole texture, not a part
                    Color.White,
                    (float)m_level.playerAngle,
                    new Vector2(playerTexture.Width / 2, playerTexture.Height / 2),
                    SpriteEffects.None,
                    0);
            Vector2 stringSize1 = m_font.MeasureString("Fuel   : " + string.Format("{0:0.00}", playerFuel) + " s");
            m_spriteBatch.DrawString(m_font, "Fuel   : " + string.Format("{0:0.00}", playerFuel) + " s",
                new Vector2(m_graphics.PreferredBackBufferWidth * 0.75f - stringSize1.X / 2,
            m_graphics.PreferredBackBufferHeight / 4f - stringSize1.Y), playerFuel > 0 ? Color.Green : Color.White);


             stringSize1 = m_font.MeasureString("Speed  : " + string.Format("{0:0.00}", Math.Abs(m_level.playerVectorVelocity.Y)) + " m/s");
            m_spriteBatch.DrawString(m_font, "Speed  : " + string.Format("{0:0.00}", Math.Abs(m_level.playerVectorVelocity.Y)) + " m/s",
                new Vector2(m_graphics.PreferredBackBufferWidth * 0.75f - stringSize1.X / 2,
            m_graphics.PreferredBackBufferHeight / 4f - stringSize1.Y + stringSize1.Y), Math.Abs(m_level.playerVectorVelocity.Y) > 2 ? Color.White: Color.Green);


             stringSize1 = m_font.MeasureString("Angle  : " + string.Format("{0:0.00}",MathHelper.ToDegrees((float)m_level.playerAngle)) + "");
            m_spriteBatch.DrawString(m_font, "Angle  : " + string.Format("{0:0.00}",MathHelper.ToDegrees((float)m_level.playerAngle)) + "",
                new Vector2(m_graphics.PreferredBackBufferWidth * 0.75f - stringSize1.X / 2,
            m_graphics.PreferredBackBufferHeight / 4f - stringSize1.Y + 2*stringSize1.Y), MathHelper.ToDegrees((float)m_level.playerAngle) < 5 || MathHelper.ToDegrees((float)m_level.playerAngle) > 355 ? Color.Green : Color.White);

            


            m_spriteBatch.End();

            // Render triangle: 
            foreach (EffectPass pass in m_effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                m_graphics.GraphicsDevice.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    m_level.m_vertsTris, 0, m_level.m_vertsTris.Length,
                    m_level.m_indexTris, 0, m_level.m_indexTris.Length / 3);
            }


        }

      

        public override void update(GameTime gameTime)
        {

            if (currentLevel == Level.LEVELONE && currentStage == Stage.COMPLETED)
            {
                currentLevel = Level.LEVELTWO;
                currentStage = Stage.PLAYING;
            }
            if (!isCollision())
            {
                m_level.playerVectorVelocity += m_level.gravityVector * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.5f;
                m_level.playerVectorVelocity += m_level.thrustVector * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.5f;
                playerX += (float)(m_level.playerVectorVelocity.X * 0.1);
                playerY -= (float)(m_level.playerVectorVelocity.Y * 0.1);
                playerCircle.setCenter(new Tuple<double, double>(playerCircle.center.Item1 + (m_level.playerVectorVelocity.X * 0.1), playerCircle.center.Item2 - (m_level.playerVectorVelocity.Y * 0.1)));

            }
        }
        private void onMoveUp(GameTime gameTime)
        {
            if (playerFuel > 0)
            {
                m_level.thrustVector.X += (float)Math.Cos(m_level.playerAngle - Math.PI / 2);
                m_level.thrustVector.Y -= (float)Math.Sin(m_level.playerAngle - Math.PI / 2);
                playerFuel -= 0.002 * gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            isUpPressed = true;
            //positionY -= 1 * gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void ModifyKey(KeyEnum keyType, Keys newKey)
        {
            if (keyType == KeyEnum.Left)
            {
                keyboardInput.removeKey(left);
                keyboardInput.registerCommand(newKey, false, new IInputDevice.CommandDelegate(onMoveLeft));
                left  = newKey;
            }
            else if (keyType == KeyEnum.Right)
            {
                keyboardInput.removeKey(right);
                keyboardInput.registerCommand(newKey, false, new IInputDevice.CommandDelegate(onMoveRight));
                right = newKey;
            }
            else if (keyType == KeyEnum.Up)
            {
                keyboardInput.removeKey(up);
                keyboardInput.registerCommand(newKey, false, new IInputDevice.CommandDelegate(onMoveUp));
                up = newKey;
            }
        }
        public void resetGameplay()
        {
            playerX = 50f;
            playerY = 50f;
            currentLevel = Level.LEVELONE;
            currentStage = Stage.PLAYING;
            m_level = new LunarLanderLevel(1, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight);
            playerFuel = 20d;
            playerCircle = new Circle(new Tuple<double, double>(50, 50), Math.Max(playerTexture.Width / 2, playerTexture.Height / 2));


        }

        public bool isCollision()
        {
            for (int i = 0; i < m_level.lines.Count; i++)
            {
                if (lineCircleInterSection(m_level.lines[i], playerCircle))
                { 
                    return true;
                }

            }

            


            /* function lineCircleIntersection(pt1, pt2, circle)
             {
                 let v1 = { x: pt2.x - pt1.x, y: pt2.y - pt1.y };
         let v2 = { x: pt1.x - circle.center.x, y: pt1.y - circle.center.y };
         let b = -2 * (v1.x * v2.x + v1.y * v2.y);
         let c = 2 * (v1.x * v1.x + v1.y * v1.y);
         let d = Math.sqrt(b * b - 2 * c * (v2.x * v2.x + v2.y * v2.y - circle.radius * circle.radius));
     if (isNaN(d)) { // no intercept
         return false;
     }
     // These represent the unit distance of point one and two on the line
     let u1 = (b - d) / c;
         let u2 = (b + d) / c;
     if (u1 <= 1 && u1 >= 0) {  // If point on the line segment
         return true;
     }
     if (u2 <= 1 && u2 >= 0) {  // If point on the line segment
         return true;
     }
     return false;*/



            return false; 
        }

        public bool lineCircleInterSection(Line line, Circle circle)
        {
            var v1 = new { X = line.x2 - line.x1, Y = line.y2 - line.y1 };
            var v2 = new { X = line.x1 - circle.center.Item1, Y = line.y1 - circle.center.Item2 };
            var b = -2 * (v1.X * v2.X + v1.Y * v2.Y);
            var c = 2 * (v1.X * v1.X + v1.Y * v1.Y);
            var d = Math.Sqrt(b * b - 2 * c * (v2.X * v2.X + v2.Y * v2.Y - circle.radius * circle.radius));

            if (double.IsNaN(d)) // no intercept
            {
                return false;
            }

            // These represent the unit distance of point one and two on the line
            var u1 = (b - d) / c;
            var u2 = (b + d) / c;

            if (u1 <= 1 && u1 >= 0) // If point on the line segment
            {
                return true;
            }

            if (u2 <= 1 && u2 >= 0) // If point on the line segment
            {
                return true;
            }

            return false;
        }


    
    }
}
