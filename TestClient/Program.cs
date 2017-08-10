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
            Client client = new Client();
            client.OnRecieve += Client_OnRecieve;
            client.Connect("127.0.0.1", 8706);

            Thread ClientThread = new Thread(client.Listen);
            ClientThread.Start();

            var example = "Hello world!";
            byte[] buffer = Helper.AppendText(1, example, Encoding.UTF8);
            buffer[0] = 1;

            client.Send(buffer);
            Thread.Sleep(200);
            Console.Read();
        }

        private static void Client_OnRecieve(byte[] data)
        {
            if (data[0] == 1)
            {
                string result = Helper.FromByteUTF8(data.Skip(1).ToArray());
                Console.WriteLine("Recieved data: [{0}]", String.Join(", ", data));
                Console.WriteLine(result.Replace("\n", ""));
            }
        }
    }
}
