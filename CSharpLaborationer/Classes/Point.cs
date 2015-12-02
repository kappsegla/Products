using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Point
    {
        private float x;
        private float y;

        public Point(Point prevpoint)
        {
            x = prevpoint.x;
            y = prevpoint.y;
        }

        public Point()
        {
            x = 0.0f;
            y = 0.0f;
        }
        public Point(float posx, float posy)
        {
            x = posx;
            y = posy;
        }

        public double DistanceTo(Point other)
        {
            float dx = this.x - other.x;
            float dy = this.y - other.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }


    }
}
