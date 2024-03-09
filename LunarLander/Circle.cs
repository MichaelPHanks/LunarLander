using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLander
{
    public class Circle
    {
        public Tuple<double, double> center;
        public float radius;

        public Circle(Tuple<double, double> center, float radius) 
        {
            this.center = center;
            this.radius = radius;
        }

        public void setCenter(Tuple<double, double> newCenter)
        {
            this.center = newCenter;
        }

        public void setRadius(float newRadius)
        {
            this.radius = newRadius;
        }

    }
}
