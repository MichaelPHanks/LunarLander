using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LunarLander
{
    /// <summary>
    ///     Handles the level generation (level 1 or 2), handles player controls, etc.
    /// </summary>
    public class LunarLanderLevel
    {
        public Vector2 gravityVector;
        public Vector2 thrustVector;
        public Vector2 horizontalVector;
        public Vector2 playerVectorVelocity;
        public double playerAngle;
        private double playerLocation;
        private double thrust;
        private double speedVertical;
        private double fuel;
        private double speedHorizontal { get; set; }
        public LunarLanderLevel(double levelNumber)
        {
            if (levelNumber == 1)
            {
                
            }
            else if (levelNumber == 2)
            {
                
            }

            gravityVector = new Vector2(0f, -9.8f);
            thrustVector = new Vector2(0f,0f);
            horizontalVector = new Vector2(0f,0f);
            playerVectorVelocity = new Vector2(0f,0f);
            playerAngle = 0;
        }
        public void setSpeedHorizontal(double newSpeed)
        { 
            this.speedHorizontal = newSpeed;
        }
        public double getSpeedHorizontal()
        {
            return speedHorizontal;
        }
        public double getSpeedVertical()
        {
            return speedVertical;
        }
        public double getThurst()
        {
            return thrust;
        }
        public double getPlayerLocation()
        {
            return playerLocation;
        }
        public double getPlayerAngle()
        {
            return playerAngle;
        }
        public double getFuel()
        {
            return fuel;
        }
    }
}
