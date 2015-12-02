using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    class Program
    {

        /// <summary>
        /// Start method for our program
        /// </summary>
        /// <param name="args">array of program parameters from console</param>
        static void Main(string[] args)
        {
            //Skapa Circle objekt
            Circle c1 = new Circle(1);
            //Kopiera objektreferencen
            Circle c2 = c1;
            //Gör en kopia
            Circle c3 = c1.Clone();

            //Gör en kopiering av data
            Circle c4 = new Circle();
            //c1.CopyTo(c4);

            //Skriv ut alla objektens radier
            Console.WriteLine("C1: " +  c1.Radius);
            Console.WriteLine("C2: " + c2.Radius);
            Console.WriteLine("C3: " + c3.Radius);
            Console.WriteLine("C4: " + c4.Radius);
            Console.ReadLine();

            //Öka värdet på c1's radie
            c1.Radius++;
            //Skriv ut alla objektens radier
            Console.WriteLine("C1: " + c1.Radius);
            Console.WriteLine("C2: " + c2.Radius);
            Console.WriteLine("C3: " + c3.Radius);
            Console.WriteLine("C4: " + c4.Radius);
            Console.ReadLine();
            
            //Test av Point klassen och dess DistanceTo metod

            Point point1 = new Point(3, 4);
            Point point2 = new Point(6, 8);

            Point point3 = new Point(point1);
            
            double distance = point3.DistanceTo(point2);
            Console.WriteLine("Distance: " + distance);
            Console.ReadLine();
        }
    }
}
