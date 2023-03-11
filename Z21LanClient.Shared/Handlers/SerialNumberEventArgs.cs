using System;

namespace Z21LanClient.Handlers
{
    public class SerialNumberEventArgs : EventArgs
    {
        public int SerialNumber { get; }

        public SerialNumberEventArgs(int serialNumber)
        {
            SerialNumber = serialNumber;
        }
    }
}