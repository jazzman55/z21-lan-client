using System;

namespace Z21LanClient.Model
{
    /// <summary>
    /// LAN_SET_BROADCASTFLAGS
    /// </summary>
    [Flags]
    public enum BroadcastFlags
    {
        DrivingAndSwitching = 0x00000001,
        RmBus = 0x00000002,
        RailComSubscribedLocos = 0x00000004,
        SystemStatus = 0x00000100,
        LocoInfo = 0x00010000,
        LocoNetOther = 0x01000000,
        LocoNetLocos = 0x02000000,
        LocoNetSwitches = 0x04000000,
        LocoNetDetector = 0x08000000,
        RailComAllLocos = 0x00040000,
        CanBusDetector = 0x00080000,
        CanBusBooster = 0x00020000
    }
}
