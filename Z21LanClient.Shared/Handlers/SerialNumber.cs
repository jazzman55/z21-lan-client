
using System;
using Z21LanClient.Extensions;

namespace Z21LanClient.Handlers
{
    /// <summary>
    /// LAN_GET_SERIAL_NUMBER
    /// </summary>
    public class SerialNumber : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public SerialNumber(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!message.FragmentsEqual(0x10, 2))
                return false;

            _messageEventHandler?.Invoke(this, new SerialNumberEventArgs(BitConverter.ToInt32(message, 4)));

            return true;
        }
    }
}