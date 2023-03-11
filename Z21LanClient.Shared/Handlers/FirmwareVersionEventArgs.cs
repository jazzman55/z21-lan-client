using System;

namespace Z21LanClient.Handlers
{
    public class FirmwareVersionEventArgs : EventArgs
    {
        public FirmwareVersionEventArgs(string firmwareVersion)
        {
            FirmwareVersion = firmwareVersion;
        }

        public string FirmwareVersion { get; }
    }
}