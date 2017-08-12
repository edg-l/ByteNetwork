using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ByteNetwork;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client started");
            Thread.Sleep(100);
            NetClient client = new NetClient("127.0.0.1", 8706);
            client.OnRecieve += Client_OnRecieve;

            Thread ClientThread = new Thread(client.Listen);
            ClientThread.Start();

            Packet send_packet = new Packet();
            send_packet.Write((byte)1);
            send_packet.Write("Hello world!");
            client.Send(send_packet);
        }

        private static void Client_OnRecieve(Packet packet)
        {
            var reader = new PacketReader(packet);
            var type = reader.Read<byte>();
            if (type == 1)
            {
                string result = reader.Read<string>();
                Console.WriteLine("Recieved data: {0}", result);
            }
        }
    }
}
