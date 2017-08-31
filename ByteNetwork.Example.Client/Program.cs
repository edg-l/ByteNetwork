using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ByteNetwork.Example.Client
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

            NetPacket send_packet = new NetPacket();
            send_packet.Write((byte)1);
            send_packet.Write("Hello world!");
            client.Send(send_packet);
        }

        private static void Client_OnRecieve(NetPacket packet)
        {
            var reader = new NetPacketReader(packet);
            var type = reader.Read<byte>();
            if (type == 1)
            {
                string result = reader.Read<string>();
                Console.WriteLine("Recieved data: {0}", result);
            }
        }
    }
}
