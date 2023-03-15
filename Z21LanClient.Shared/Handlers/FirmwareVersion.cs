using System;
using Z21LanClient.Extensions;

namespace Z21LanClient.Handlers
{
    /// <summary>
    /// LAN_X_GET_FIRMWARE_VERSION
    /// </summary>
    public class FirmwareVersion : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public FirmwareVersion(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!message.FragmentsEqual(new byte[]{0x40, 0x00, 0xF3}, 2))
                return false;

            _messageEventHandler?.Invoke(this, new FirmwareVersionEventArgs(
                $"{message[6].BcdToInt()}.{message[7].BcdToInt()}"));

            return true;
        }
    }
}