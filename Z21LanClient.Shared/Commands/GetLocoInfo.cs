namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_X_GET_LOCO_INFO
    /// </summary>
    public class GetLocoInfo : ICommand
    {
        public byte[] Bytes { get; }

        public GetLocoInfo(int address)
        {
            Bytes = new byte[] { 0x09, 0x00, 0x40, 0x00, 0xE3, 0xF0, 0x00, 0x00, 0x00 };

            var b = (byte)(address >> 8);
            if (address >= 128)
                b += 192;
            Bytes[6] = b;
            Bytes[7] = (byte)(address % 256);

            Bytes[8] = (byte)(Bytes[4] ^ Bytes[5] ^ Bytes[6] ^ Bytes[7]);
        }

    }
}