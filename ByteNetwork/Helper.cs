using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteNetwork
{
    public static class Helper
    {
        public static byte[] FromStringUTF8(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static byte[] FromStringASCII(string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }

        public static string FromByteUTF8(byte[] text)
        {
            return Encoding.UTF8.GetString(text);
        }

        public static string FromByteASCII(byte[] text)
        {
            return Encoding.ASCII.GetString(text);
        }

        // Append the string after the given skip number.
        public static byte[] AppendText(int skip, string text, Encoding encoding)
        {
            byte[] buffer = new byte[text.Length + skip];
            var bytes = encoding.GetBytes(text);
            for (int i = 0; i < bytes.Length; i++)
            {
                buffer[i + skip] = bytes[i];
            }
            return buffer;
        }

        public static byte[] FromInt(int number)
        {
            return BitConverter.GetBytes(number);
        }

        public static int FromBytes(byte[] number)
        {
            return BitConverter.ToInt32(number, 0);
        }
    }
}
