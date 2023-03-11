namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_GET_HWINFO
    /// </summary>
    public class GetHardwareInfo : ICommand
    {
        public byte[] Bytes => new byte[] { 0x04, 0x00, 0x1A, 0x00 };
    }
}