using System;
using Z21LanClient.Extensions;

namespace Z21LanClient.Handlers
{
    /// <summary>
    /// LAN_X_GET_VERSION
    /// </summary>
    public class Version : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public Version(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!message.FragmentsEqual(new byte[] { 0x40, 0x00, 0x63, 0x21 }, 2))
                return false;

            var xbusVer = $"{message[6].BcdToInt():D2}";
            _messageEventHandler?.Invoke(this, new VersionEventArgs($"{xbusVer[0]}.{xbusVer[1]}", $"{message[7]:X2}"));

            return true;
        }
    }
}