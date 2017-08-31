using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ByteNetwork.Example.Server
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

        private static void Server_OnRecieve(System.Net.IPEndPoint address, NetPacket packet)
        {
            var reader = new NetPacketReader(packet);
            var type = reader.Read<byte>();
            if (type == 1)
            {
                string result = reader.Read<string>();
                Console.WriteLine("Recieved data from {0}:{1}: {2}", address.Address, address.Port, result);

                NetPacket send_packet = new NetPacket();
                send_packet.Write((byte)1);
                send_packet.Write("Hello client");
                server.Send(address, send_packet);
            }
        }
    }
}
