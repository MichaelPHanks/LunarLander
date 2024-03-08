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
        private VertexPositionColor[] m_vertsTriStrip;
        private int[] m_indexTriStrip;

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
                


                Line safeZone1 = new Line(width / 5, height / 2, width / 4, height / 2);


                Line safeZone2 = new Line(width / 2, (int)(height / 1.5), (int)(width / 1.5), (int)(height / 1.5));
                // Create line from beginning to first safe zone, another line from end of first safe zone to
                // beginning of second safe zone, and a last one from end of second safe zone to end of screen.

                Line firstThird = new Line(0, (int)(height / 1.25), width / 5, height / 2);
                Line secondThird = new Line(width / 4, height / 2, width / 2, (int)(height / 1.5));
                Line thirdThird = new Line((int)(width / 1.5), (int)(height / 1.5), width, (int)(height / 1.25));

                lines.Add(firstThird);
                lines.Add(secondThird);
                lines.Add(thirdThird);

                List<Line> finalMap = midPointFormula(lines);
                lines = finalMap.ToList();
                // Create triangles below map.

                finalMap.Insert((finalMap.Count / 3) , safeZone1);
                finalMap.Insert((finalMap.Count * 2/3) + 1, safeZone2);



                List<VertexPositionColor> vertexPositionColors = new List<VertexPositionColor>();
                m_vertsTris = new VertexPositionColor[finalMap.Count * 2 * 3];

                int mapCounter = 0;
                m_indexTris = new int[finalMap.Count * 3 * 2];

                for (int i = 0; i < m_vertsTris.Length; i+=6)
                {
                    if (finalMap[mapCounter].y1 == 0)
                    {
                        Console.WriteLine();
                    }
                    m_vertsTris[i].Position = new Vector3(finalMap[mapCounter].x1, finalMap[mapCounter].y1, 0);
                    m_vertsTris[i].Color = Microsoft.Xna.Framework.Color.Gray;
                    m_indexTris[i] = i;

                    m_vertsTris[i + 1].Position = new Vector3(finalMap[mapCounter].x2, finalMap[mapCounter].y2, 0);
                    m_vertsTris[i + 1].Color = Microsoft.Xna.Framework.Color.Gray;
                    m_indexTris[i+ 1] = i+ 1;

                    m_vertsTris[i + 2].Position = new Vector3(finalMap[mapCounter].x1, height, 0) ;
                    m_vertsTris[i + 2].Color = Microsoft.Xna.Framework.Color.Gray;
                    m_indexTris[i + 2] = i + 2;

                    m_vertsTris[i + 3].Position = new Vector3(finalMap[mapCounter].x2, finalMap[mapCounter].y2, 0);
                    m_vertsTris[i + 3].Color = Microsoft.Xna.Framework.Color.Gray;
                    m_indexTris[i + 3] = i + 3;

                    m_vertsTris[i + 4].Position = new Vector3(finalMap[mapCounter].x2, height, 0);
                    m_vertsTris[i + 4].Color = Microsoft.Xna.Framework.Color.Gray;
                    m_indexTris[i + 4] = i + 4;

                    m_vertsTris[i + 5].Position = new Vector3(finalMap[mapCounter].x1, height, 0);
                    m_vertsTris[i + 5].Color = Microsoft.Xna.Framework.Color.Gray;
                    m_indexTris[i + 5] = i + 5;
                    /*if (i % 3 == 0)
                    {
                        m_vertsTris[i].Position = new Vector3(finalMap[mapCounter].x1, finalMap[mapCounter].y1, 0);
                        m_vertsTris[i].Color = Microsoft.Xna.Framework.Color.Red;
                    }
                    if (i - 1 % 3 == 0)
                    {
                        m_vertsTris[i].Position = new Vector3(finalMap[mapCounter].x2, finalMap[mapCounter].y2, 0);
                        m_vertsTris[i].Color = Microsoft.Xna.Framework.Color.Red;
                    }
                    else 
                    {
                        m_vertsTris[i].Position = new Vector3(finalMap[mapCounter].x1, finalMap[mapCounter].y1, 0);
                        m_vertsTris[i].Color = Microsoft.Xna.Framework.Color.Red;
                    }*/


                    
                    mapCounter++;
                    
                    
                }
                /*m_vertsTris[0].Position = new Vector3(100, 300, 0);
                m_vertsTris[0].Color = Microsoft.Xna.Framework.Color.Red;
                m_vertsTris[1].Position = new Vector3(200, 100, 0);
                m_vertsTris[1].Color = Microsoft.Xna.Framework.Color.Green;
                m_vertsTris[2].Position = new Vector3(300, 300, 0);
                m_vertsTris[2].Color = Microsoft.Xna.Framework.Color.Blue;

                m_indexTris = new int[3];

                // Triangle 1 - Indexes
                m_indexTris[0] = 0;
                m_indexTris[1] = 1;
                m_indexTris[2] = 2;*/
                //vertexPositionColors[0].Position = new Vector3()

                /*m_vertsTris = new VertexPositionColor[6];

                // Define the position and color for each vertex - Triangle 1
                m_vertsTris[0].Position = new Vector3(100, 300, 0);
                m_vertsTris[0].Color = Microsoft.Xna.Framework.Color.Red;
                m_vertsTris[1].Position = new Vector3(200, 100, 0);
                m_vertsTris[1].Color = Microsoft.Xna.Framework.Color.Green;
                m_vertsTris[2].Position = new Vector3(300, 300, 0);
                m_vertsTris[2].Color = Microsoft.Xna.Framework.Color.Blue;

                // Define the position and color for each vertex - Triangle 2
                m_vertsTris[3].Position = new Vector3(500, 300, 0);
                m_vertsTris[3].Color = Microsoft.Xna.Framework.Color.Blue;
                m_vertsTris[4].Position = new Vector3(600, 100, 0);
                m_vertsTris[4].Color = Microsoft.Xna.Framework.Color.Green;
                m_vertsTris[5].Position = new Vector3(700, 300, 0);
                m_vertsTris[5].Color = Microsoft.Xna.Framework.Color.Red;

                // Create an array that holds the 'index' of each vertex
                // for each triangle, in groups of 3
                m_indexTris = new int[6];

                // Triangle 1 - Indexes
                m_indexTris[0] = 0;
                m_indexTris[1] = 1;
                m_indexTris[2] = 2;

                // Triangle 2 - Indexes
                m_indexTris[3] = 3;
                m_indexTris[4] = 4;
                m_indexTris[5] = 5;

                //
                // Define the data for 3 triangles in a triangle strip
                m_vertsTriStrip = new VertexPositionColor[5];
                m_vertsTriStrip[0].Position = new Vector3(200, 600, 0);
                m_vertsTriStrip[0].Color = Microsoft.Xna.Framework.Color.Red;
                m_vertsTriStrip[1].Position = new Vector3(300, 400, 0);
                m_vertsTriStrip[1].Color = Microsoft.Xna.Framework.Color.Green;
                m_vertsTriStrip[2].Position = new Vector3(400, 600, 0);
                m_vertsTriStrip[2].Color = Microsoft.Xna.Framework.Color.Blue;
                m_vertsTriStrip[3].Position = new Vector3(500, 400, 0);
                m_vertsTriStrip[3].Color = Microsoft.Xna.Framework.Color.Red;
                m_vertsTriStrip[4].Position = new Vector3(600, 600, 0);
                m_vertsTriStrip[4].Color = Microsoft.Xna.Framework.Color.Green;

                m_indexTriStrip = new int[6];
                m_indexTriStrip[0] = 0;
                m_indexTriStrip[1] = 1;
                m_indexTriStrip[2] = 2;
                m_indexTriStrip[3] = 3;
                m_indexTriStrip[4] = 4;*/

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
                    int midX = (lines[j].x2 + lines[j].x1) / 2;
                    int midY = Math.Abs((lines[j].y2 + lines[j].y1)) / 2;

                    // Get a random number from -25 to 25???

                    // X stays the same, y changes.
                    double u1 = 1.0 - random.NextDouble(); //uniform(0,1] random doubles
                    double u2 = 1.0 - random.NextDouble();
                    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                 Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                    double randNormal =
                                 0 + 1 * randStdNormal;


                    int r = (int)(0.5 * randStdNormal * Math.Abs(lines[j].x2 - lines[j].x1)) ;

                    midY = (int)(0.5 * (lines[j].y2 + lines[j].y1) + r);
                    //midY += random.Next(-50,50);
                    if (midY <  100)
                    {
                        Console.WriteLine();
                    }
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
