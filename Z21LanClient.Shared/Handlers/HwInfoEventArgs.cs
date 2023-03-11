using System;
namespace Z21LanClient.Handlers
{
    public class HwInfoEventArgs : EventArgs
    {
        public HwInfoEventArgs(string hardwareType, string firmwareVersion)
        {
            HardwareType = hardwareType;
            FirmwareVersion = firmwareVersion;
        }

        public string HardwareType { get; }
        public string FirmwareVersion { get; }
    }
}