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
        Socket sockJoueur1 = null;
        Socket sockJoueur2 = null;
        private int nbJoueurConnecter = 2;
        static byte[] Buffer { get; set; }
        bool J1Connecter = false;
        bool J2Connecter = false;
        bool J1EnvoiBloque = false;
        bool J2EnvoiBloque = false;

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
            if(J1Connecter != false && J2Connecter != false )
            {
                throw new Exception("Les joueurs sont déjà connecter ! ");
            }
            else if (NbJoueurAConnecter >= 1 && NbJoueurAConnecter <= 2 && sockJoueur1 == null)
            {
                sockConn.Listen(1);
                sockJoueur1 = sockConn.Accept();
                J1Connecter = true;
                EnvoyerMessage(1,"Joueur1");
                nbJoueurConnecter--;

                if (NbJoueurAConnecter == 2)
                {
                    sockConn.Listen(1);
                    sockJoueur2 = sockConn.Accept();
                    J2Connecter = true;
                    EnvoyerMessage(2, "Joueur2");
                    nbJoueurConnecter--;
                }
            }
            else if (NbJoueurAConnecter == 2 && nbJoueurConnecter == 1)
            {
                sockConn.Listen(1);
                sockJoueur2 = sockConn.Accept();
                J2Connecter = true;
                EnvoyerMessage(2, "Joueur2");
                nbJoueurConnecter--;
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
            if (numeroJoueur == 1 && J1EnvoiBloque == false)
            {
                EnvoyerMessage(sockJoueur1, Message);
                J1EnvoiBloque = true;
            }
            else if (numeroJoueur == 1 && J1EnvoiBloque == true)
            {
                throw new Exception("Vous devez lire le message du joueur 1 avant de lui envoyer un nouveau message ");
            }

            if (numeroJoueur == 2 && J2EnvoiBloque == false)
            {
                EnvoyerMessage(sockJoueur2, Message);
                J2EnvoiBloque = true;
            }
            else if (numeroJoueur == 2 && J2EnvoiBloque == true)
            {
                throw new Exception("Vous devez lire le message du joueur 2 avant de lui envoyer un nouveau message ");
            }

            if ( numeroJoueur != 1 && numeroJoueur != 2)
            {
                throw new Exception("Le numero de joueur peut seulement être 1 ou 2");
            }
            
        }


        /// <summary>
        /// Permet d'envoyer un message à un client 
        /// </summary>
        /// <param name="sock">Le socket auquel il faut envoyer le message</param>
        /// <param name="Message">Le message a envoyer</param>
        private void EnvoyerMessage(Socket sock, String Message)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(Message);
                sock.Send(data);
            }
            catch(Exception e)
            {

            }
        }

        public String LireMessage(int numeroJoueur)
        {
            String Reponse = "";
            if (numeroJoueur == 1 && J1EnvoiBloque == true)
            {
                Reponse = LireMessage(sockJoueur1);
                J1EnvoiBloque = false;
            }
            else if (numeroJoueur == 1 && J1EnvoiBloque == false)
            {
                throw new Exception("Il n'y a rien a lire pour le joueur 1 ");
            }

            if (numeroJoueur == 2 && J2EnvoiBloque == true)
            {
                Reponse = LireMessage(sockJoueur2);
                J2EnvoiBloque = false;
            }
            else if (J2EnvoiBloque == false && numeroJoueur == 2)
            {
                throw new Exception("Il n'y a rien a lire pour le joueur 2 ");
            }

            if (numeroJoueur != 1 && numeroJoueur != 2)
            {
                throw new Exception("Le numero de joueur peut seulement être 1 ou 2");
            }
            return Reponse;
        }

        private String LireMessage(Socket sock)
        {
            String Reponse = "";
            try
            {
                while (Reponse == "")
                {
                    Buffer = new byte[sock.SendBufferSize];
                    int bytesRead = sock.Receive(Buffer);
                    byte[] formatted = new byte[bytesRead];
                    for (int i = 0; i < bytesRead; i++)
                    {
                        formatted[i] = Buffer[i];
                    }
                    Reponse = Encoding.ASCII.GetString(formatted);
                }
            }
            catch(Exception e)
            {

            }
            return Reponse;
        }
    }
}
