using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace CommClient
{
    class CommClients
    {
        Socket sock;
        IPEndPoint IPE;
        static byte[] Buffer { get; set; }
        public String nom { get; set; }

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="IP"> L'ip du serveur</param>
        /// <param name="NumPort"> Le port du serveur</param>
        public CommClients(String IP, int NumPort)
        {
            IPE = new IPEndPoint(IPAddress.Parse(IP), NumPort);
            sock = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            sock.Connect(IPE);
            nom = LireMessage();
        }
        /// <summary>
        ///  Méthode blocante servant a envoyer un message au serveur et en obtenir une réponse
        /// </summary>
        /// <param name="Message"> Le messsage a envoyer</param>
        /// <returns></returns>
        public String Communiquer(String Message)
        {
            string reponse = "";
            try
            {
                EnvoyerMessage(Message);
                reponse = LireMessage();
            }
            catch(Exception e)
            {

            }
            return reponse;
        }

        private String LireMessage()
        {
            String Reponse = "";
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
            return Reponse;
        }


        private void EnvoyerMessage(String Message)
        {
            byte[] data = Encoding.ASCII.GetBytes(Message);
            sock.Send(data);
        }
    }
}
