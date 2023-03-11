namespace Z21LanClient.Commands
{
    public class SetStop : ICommand
    {
        public byte[] Bytes => new byte[] { 0x06, 0x00, 0x40, 0x00, 0x80, 0x80 };
    }
}