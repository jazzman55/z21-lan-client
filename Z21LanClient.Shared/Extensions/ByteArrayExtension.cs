
using System.Text;

namespace Z21LanClient.Extensions
{
    public static class ByteArrayExtension
    {
        public static string ToHexString(this byte[] bytes)
        {
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append($"{b:X2} ");
            }
            return builder.ToString().Trim();
        }

        public static bool FragmentsEqual(this byte[] bytes, byte[] fragment, int startIndex)
        {
            for (int i = 0; i < fragment.Length; i++)
            {
                if (bytes[i + startIndex] != fragment[i])
                    return false;
            }
            return true;
        }

        public static bool FragmentsEqual(this byte[] bytes, byte b, int index)
        {
            return bytes[index] == b;
        }

        public static void BitsToBoolArray(this byte b, int startBit, int endBit, bool[] boolArray, int index)
        {
            for (int i = 0; i <= endBit - startBit; i++)
            {
                boolArray[index + i] = b.IsBitEnabled(i + startBit);
            }
        }
    }
}
