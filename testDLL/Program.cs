using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlotteDLL;

namespace testDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            Flotte maFlotte = new Flotte();

            Console.WriteLine("Mes navires: ");
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine(maFlotte._flotte[i]);
            }

            Console.ReadKey();
        }
    }
}
