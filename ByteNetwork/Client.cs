using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ByteNetwork
{
    public class Client
    {
        private UdpClient ClientUDP;

        public delegate void Recieve(byte[] data);
        public event Recieve OnRecieve;
        private IPAddress Address;
        private int Port;

        public void Connect(string address, int port)
        {
            ClientUDP = new UdpClient();
            ClientUDP.Connect(address, port);
            Address = IPAddress.Parse(address);
            Port = port;
        }

        public void Listen()
        {
            while (true)
            {
                
                IPEndPoint endpoint = new IPEndPoint(Address, Port);
                var data = ClientUDP.Receive(ref endpoint);
                OnRecieve(data);
            }
        }

        public void Send(byte[] data)
        {
            ClientUDP.Send(data, data.Length);
        }
    }
}
