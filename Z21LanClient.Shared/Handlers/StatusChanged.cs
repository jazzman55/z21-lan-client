using System;
using Z21LanClient.Extensions;
using Z21LanClient.Model;

namespace Z21LanClient.Handlers
{
    /// <summary>
    /// LAN_X_STATUS_CHANGED
    /// </summary>
    public class StatusChanged : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public StatusChanged(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!message.FragmentsEqual(new byte[] { 0x40, 0x00, 0x62, 0x22 }, 2))
                return false;

            _messageEventHandler?.Invoke(this, new StatusChangedEventArgs((CentralState)message[6]));

            return true;
        }
    }
}