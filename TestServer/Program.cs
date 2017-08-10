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

        private static void Server_OnRecieve(System.Net.IPEndPoint address, byte[] data)
        {
            if (data[0] == 1)
            {
                string result = NetHelper.FromByteUTF8(data.Skip(1).ToArray());
                Console.WriteLine("Recieved data from {0}:{1}: [{2}]", address.Address, address.Port, String.Join(", ", data));
                Console.WriteLine(result.Replace("\n", ""));
                var buffer = NetHelper.AppendText(1, "Welcome client!", Encoding.UTF8);
                buffer[0] = 1;
                server.Send(address, buffer);
            }
        }
    }
}
