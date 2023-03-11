using System;

namespace Z21LanClient.Handlers
{
    public class TrackPowerOff :IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public TrackPowerOff(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!Helpers.BytesEqual(message, new byte[] {0x40, 0x00, 0x61, 0x00}, 2))
                return false;

            _messageEventHandler?.Invoke(this, EventArgs.Empty);

            return true;
        }
    }
}