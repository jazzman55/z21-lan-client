
using System;

namespace Z21LanClient.Handlers
{
    public class SerialNumber : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public SerialNumber(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!Helpers.BytesEqual(message, 0x10, 2))
                return false;

            _messageEventHandler?.Invoke(this, new SerialNumberEventArgs(BitConverter.ToInt32(message, 4)));

            return true;
        }
    }
}