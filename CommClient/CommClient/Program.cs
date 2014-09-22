using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tentative de connexion");
            CommClient Communication = new CommClient("127.0.0.1", 8088);
            Console.WriteLine("Connexion Réussi");
            
            Console.ReadKey();
        }
    }
}
