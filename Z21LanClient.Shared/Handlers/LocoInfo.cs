using System;
using Z21LanClient.Model;

namespace Z21LanClient.Handlers
{
    /// <summary>
    /// LAN_X_LOCO_INFO
    /// </summary>
    public class LocoInfo : IHandler
    {
        private readonly EventHandler _messageEventHandler;

        public LocoInfo(EventHandler messageEventHandler)
        {
            _messageEventHandler = messageEventHandler;
        }

        public bool Handle(byte[] message)
        {
            if (!Helpers.BytesEqual(message, new byte[] { 0x40, 0x00, 0xEF }, 2))
                return false;

            var dataLen = message[0];

            var speedSteps = (message[7] & 7) switch
            {
                4 => SpeedSteps.Dcc128,
                _ => (SpeedSteps)(message[7] & 7)
            };


            var speed = (message[8] & 127);
            speed = speed switch
            {
                0 => 0,
                1 => -1,
                _ => speed - 1
            };

            var args = new LocoInfoEventArgs(

                Helpers.GetAddress(message, 5),
                Helpers.BitEnabled(message[7], 3),
                speedSteps,
                Helpers.BitEnabled(message[8], 7) ? Direction.Forward : Direction.Backward,
                speed,
                 dataLen > 10 && Helpers.BitEnabled(message[9], 6),
                dataLen > 10 && Helpers.BitEnabled(message[9], 5),
                new bool[32]);

            int functionIndex = 0;
            for (int i = 9; i < dataLen - 1; i++) //last byte is the XOR checksum
            {
                switch (i)
                {
                    case 9:
                        args.Functions[0] = Helpers.BitEnabled(message[9], 4);
                        Helpers.BitsToBoolArray(message[9], 0, 3, args.Functions, 1);
                        functionIndex += 5;
                        continue;
                    case 13:
                        Helpers.BitsToBoolArray(message[13], 0, 2, args.Functions, functionIndex);
                        functionIndex += 3;
                        continue;
                    default:
                        Helpers.BitsToBoolArray(message[i], 0, 7, args.Functions,  functionIndex);
                        functionIndex += 8;
                        break;
                }
            }

            _messageEventHandler?.Invoke(this, args);

            return true;
        }
    }
}