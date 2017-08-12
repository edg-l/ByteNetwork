using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteNetwork
{
    public class Packet
    {
        public List<byte> Buffer { get; set; }

        public Packet()
        {
            Buffer = new List<byte>();
        }

        public Packet(byte[] data)
        {
            Buffer = new List<byte>(data);
        }

        public void Write(byte n)
        {
            Buffer.Add(n); // len = 1
        }

        public void Write(short n)
        {
            Buffer.AddRange(BitConverter.GetBytes(n)); // len = 2
        }

        public void Write(bool n)
        {
            Buffer.Add(BitConverter.GetBytes(n)[0]); // len = 1
        }

        public void Write(int n)
        {
            var bytes = BitConverter.GetBytes(n); // len = 4
            Buffer.AddRange(bytes);
        }

        public void Write(float n)
        {
            var bytes = BitConverter.GetBytes(n); // len = 4
            Buffer.AddRange(bytes);
        }

        public void Write(double n)
        {
            var bytes = BitConverter.GetBytes(n); // len = 8
            Buffer.AddRange(bytes);
        }

        public void Write(long n)
        {
            var bytes = BitConverter.GetBytes(n); // len = 8
            Buffer.AddRange(bytes);
        }

        public void Write(string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str); // len = can't know
            Write(buffer.Length); // Write size before appending str
            Buffer.AddRange(buffer);
        }
    }
}
