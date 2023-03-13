using System;
using Z21LanClient.Model;

namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_SET_BROADCASTFLAGS
    /// </summary>
    public class SetBroadcastFlags : ICommand
    {
        public byte[] Bytes { get; }

        public SetBroadcastFlags(BroadcastFlags flags)
        {
            Bytes = new byte[] { 0x08, 0x00, 0x50, 0x00, 0x00, 0x00, 0x00, 0x00 };

            BitConverter.GetBytes((int)flags).CopyTo(Bytes, 4);
        }
    }
}