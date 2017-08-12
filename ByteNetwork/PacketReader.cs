using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteNetwork
{
    public class PacketReader
    {
        Packet Packet;

        int CurrentIndex;

        public PacketReader(Packet packet)
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
                var value = Encoding.UTF8.GetString(string_bytes);
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
    }
}
