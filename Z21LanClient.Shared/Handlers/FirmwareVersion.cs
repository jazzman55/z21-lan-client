using System;

namespace Z21LanClient.Handlers
{
    public class FirmwareVersion : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public FirmwareVersion(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!Helpers.BytesEqual(message, new byte[]{0x40, 0x00, 0xF3}, 2))
                return false;

            _messageEventHandler?.Invoke(this, new FirmwareVersionEventArgs(
                $"{Helpers.BcdToInt(message[6])}.{Helpers.BcdToInt(message[7])}"));

            return true;
        }
    }
}