using System;

namespace CSharpLaborationer
{
    class Program
    {
        public void test() {
        }

        public void test(string s) {  }
       
        private static int change;

        private static void test(int i)
        {
            int[] m = new int[10];
            


            if( i != 0 )
            {
                Console.WriteLine("Inte Noll");
            }

            if (i < 3 && i >= 0) //0 och 1 och 2
            {
                Console.WriteLine("1 eller 2");
                
            }
            else if ( i <= 5 && i > 2 ) //3 och 4 och 5
            {
                Console.WriteLine("3 eller 4");
            }
            else
            {
                Console.WriteLine("Others");
            }
        }


        private static int readIntValueFromConsole(string promptMessage)
        {
            Console.WriteLine(promptMessage);
            string readline = Console.ReadLine();
            return Convert.ToInt32(readline);
        }

        private static int calculateChange(int v)
        {
            int value = change / v;
            change = change % v;
            return value;
        }

        private static void calculateAndPrintChange(int paidAmount, int sumToPay)
        {
            change = paidAmount - sumToPay;

            int num200Bills = calculateChange(200);
            int num100Bills = calculateChange(100);
            int num50Bills = calculateChange(50);
            int num20Bills = calculateChange(20);
            int num10Coins = calculateChange(10);
            int num1Coins = change;

            Console.Write("Number of 200 kr bills: " + num200Bills +
                          "\nNumber of 100 kr bills: " + num100Bills +
                          "\nNumber of  50 kr bills: " + num50Bills +
                          "\nNumber of  20 kr bills: " + num20Bills +
                            "\nNumber of  10 kr coins: " + num10Coins +
                            "\nNumber of   1 kr coins: " + num1Coins);


        }

        static void readAndIF()
        {
            char key = Console.ReadKey().KeyChar;
            string message = "";
            if (key == 'a' || key == 'A')
                message = "Ahhh";
            else if (key == '2')
                message = "TVÅ";
            else if (key == '3')
                message = "TRE";
            Console.WriteLine(message);
        }
        static void readAndSwitch()
        {
            char key = Console.ReadKey().KeyChar;
            string message = "";

            switch (key)
            {
                case 'a':
                case 'A':
                    message = "Ahhh";
                    break;
                case '2':
                    message = "TVÅ";
                    break;
                case '3':
                    message = "TRE";
                    break;
            }
            Console.WriteLine(message);
        }


        private class Car
        {
            public Color _color;
            private int weight;
            private int maxspeed;

            public Car()
            {
            }
        }
        static void Main(string[] args)
        {
            Car car = new Car();  //Bil 1 skapad

            Rectangle rec = new Rectangle();
            
            rec.Width = 10;
            rec.Height = 20;


            Rectangle rec2 = new Rectangle(10, 20, new Color());
          
        



            int i = 0;
            while (i < args.Length)
            {
                

                Console.WriteLine(args[i]);
                i++;
            }
            
            Console.ReadKey();
            return;            

            int paidAmount = readIntValueFromConsole("Enter paid amount:");

            int sumToPay = readIntValueFromConsole("Enter sum to pay:");

            if(paidAmount < 1 || sumToPay < 1)
            {
                Console.WriteLine("Error: 0 or negative ");
                //Wait for a keystroke.
                Console.ReadKey();
                return;
            }

            if (paidAmount < sumToPay)
            {
                Console.WriteLine("Pay more, Please!");
            }
            else if(paidAmount == sumToPay)
            {
                Console.WriteLine("No change!");
            }
            else
                calculateAndPrintChange(paidAmount, sumToPay);

            //Wait for a keystroke.
            Console.ReadKey();
        }

       
    }
}


class Rectangle
{
    private Color _color;
    private int _width;
    private int _height;

    public Rectangle()
    {
        _width = 1;
        _height = 1;
        _color = new Color();
    }
    public Rectangle(int width, int height, Color color)
    {
        _width = width;
        _height = height;
        _color = color;
    }

    public Color Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public int Width
    {
        get { return _width; }
        set { _width = value; }
    }
    public int Height
    {
        get { return _height; }
        set { _height = value; }
    }

    public int calculateArea()
    {
        return _width * _height;
    }
}

class Color
{
}