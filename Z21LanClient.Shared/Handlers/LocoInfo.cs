using System;
using Z21LanClient.Model;

namespace Z21LanClient.Handlers
{
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

            var args = new LocoInfoEventArgs(

                ((message[5] & 0x3F) << 8) + message[6],
                Helpers.BitEnabled(message[7], 3),
                (SpeedSteps)(message[8] & 7),
                Helpers.BitEnabled(message[8], 7) ? Direction.Forward : Direction.Backward,
                (message[8] & 127),
                Helpers.BitEnabled(message[9], 6),
                Helpers.BitEnabled(message[9], 5),
                new bool[32]);

            int functionIndex = 0;
            for (int i = 9; i < dataLen - 1; i++) //last byte is the XOR checksum
            {
                if (i == 9)
                {
                    args.Functions[0] = Helpers.BitEnabled(message[9], 4);
                    Helpers.BitsToBoolArray(message[9], 0, 3, args.Functions, 1);
                    functionIndex += 5;
                    continue;
                }

                if (i == 13)
                {
                    Helpers.BitsToBoolArray(message[13], 0, 2, args.Functions, functionIndex);
                    functionIndex += 3;
                    continue;
                }

                Helpers.BitsToBoolArray(message[i], 0, 7, args.Functions,  functionIndex);
                functionIndex += 8;
            }

            _messageEventHandler?.Invoke(this, args);

            return true;
        }
    }
}