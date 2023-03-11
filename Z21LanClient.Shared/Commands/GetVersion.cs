namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_X_GET_VERSION
    /// </summary>
    public class GetVersion : ICommand
    {
        public byte[] Bytes => new byte[] { 0x07, 0x00, 0x40, 0x00, 0x21, 0x21, 0x00 };
    }
}