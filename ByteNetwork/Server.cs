using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ByteNetwork
{
    public class NetServer
    {
        public delegate void Recieve(IPEndPoint address, Packet packet);
        public event Recieve OnRecieve;

        private UdpClient ServerUDP;
        private IPEndPoint Endpoint;

        private bool StopListening = false;

        public UdpClient GetUDPClient() => ServerUDP;

        public NetServer(int port)
        {
            ServerUDP = new UdpClient(port);
            Endpoint = new IPEndPoint(IPAddress.Any, port);
        }

        public void Listen()
        {
            while (!StopListening)
            {
                var data = ServerUDP.Receive(ref Endpoint);
                OnRecieve(Endpoint, new Packet(data));
            }
        }

        public void Send(IPEndPoint endpoint, Packet packet)
        {
            var data = packet.Buffer.ToArray();
            ServerUDP.Send(data, data.Length, endpoint);
        }

        public void Stop()
        {
            StopListening = true;
        }
    }
}
