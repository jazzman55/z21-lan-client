namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_X_GET_FIRMWARE_VERSION
    /// </summary>
    public class GetFirmwareVersion : ICommand
    {
        public byte[] Bytes => new byte[] { 0x07, 0x00, 0x40, 0x00, 0xF1, 0x0A, 0xFB };
    }
}