using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes
{
    public class Ellipse : Shape
    {
        public override double Area
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override double Perimeter
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Ellipse(double length, double width) : base(length,width)
        {

        }
    }
}