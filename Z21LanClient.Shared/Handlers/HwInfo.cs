
using System;
using Z21LanClient.Extensions;

namespace Z21LanClient.Handlers
{
    /// <summary>
    /// LAN_GET_HWINFO
    /// </summary>
    public class HwInfo : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public HwInfo(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!message.FragmentsEqual(0x1A, 2))
                return false;

            var hwType = BitConverter.ToUInt32(message, 4) switch
            {
                0x00000200 => "Z21 OLD (black)",
                0x00000201 => "Z21 NEW (black)",
                0x00000202 => "SmartRail",
                0x00000203 => "Z21 SMALL (white)",
                0x00000204 => "Z21 START",
                _ => "UNKNOWN"
            };

            var fwVersion = $"{message[9].BcdToInt()}.{message[8].BcdToInt()}";

            _messageEventHandler?.Invoke(this, new HwInfoEventArgs(hwType, fwVersion));

            return true;
        }
    }
}