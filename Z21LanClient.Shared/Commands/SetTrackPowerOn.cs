namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_X_SET_TRACK_POWER_ON
    /// </summary>
    public class SetTrackPowerOn : ICommand
    {
        public byte[] Bytes => new byte[] { 0x07, 0x00, 0x40, 0x00, 0x21, 0x81, 0xA0 };
    }
}