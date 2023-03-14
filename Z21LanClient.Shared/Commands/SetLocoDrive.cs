using System;
using Z21LanClient.Model;

namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_X_SET_LOCO_DRIVE
    /// </summary>
    public class SetLocoDrive : ICommand
    {
        public byte[] Bytes { get; }

        /// <summary>
        /// Creates new instance of SetLocoDrive command.
        /// </summary>
        /// <param name="address">Loco address</param>
        /// <param name="direction">Driving direction</param>
        /// <param name="speed">Speed DCC128: [0-126] / DCC14: [0-14] / E-Stop: -1 </param>
        /// <param name="speedSteps">DCC128 / DCC14. DCC28 not currently supported </param>
        public SetLocoDrive(int address, Direction direction, int speed, SpeedSteps speedSteps = SpeedSteps.Dcc128)
        {
            switch (speedSteps)
            {
                case SpeedSteps.Dcc14:
                    if (speed > 14) throw new ArgumentOutOfRangeException(nameof(speed)); break;
                case SpeedSteps.Dcc128:
                    if (speed > 126) throw new ArgumentOutOfRangeException(nameof(speed)); break;
                case SpeedSteps.Dcc28:
                    throw new NotSupportedException("DCC28 not currently supported");
            }

            Bytes = new byte[] { 0x0A, 0x00, 0x40, 0x00, 0xE4, 0x00, 0x00, 0x00, 0x00, 0x00 };

            Bytes[5] = (byte)(0x10 | (ushort)speedSteps);

            Helpers.SetAddress(address, Bytes, 6);

            speed = speed switch
            {
                -1 => 1, //E-Stop
                0 => 0, // Stop
                _ => speed + 1
            };

            Bytes[8] = (byte)(speed | (direction == Direction.Forward ? 0b10000000 : 0));

            Bytes[9] = Helpers.Checksum(Bytes);
        }
    }
}