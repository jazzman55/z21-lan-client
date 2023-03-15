
using System.Text;

namespace Z21LanClient.Shared.Extensions
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
    }
}
