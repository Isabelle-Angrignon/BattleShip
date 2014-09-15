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

        public CommClient()
        {
            IPE = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1223);
            sock = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            sock.Connect(IPE);
        }
    }
}
