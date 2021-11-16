using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GCdemo2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" * Welcome to garbage collection program  * \n");

            Console.WriteLine(" The highest generation is : {0}\n ", GC.MaxGeneration);

            Console.WriteLine(" Before variable assignment ands objects creation - ");
            Console.WriteLine("\t Total Memory: {0} \t GC Collection Count: {1}",
                 GC.GetTotalMemory(false), GC.CollectionCount(0));


            Console.WriteLine("\n Some Calculations : ");
            for (double i = 1; i <= 25; i++)
            {
                double power = Math.Pow(i, 6);
                Console.Write("  " + power);
                if (power % 11 == 0)
                {
                    Console.Write("\n Object : demo --> " );
                    GCdemo2 demo = new GCdemo2();
                    Console.Write("\t Generation : {0} \t Total Memory: {1} \t GC Collection Count: {2} \n",
                        GC.GetGeneration(demo), GC.GetTotalMemory(false), GC.CollectionCount(0));
                    demo = null;
                    GC.Collect();
                    Console.WriteLine();
                }
            }

            unsafe
            {
                int value1 = 12345;
                int* pointer1 = &value1;
                Console.WriteLine("\n\n Pointer location of {0} : {1} \n", value1,(long)pointer1) ;
            }

            string[] data = new string[2000];


            GCdemo2 gd = new GCdemo2();

            Console.WriteLine("\n [ After object created ] ");
            Console.WriteLine("\t Generation : {0} \t Total Memory: {1} \t GC Collection Count: {2} \n",
                GC.GetGeneration(gd), GC.GetTotalMemory(false), GC.CollectionCount(0));

            string procname=Process.GetCurrentProcess().ProcessName;

            Console.WriteLine(" [ Disposing ]");
            gd.Dispose();

            Console.WriteLine("\n [ After dispose complete ]");
            Console.WriteLine("\t Generation : {0} \t Total Memory: {1} \t GC Collection Count: {2} \n", 
                GC.GetGeneration(gd), GC.GetTotalMemory(false), GC.CollectionCount(0));

            gd = null;

            Console.WriteLine(" [ Null assigned ]");

            Console.WriteLine("\n After null assignment - ");
            Console.WriteLine("\t Total Memory: {0} \t GC Collection Count: {1}"
               , GC.GetTotalMemory(false), GC.CollectionCount(0));


            GC.Collect();

            Console.WriteLine("\n [ After collect ] ");
            Console.WriteLine("\t Total Memory: {0} \t GC Collection Count: {1}"
               , GC.GetTotalMemory(false), GC.CollectionCount(0));

            Console.Write("\n Press any key to exit... ");
            Console.ReadLine();
        }
    }

    class GCdemo2 : IDisposable     //release unmanaged resources
    {
        private bool Disposed = false;
        public GCdemo2()
        {
            Console.WriteLine(" [ Object Created ]");
        }

        ~GCdemo2()
        {
            Console.WriteLine(" [ Destructor Called. ]");
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    Console.WriteLine(" [ Inside Dispose method - disposing ]");
                    //Clear all the managed resources here  
                }
                else
                {
                    //Clear all the unmanaged resources here 
                }
                Disposed = true;
                Console.WriteLine(" [ Disposed ]");
            }
        }
    }
}
