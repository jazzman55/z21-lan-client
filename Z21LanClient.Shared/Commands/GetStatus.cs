namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_X_GET_STATUS
    /// </summary>
    public class GetStatus : ICommand
    {
        public byte[] Bytes => new byte[] { 0x07, 0x00, 0x40, 0x00, 0x21, 0x24, 0x05 };
    }
}