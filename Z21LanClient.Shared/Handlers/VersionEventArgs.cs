using System;

namespace Z21LanClient.Handlers
{
    public class VersionEventArgs : EventArgs
    {
        public VersionEventArgs(string xBusVersion, string commandStationId)
        {
            XBusVersion = xBusVersion;
            CommandStationId = commandStationId;
        }

        public string XBusVersion { get;}
        public string CommandStationId { get; }
    }
}