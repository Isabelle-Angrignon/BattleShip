using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace CommServeur
{
    class CommunicationServeur
    {
        Socket sockConn;
        Socket sockJoueur1;
        Socket sockJoueur2;
        private int nbJoueurConnecter;
        static byte[] Buffer { get; set; }

        /// <summary>
        /// Seulement entre 0 et 2 
        /// </summary>
        private int NbJoueurConnecter
        {
            get { return nbJoueurConnecter; }
            set { if (nbJoueurConnecter >= 0 && nbJoueurConnecter <= 2) { nbJoueurConnecter = value; } }
        }


        /// <summary>
        /// Constructeur de la communication du serveur
        /// </summary>
        /// <param name="Ip"> L'ip du serveur </param>
        /// <param name="Port"> Le port du serveur</param>

        public CommunicationServeur(String Ip , int Port){
            InitialiserSocket(ref sockConn , Ip , Port);
        }
        /// <summary>
        /// Attend la connection d'un joueur si un joueur se connecte le met en tant
        ///  que joueur un ou joueur 2 selon si il est arrivé en premier.
        /// </summary>
        /// <param name="NbJoueurConnecter">Dis si on veut connecter un joueur ou deux </param>
        public void Connection(int NbJoueurAConnecter)
        {
            if (NbJoueurAConnecter >= 1 && NbJoueurAConnecter <= 2)
            {
                sockConn.Listen(1);
                sockJoueur1 = sockConn.Accept();

                if (NbJoueurAConnecter == 2)
                {
                    sockConn.Listen(1);
                    sockJoueur2 = sockConn.Accept();
                }
            }
            else
            {
                throw new Exception("Le nombre de joueur a connecter doit être 1 ou 2 ");
            }
        }
        /// <summary>
        /// Initialise un socket 
        /// </summary>
        /// <param name="sock">Le socket a initialiser ( passage par référence)</param>
        /// <param name="Ip"> L'ip avec lequel le socet doit être initialiser </param>
        /// <param name="Port"> Le port avec lequel le socket doit être initialiser</param>
        private void InitialiserSocket(ref Socket sock, String Ip, int Port)
        {
            IPEndPoint Iep;
            sock = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            Iep = new IPEndPoint(IPAddress.Parse(Ip), Port);
            sock.Bind(Iep);
        }


        /// <summary>
        /// Permet d'envoyer un message à un client 
        /// </summary>
        /// <param name="numeroJoueur">Le joueur auquel il faut envoyer le message</param>
        /// <param name="Message"> Le message qu'il faut envoyer </param>
        public void EnvoyerMessage(int numeroJoueur, String Message)
        {

        }


       

    }
}
