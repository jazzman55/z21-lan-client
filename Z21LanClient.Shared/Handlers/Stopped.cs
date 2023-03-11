using System;

namespace Z21LanClient.Handlers
{
    public class Stopped : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public Stopped(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!Helpers.BytesEqual(message, new byte[]{0x40, 0x00, 0x81}, 2))
                return false;

            _messageEventHandler?.Invoke(this, EventArgs.Empty);

            return true;
        }
    }
}