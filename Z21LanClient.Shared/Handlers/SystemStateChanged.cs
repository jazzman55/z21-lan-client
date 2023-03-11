using System;
using Z21LanClient.Model;

namespace Z21LanClient.Handlers
{
    public class SystemStateChanged : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public SystemStateChanged(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!Helpers.BytesEqual(message, 0x84, 2))
                return false;

            _messageEventHandler?.Invoke(this, new SystemStateChangedEventArgs(
                BitConverter.ToInt16(message, 4),
                BitConverter.ToInt16(message, 6),
                BitConverter.ToInt16(message, 8),
                BitConverter.ToInt16(message, 10),
                BitConverter.ToUInt16(message, 12),
                BitConverter.ToUInt16(message, 14),
                (CentralState)message[16],
                (CentralStateEx)message[17],
                (Capabilities)message[19]));

            return true;
        }
    }
}