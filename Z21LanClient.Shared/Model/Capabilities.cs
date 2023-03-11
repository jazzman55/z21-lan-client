using System;

namespace Z21LanClient.Model
{
    [Flags]
    public enum Capabilities : byte
    {
        DCC = 0x01,
        MM = 0x02,
        Reserved = 0x04,
        RailCom = 0x08,
        LocoCmds = 0x10,
        AccessoryCmds = 0x20,
        DetectorCmds = 0x40,
        NeedsUnlockCode = 0x80
    }
}
