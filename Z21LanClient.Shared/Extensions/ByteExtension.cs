using System;

namespace Z21LanClient.Extensions
{
    public static class ByteExtension
    {
        public static int BcdToInt(this byte b)
        {
            return (b >> 4) * 10 + (b & 15);
        }

        public static bool IsBitEnabled(this byte b, int position)
        {
            return ((b >> position) & 1) == 1;
        }
    }
}
