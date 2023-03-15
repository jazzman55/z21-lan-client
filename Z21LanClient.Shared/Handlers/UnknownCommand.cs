using System;
using Z21LanClient.Extensions;

namespace Z21LanClient.Handlers
{
    /// <summary>
    /// LAN_X_UNKNOWN_COMMAND
    /// </summary>
    public class UnknownCommand : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public UnknownCommand(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!message.FragmentsEqual(new byte[] { 0x40, 0x00, 0x61, 0x82 }, 2))
                return false;

            _messageEventHandler?.Invoke(this, EventArgs.Empty);

            return true;
        }
    }
}