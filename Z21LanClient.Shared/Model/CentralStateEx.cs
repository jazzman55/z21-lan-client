using System;

namespace Z21LanClient.Model
{
    [Flags]
    public enum CentralStateEx : byte
    {
        HighTemperature = 0x01,
        PowerLost = 0x02,
        ShortCircuitExternal = 0x04,
        ShortCircuitInternal = 0x08,
        RCN213 = 0x20
    }
}
