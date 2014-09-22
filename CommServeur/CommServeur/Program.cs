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
            CommunicationServeur comm = new CommunicationServeur("127.0.0.1", 8088);
            Console.WriteLine("En attente de la connexion d'un client ");
            comm.Connection(2);
            Console.WriteLine("un joueur c'est connecter");
            comm.EnvoyerMessage(1, "Tu es le joueur 1");
            comm.EnvoyerMessage(2, "Tu es le joueur 2");
            Console.WriteLine(comm.LireMessage(1));
            Console.WriteLine(comm.LireMessage(2));
            Console.ReadKey();

        }
    }
}
