namespace nanoFramework.Z21LanClient.Extensions
{
    public static class ByteArrayExtension
    {
        public static byte[] GetFragment(this byte[] b, int index, int length)
        {
            var fragment = new byte[length];
            for (int i = 0; i < length; i++)
            {
                fragment[i] = b[i + index];
            }

            return fragment;
        }
    }
}