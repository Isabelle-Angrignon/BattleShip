using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommServeur
{
    class Program
    {
        static void Main(string[] args)
        {
            CommunicationServeur comm = new CommunicationServeur("127.0.0.1", 1223);
            Console.WriteLine("En attente de la connexion d'un client ");
            comm.Connection(1);
            Console.WriteLine("un joueur c'est connecter");
            Console.ReadKey();

        }
    }
}
