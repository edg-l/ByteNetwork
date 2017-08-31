using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace ByteNetwork
{
    public class NetPacketReader
    {
        NetPacket Packet;

        int CurrentIndex;

        public NetPacketReader(NetPacket packet)
        {
            Packet = packet;
            CurrentIndex = 0;
        }

        public void ResetRead()
        {
            CurrentIndex = 0;
        }

        public T Read<T>()
        {
            Type type = typeof(T);

            if(type == typeof(string))
            {
                var length = Read<int>();
                var string_bytes = Packet.Buffer.Skip(CurrentIndex).Take(length).ToArray();
                string value = Decompress(string_bytes);
                CurrentIndex += length;
                return (T)Convert.ChangeType(value, typeof(T));
            }
            if (type == typeof(int))
            {
                int value = BitConverter.ToInt32(Packet.Buffer.ToArray(), CurrentIndex);
                CurrentIndex += 4;
                return (T)Convert.ChangeType(value, typeof(T));
            }
            if (type == typeof(short))
            {
                short value = BitConverter.ToInt16(Packet.Buffer.ToArray(), CurrentIndex);
                CurrentIndex += 2;
                return (T)Convert.ChangeType(value, typeof(T));
            }
            if (type == typeof(byte))
            {
                short value = Packet.Buffer[CurrentIndex];
                CurrentIndex += 1;
                return (T)Convert.ChangeType(value, typeof(T));
            }
            if (type == typeof(float))
            {
                float value = BitConverter.ToSingle(Packet.Buffer.ToArray(), CurrentIndex);
                CurrentIndex += 4;
                return (T)Convert.ChangeType(value, typeof(T));
            }
            if (type == typeof(double))
            {
                double value = BitConverter.ToDouble(Packet.Buffer.ToArray(), CurrentIndex);
                CurrentIndex += 8;
                return (T)Convert.ChangeType(value, typeof(T));
            }
            if (type == typeof(long))
            {
                long value = BitConverter.ToInt64(Packet.Buffer.ToArray(), CurrentIndex);
                CurrentIndex += 8;
                return (T)Convert.ChangeType(value, typeof(T));
            }

            return (T)Convert.ChangeType(null, typeof(T));
        }

        public static string Decompress(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }
    }
}
