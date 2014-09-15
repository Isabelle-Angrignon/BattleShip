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

            maFlotte._flotte[0]._pos[0].setPos(1, 1);
            maFlotte._flotte[4]._pos[0].setPos(9, 9);


            Console.WriteLine("Mes navires: ");
            Console.WriteLine(maFlotte);
           /* for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine(maFlotte._flotte[i]);
                //Console.WriteLine(maFlotte._flotte[i]._pos[0]);
            }*/
            
            Console.ReadKey();
        }
    }
}
