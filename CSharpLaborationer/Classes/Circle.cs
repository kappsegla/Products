using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    class Circle
    {
        private int radius;
        
        public int Radius { get { return radius; } set { radius = value; } }

        public Circle()
        {
        }

        public Circle(int radius)
        {
            this.radius = radius;
        }
        
        public Circle Clone()     {
            // Create a new Circle object         
            Circle clone = new Circle();
            // Copy private data from this to clone
            clone.radius = this.radius;
            // Return the new Circle object containing the copied data
            return clone;
        }

        public void CopyTo(Circle clone)
        {
                // Copy private data from this to clone
                clone.radius = this.radius;
        }
    } 
}
