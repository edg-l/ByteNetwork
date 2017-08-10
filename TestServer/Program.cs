using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteNetwork;

namespace TestServer
{
    class Program
    {
        static Server server;
        static void Main(string[] args)
        {
            Console.WriteLine("Server started");
            server = new Server();
            server.OnRecieve += Server_OnRecieve;
            server.Start(8706);
        }

        private static void Server_OnRecieve(System.Net.IPEndPoint address, byte[] data)
        {
            if (data[0] == 1)
            {
                string result = Helper.FromByteUTF8(data.Skip(1).ToArray());
                Console.WriteLine("Recieved data from {0}:{1}: [{2}]", address.Address, address.Port, String.Join(", ", data));
                Console.WriteLine(result.Replace("\n", ""));
                var buffer = Helper.AppendText(1, "Welcome client!", Encoding.UTF8);
                buffer[0] = 1;
                server.Send(address, buffer);
            }
        }
    }
}
