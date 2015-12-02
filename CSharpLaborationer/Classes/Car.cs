using System;

namespace Classes
{
    public class Car
    {
        private Color color;
        private int weight;
        private Engine engine;
        private bool started;

        public Car()  //default constructor
        {
            color = new Color(127,0,0);
            started = false;
        }

        public Car(bool startstate)
        {
            started = startstate;
        }

        public bool isStarted()
        {
            return started;
        }

        public void stop()
        {
            started = false;
        }

        public void start()
        {
            started = true;
        }
    }
}