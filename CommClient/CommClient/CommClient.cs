using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace CommClient
{
    class CommClient
    {
        Socket sock;
        IPEndPoint IPE;
        static byte[] Buffer { get; set; }

        public CommClient(String IP , int NumPort)
        {
            IPE = new IPEndPoint(IPAddress.Parse(IP), NumPort);
            sock = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            sock.Connect(IPE);
        }

        public String Communiquer(String Message)
        {
            EnvoyerMessage(Message);
            return LireMessage();
        }

        public String LireMessage()
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


        public void EnvoyerMessage(String Message)
        {
            byte[] data = Encoding.ASCII.GetBytes(Message);
            sock.Send(data);
        }
    }
}
