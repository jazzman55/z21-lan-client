using Z21LanClient.Model;

namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_X_SET_LOCO_FUNCTION
    /// </summary>
    public class SetLocoFunction : ICommand
    {
        public byte[] Bytes { get; }

        public SetLocoFunction(int address, int function, FunctionToggle toggle)
        {
            Bytes = new byte[10] {0x0A, 0x00, 0x40, 0x00, 0xE4, 0xF8, 0x00, 0x00, 0x00, 0x00 };

            Helpers.CopyAddress(address, Bytes, 6);

            Bytes[8] = (byte)(function | ((byte)toggle << 6));
            Bytes[9] = Helpers.Checksum(Bytes, 4, 8);
        }
    }
}