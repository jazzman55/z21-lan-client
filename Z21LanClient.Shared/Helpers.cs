using System;

namespace Z21LanClient
{
    public static class Helpers
    {
        public static byte Checksum(byte[] bytes, int startIndex, int endIndex)
        {
            byte result = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                result ^= bytes[i];
            }
            return result;
        }

        public static void CopyAddress(int address, byte[] bytes, int index)
        {
            BitConverter.GetBytes((UInt16)address).CopyTo(bytes, index);
        }

        public static bool BytesEqual(byte[] bytes1, byte[] bytes2, int startIndex)
        {
            for (int i = 0; i < bytes2.Length; i++)
            {
                if (bytes1[i + startIndex] != bytes2[i])
                    return false;
            }
            return true;
        }

        public static bool BytesEqual(byte[] bytes, byte b, int index)
        {
            return bytes[index] == b;
        }

        public static int BcdToInt(byte b)
        {
            return (b >> 4) * 10 + (b & 15);
        }

        public static bool BitEnabled(byte b, int position)
        {
            return ((b >> position) & 1) == 1;
        }

        public static void BitsToBoolArray(byte b, int startBit, int endBit, bool[] boolArray, int index)
        {
            for (int i = 0; i <= endBit - startBit; i++)
            {
                boolArray[index + i] = BitEnabled(b, i + startBit);
            }
        }

        
    }
}
