using Z21LanClient.Model;

namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_X_SET_LOCO_DRIVE
    /// </summary>
    public class SetLocoDrive : ICommand
    {
        public byte[] Bytes { get; }

        public SetLocoDrive(int address, Direction direction, int speed, SpeedSteps speedSteps = SpeedSteps.DCC_128)
        {
            Bytes = new byte[10] { 0x0A, 0x00, 0x40, 0x00, 0xE4, 0x00, 0x00, 0x00, 0x00, 0x00 };

            Bytes[5] = (byte)(0x10 + (ushort)speedSteps);

            Helpers.CopyAddress(address, Bytes, 6);

            Bytes[8] = (byte)(speed | (direction == Direction.Forward ? 0b10000000 : 0));

            Bytes[9] = Helpers.Checksum(Bytes, 4, 8);
        }
    }
}