using System;

namespace ProductsMVC.Providers
{
    public interface IRandom
    {
        int Next(int maxvalue);
    }

    public class TestRandom : IRandom
    {
        int[] list = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int next = 0;
        public int Next(int maxvalue)
        {
            if (maxvalue > 10)
                maxvalue = 10;
            return list[next++];
        }
    }

    public class NormalRandom : IRandom
    {
        Random r = new Random();

        public int Next(int maxvalue)
        {
            return r.Next(maxvalue);   
        }
    }
}