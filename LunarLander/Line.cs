using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLander
{
    public class Line
    {
        public int x1;
        public int y1;
        public int x2;
        public int y2;
        public bool isSafe;
        public Line(int x1, int y1, int x2, int y2, bool isSafe) 
        {
            this.x1 = x1;
            this.y1 = y1;
                
            this.x2 = x2;
            this.y2 = y2;

            this.isSafe = isSafe;

        }

    }
}
