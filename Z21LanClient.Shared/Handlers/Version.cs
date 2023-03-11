using System;

namespace Z21LanClient.Handlers
{
    public class Version : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public Version(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!Helpers.BytesEqual(message, new byte[] { 0x40, 0x00, 0x63, 0x21 }, 2))
                return false;

            _messageEventHandler?.Invoke(this, new VersionEventArgs($"{Helpers.BcdToInt(message[6]):D2}", $"{message[7]:x2}"));

            return true;
        }
    }
}