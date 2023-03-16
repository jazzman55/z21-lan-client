using System;
using System.Collections;

namespace Z21LanClient.Extensions
{
    public static class MessageExtension
    {
        public static ArrayList SplitMessages(this byte[] message)
        {
            var list = new ArrayList();
            int i = 0;
            while (i < message.Length)
            {
                var len = message[i];
                if (len < 4 || i + len > message.Length)
                {
                    //invalid message
                    throw new ArgumentException("Invalid message length", nameof(message));
                }

                var m = new byte[len];
                Array.Copy(message, i, m, 0, len);
                list.Add(m);
                i += len;
            }
            return list;
        }

        public static byte Checksum(this byte[] bytes, int startIndex = 4, int endIndex = -1)
        {
            if (endIndex == -1)
                endIndex = bytes.Length - 2; //assuming one before last byte

            byte result = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                result ^= bytes[i];
            }
            return result;
        }

        public static void SetChecksum(this byte[] bytes, int startIndex = 4, int endIndex = -1)
        {
            // ReSharper disable once UseIndexFromEndExpression
            bytes[bytes.Length - 1] = Checksum(bytes, startIndex, endIndex);
        }

        public static void SetAddress(this byte[] bytes, int address, int index)
        {
            bytes[index] = (byte)(address < 0x80 ? address >> 8 : (address >> 8) | 0xC0);
            bytes[index + 1] = (byte)address;
        }

        public static int GetAddress(this byte[] bytes, int index) => ((bytes[index] & 0x3F) << 8) + bytes[index + 1];
    }
}
