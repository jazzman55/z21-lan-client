namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_X_SET_STOP
    /// </summary>
    public class SetStop : ICommand
    {
        public byte[] Bytes => new byte[] { 0x06, 0x00, 0x40, 0x00, 0x80, 0x80 };
    }
}