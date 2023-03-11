namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_SYSTEMSTATE_GETDATA
    /// </summary>
    public class SystemStateGetData : ICommand
    {
        public byte[] Bytes => new byte[] { 0x04, 0x00, 0x85, 0x00 };
    }
}