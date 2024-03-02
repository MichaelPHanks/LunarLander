using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public VertexPositionColor[] m_vertsTris;
        public int[] m_indexTris;

        public Vector2 gravityVector;
        public Vector2 thrustVector;
        public Vector2 horizontalVector;
        public Vector2 playerVectorVelocity;
        public double playerAngle;
        private double playerLocation;
        private double thrust;
        private double speedVertical;
        private double fuel;

        public List<Line> lines;
        private double speedHorizontal { get; set; }
        public LunarLanderLevel(double levelNumber, int width, int height)
        {

            lines = new List<Line>();
            /*if (levelNumber == 1)
            {
                
            }
            else if (levelNumber == 2)
            {
                
            }*/

            gravityVector = new Vector2(0f, -9.8f);
            thrustVector = new Vector2(0f,0f);
            horizontalVector = new Vector2(0f,0f);
            playerVectorVelocity = new Vector2(0f,0f);
            playerAngle = Math.PI / 2;



            // Lets say height is 15, width is also 15.


            //lines.Add(new Line(0, height / 3, width, height / 3));

            // Add random lines for 'safe zone'.

            if (levelNumber == 1)
            {
                // Create two safe zones (will hard code for now)
                // First safe zone: (3,3.75) -> (3.75, 3.75)
                Line safeZone1 = new Line(width / 5, height / 4, width / 4, height / 4);

                
                Line safeZone2 = new Line(width / 2, height / 6, (int)(width / 1.5), height / 6);

                // Create line from beginning to first safe zone, another line from end of first safe zone to
                // beginning of second safe zone, and a last one from end of second safe zone to end of screen.

                Line firstThird = new Line(0, height / 3, width / 5, height / 4);
                Line secondThird = new Line(width / 4, height / 4, width / 2, height / 6);
                Line thirdThird = new Line((int)(width / 1.5), height / 6, width, height / 3);

                lines.Add(firstThird);
                lines.Add(secondThird);
                lines.Add(thirdThird);

                List<Line> finalMap = midPointFormula(lines);

                // Create triangles below map.

                finalMap.Insert((finalMap.Count / 3) , safeZone1);
                finalMap.Insert((finalMap.Count * 2/3) + 1, safeZone2);



                List<VertexPositionColor> vertexPositionColors = new List<VertexPositionColor>();
                m_vertsTris = new VertexPositionColor[finalMap.Count];

                m_vertsTris[0].Position = new Vector3(100, 300, 0);
                m_vertsTris[0].Color = Microsoft.Xna.Framework.Color.Red;
                m_vertsTris[1].Position = new Vector3(200, 100, 0);
                m_vertsTris[1].Color = Microsoft.Xna.Framework.Color.Green;
                m_vertsTris[2].Position = new Vector3(300, 300, 0);
                m_vertsTris[2].Color = Microsoft.Xna.Framework.Color.Blue;

                m_indexTris = new int[3];

                // Triangle 1 - Indexes
                m_indexTris[0] = 0;
                m_indexTris[1] = 1;
                m_indexTris[2] = 2;
                //vertexPositionColors[0].Position = new Vector3()

            }


            // Need to recursively take the midpoint of each of them and add new lines.

            

        }

        public List<Line> midPointFormula(List<Line> startingLines)
        {

            Random random = new Random();
            List<Line> lines = startingLines;

            for (int i = 0; i < 5; i ++)
            {
                //lines = startingLines;
                List<Line> lines1 = new List<Line>();

                for (int j = 0; j < lines.Count; j++)
                {
                    // Modify startingLines
                    int midX = (lines[j].x1 - lines[j].x2) / 2;
                    int midY = (lines[j].y1 - lines[j].y2) / 2;

                    // Get a random number from -25 to 25???

                    // X stays the same, y changes.

                    midY += random.Next(-25,25);

                    // Remove the starting lines
                    Line tempLine = lines[j];
                    //startingLines.RemoveAt(j + 2);


                    lines1.Add(new Line(lines[j].x1, lines[j].y1, midX, midY));
                    lines1.Add(new Line(midX, midY, lines[j].x2, lines[j].y2));

                    // First line
                    /*startingLines.Insert(j + 1,new Line());

                    if (j == lines.Count - 1)
                    {
                        startingLines.Insert(j + 2, new Line());

                    }
                    // Second line
                    else
                    {
                        startingLines.Insert(j + 2, new Line());
                    }
*/
                }
                lines = lines1;


            }

            // If line size is 4:
            // If j = 0, remove 0, if j = 1, remove at 2, if j = 3, remove at 


            



            return lines;
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
