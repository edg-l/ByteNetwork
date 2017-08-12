using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteNetwork;
using System.Threading;

namespace TestServer
{
    class Program
    {
        static NetServer server;

        static void Main(string[] args)
        {
            Console.WriteLine("Server started");
            server = new NetServer(8706);
            server.OnRecieve += Server_OnRecieve;

            Thread ListenThread = new Thread(server.Listen);
            ListenThread.Start();
        }

        private static void Server_OnRecieve(System.Net.IPEndPoint address, Packet packet)
        {
            var reader = new PacketReader(packet);
            var type = reader.Read<byte>();
            if (type == 1)
            {
                string result = reader.Read<string>();
                Console.WriteLine("Recieved data from {0}:{1}: {2}", address.Address, address.Port, result);

                Packet send_packet = new Packet();
                send_packet.Write((byte)1);
                send_packet.Write("Hello client");
                server.Send(address, send_packet);
            }
        }
    }
}
