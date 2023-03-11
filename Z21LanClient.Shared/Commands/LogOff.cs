namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_LOGOFF
    /// </summary>
    public class LogOff : ICommand
    {
        public byte[] Bytes => new byte[] { 0x04, 0x00, 0x30, 0x00 };
    }
}