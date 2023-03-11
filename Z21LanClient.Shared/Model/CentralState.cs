using System;

namespace Z21LanClient.Model
{
    [Flags]
    public enum CentralState : byte
    {
        EmergencyStop = 0x01,
        TrackVoltageOff = 0x02,
        ShortCircuit = 0x04,
        ProgrammingModeActive = 0x20
    }
}
