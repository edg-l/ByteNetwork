using System.Net.Sockets;
using System.Net;

namespace ByteNetwork
{
    public class NetServer
    {
        public delegate void Recieve(IPEndPoint address, NetPacket packet);
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

        /// <summary>
        /// Start recieving packets
        /// </summary>
        public void Listen()
        {
            while (!StopListening)
            {
                var data = ServerUDP.Receive(ref Endpoint);
                OnRecieve(Endpoint, new NetPacket(data));
            }
        }

        /// <summary>
        /// Send a packet to the specified endpoint
        /// </summary>
        public void Send(IPEndPoint endpoint, NetPacket packet)
        {
            var data = packet.Buffer.ToArray();
            ServerUDP.Send(data, data.Length, endpoint);
        }

        /// <summary>
        /// Stop listening
        /// </summary>
        public void Stop()
        {
            StopListening = true;
        }
    }
}
