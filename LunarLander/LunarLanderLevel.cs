using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
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

        private int m_height;

        public List<Line> lines;

        private VertexPositionColor[] m_vertsTriStrip;
        private int[] m_indexTriStrip;

        private double speedHorizontal { get; set; }
        public LunarLanderLevel(double levelNumber, int width, int height)
        {

            lines = new List<Line>();
            m_height = height;

            gravityVector = new Vector2(0f, -9.8f);
            thrustVector = new Vector2(0f,0f);
            horizontalVector = new Vector2(0f,0f);
            playerVectorVelocity = new Vector2(0f,0f);
            playerAngle = Math.PI / 2;



           

            if (levelNumber == 1)
            {
               


                // Random y for safe zones : between half of screen and 4/5ths of screen

                // Random x for first safe zone: between first 1/8th of screen and first 1/3rd of screen.

                // Random x for second safe zone: between 1/2th of screen and 2/3rd's
                Random random = new Random();
                int fared = height / 2;
                int perhaps = height * (3 / 4);
                int firstY = random.Next(height / 2, (int)(height * (3f / 4f)));
                int secondY = random.Next(height / 2, (int)(height * (3f / 4f)));


                int firstX = random.Next(width / 8, width / 3);
                int secondX = random.Next(width / 2, (int)(width * (2f / 3f)));

               // Line safeZone1 = new Line(width / 5, height / 2, width / 4, height / 2, true);
                Line safeZone1 = new Line(firstX, firstY, firstX + width / 16, firstY, true);


                //Line safeZone2 = new Line(width / 2, (int)(height / 1.5), (int)(width / 1.5), (int)(height / 1.5), true);
                Line safeZone2 = new Line(secondX, secondY, secondX + width / 8, secondY, true);

                // Create line from beginning to first safe zone, another line from end of first safe zone to
                // beginning of second safe zone, and a last one from end of second safe zone to end of screen.

                //Line firstThird = new Line(0, (int)(height / 1.25), width / 5, height / 2, false);
                Line firstThird = new Line(0, (int)(height / 1.25), firstX, firstY, false);

                Line secondThird = new Line(safeZone1.x2, firstY, secondX, secondY, false);
                Line thirdThird = new Line(secondX + width / 8, secondY, width, (int)(height / 1.25), false);

                lines.Add(firstThird);
                lines.Add(secondThird);
                lines.Add(thirdThird);

                List<Line> finalMap = midPointFormula(lines, 6);
                // Create triangles below map.

                finalMap.Insert((finalMap.Count / 3), safeZone1);
                finalMap.Insert((finalMap.Count * 2 / 3) + 1, safeZone2);

                lines = finalMap.ToList();


                List<VertexPositionColor> vertexPositionColors = new List<VertexPositionColor>();
                m_vertsTris = new VertexPositionColor[finalMap.Count * 2 * 3];

                int mapCounter = 0;
                m_indexTris = new int[finalMap.Count * 3 * 2];

                for (int i = 0; i < m_vertsTris.Length; i += 6)
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
                    m_indexTris[i + 1] = i + 1;

                    m_vertsTris[i + 2].Position = new Vector3(finalMap[mapCounter].x1, height, 0);
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
                    



                    mapCounter++;


                }
                

            }


            else if (levelNumber == 2)
            {
                // Create two safe zones (will hard code for now)
                // First safe zone: (3,3.75) -> (3.75, 3.75)

                // Random start for x: between width / 8 and (int)(width * (3f / 4f))

                // Random value for y: same as before
                Random random = new Random();

                int firstY = random.Next(height / 2, (int)(height * (3f / 4f)));
                int firstX = random.Next(width / 8, (int)(width * (3f / 4f)));
                Line safeZone1 = new Line(firstX, firstY, firstX + width / 16, firstY, true);


                // Create line from beginning to first safe zone, another line from end of first safe zone to
                // beginning of second safe zone, and a last one from end of second safe zone to end of screen.

                Line firstHalf = new Line(0, (int)(height / 1.25), firstX, firstY, false);
                Line secondHalf = new Line(firstX + width / 16, firstY, width, (int)(height / 1.25), false);

                lines.Add(firstHalf);
                lines.Add(secondHalf);

                List<Line> finalMap = midPointFormula(lines,7);
                // Create triangles below map.

                finalMap.Insert((finalMap.Count / 2), safeZone1);

                lines = finalMap.ToList();


                List<VertexPositionColor> vertexPositionColors = new List<VertexPositionColor>();
                m_vertsTris = new VertexPositionColor[finalMap.Count * 2 * 3];

                int mapCounter = 0;
                m_indexTris = new int[finalMap.Count * 3 * 2];

                for (int i = 0; i < m_vertsTris.Length; i += 6)
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
                    m_indexTris[i + 1] = i + 1;

                    m_vertsTris[i + 2].Position = new Vector3(finalMap[mapCounter].x1, height, 0);
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




                    mapCounter++;


                }
            }



            

        }

        public List<Line> midPointFormula(List<Line> startingLines, int howManyIntervals)
        {

            Random random = new Random();
            List<Line> lines = startingLines;

            for (int i = 0; i < howManyIntervals; i ++)
            {
                //lines = startingLines;
                List<Line> lines1 = new List<Line>();

                for (int j = 0; j < lines.Count; j++)
                {
                    // Modify startingLines
                    int midX = (lines[j].x2 + lines[j].x1) / 2;
                    int midYTemp = Math.Abs((lines[j].y2 + lines[j].y1)) / 2;

                    // Get a random number from -25 to 25???

                    // X stays the same, y changes.
                    double u1 = 1.0 - random.NextDouble(); //uniform(0,1] random doubles
                    double u2 = 1.0 - random.NextDouble();
                    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                 Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                    double randNormal =
                                 0 + randStdNormal * 1;


                    

                    int r = (int)(0.45 * randStdNormal * Math.Abs(lines[j].x2 - lines[j].x1));

                    int midY = (int)(0.5 * (lines[j].y2 + lines[j].y1) + r);

                    
                    
                   


                    lines1.Add(new Line(lines[j].x1, lines[j].y1, midX, midY, false));
                    lines1.Add(new Line(midX, midY, lines[j].x2, lines[j].y2, false));

                    
                }
                lines = lines1;


            }

            


            



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
