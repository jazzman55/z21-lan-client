using System;
using Z21LanClient.Model;

namespace Z21LanClient.Handlers
{
    public class StatusChanged : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public StatusChanged(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!Helpers.BytesEqual(message, new byte[] { 0x40, 0x00, 0x62, 0x22 }, 2))
                return false;

            _messageEventHandler?.Invoke(this, new StatusChangedEventArgs((CentralState)message[6]));

            return true;
        }
    }
}