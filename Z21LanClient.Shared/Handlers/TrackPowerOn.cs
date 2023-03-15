using System;
using Z21LanClient.Extensions;

namespace Z21LanClient.Handlers
{
    /// <summary>
    /// LAN_X_BC_TRACK_POWER_ON
    /// </summary>
    public class TrackPowerOn :IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public TrackPowerOn(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!message.FragmentsEqual(new byte[] {0x40, 0x00, 0x61, 0x01}, 2))
                return false;

            _messageEventHandler?.Invoke(this, EventArgs.Empty);

            return true;
        }
    }
}