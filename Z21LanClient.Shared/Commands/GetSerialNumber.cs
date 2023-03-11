namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_GET_SERIAL_NUMBER
    /// </summary>
    public class GetSerialNumber : ICommand
    {
        public byte[] Bytes => new byte[] { 0x04, 0x00, 0x10, 0x00 };
    }
}