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

        public CommClient(String IP , int NumPort)
        {
            IPE = new IPEndPoint(IPAddress.Parse(IP), NumPort);
            sock = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            sock.Connect(IPE);
        }
    }
}
