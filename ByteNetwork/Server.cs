using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ByteNetwork
{
    public class Server
    {
        public delegate void Recieve(IPEndPoint address, byte[] data);
        public event Recieve OnRecieve;

        private UdpClient ServerUDP;

        public void Start(int port)
        {
            ServerUDP = new UdpClient(port);

            while (true)
            {
                System.Net.IPEndPoint endpoint = new System.Net.IPEndPoint(IPAddress.Any, port);
                var data = ServerUDP.Receive(ref endpoint);
                OnRecieve(endpoint, data);
            }
        }

        public void Send(IPEndPoint endpoint, byte[] data)
        {
            ServerUDP.Send(data, data.Length, endpoint);
        }
    }
}
