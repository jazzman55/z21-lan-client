namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_X_SET_TRACK_POWER_OFF
    /// </summary>
    public class SetTrackPowerOff : ICommand
    {
        public byte[] Bytes => new byte[] { 0x07, 0x00, 0x40, 0x00, 0x21, 0x80, 0xA1 };
    }
}