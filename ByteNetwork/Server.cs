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
        private IPEndPoint Endpoint;

        private bool StopListening = false;

        public Server(int port)
        {
            ServerUDP = new UdpClient(port);
            Endpoint = new IPEndPoint(IPAddress.Any, port);
        }

        public void Listen()
        {
            while (!StopListening)
            {
                var data = ServerUDP.Receive(ref Endpoint);
                OnRecieve(Endpoint, data);
            }
        }

        public void Send(IPEndPoint endpoint, byte[] data)
        {
            ServerUDP.Send(data, data.Length, endpoint);
        }

        public void Stop()
        {
            StopListening = true;
        }
    }
}
