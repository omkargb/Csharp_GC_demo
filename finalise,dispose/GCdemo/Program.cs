using System;

namespace GCdemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Welcome to garbage collection program - using finalise \n");
            GCdemo d = new GCdemo();
            d = null;
            Console.WriteLine(" -> Null asssigned.");
            Console.ReadLine();
        }
    }

    class GCdemo
    {
        public GCdemo()
        {
            Console.WriteLine(" -> Object Created");
        }

        ~GCdemo()
        {
            Console.WriteLine(" -> Destructor Called.");
        }
    }


}
