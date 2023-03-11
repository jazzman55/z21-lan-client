namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_GET_CODE
    /// </summary>
    public class GetLanCode : ICommand
    {
        public byte[] Bytes => new byte[] { 0x04, 0x00, 0x18, 0x00 };
    }
}