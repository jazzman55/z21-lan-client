using System;
using Z21LanClient.Extensions;
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
            if (!message.FragmentsEqual(new byte[] { 0x40, 0x00, 0xEF }, 2))
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

                message.GetAddress(5),
                message[7].IsBitEnabled(3),
                speedSteps,
                message[8].IsBitEnabled(7) ? Direction.Forward : Direction.Backward,
                speed,
                 dataLen > 10 && message[9].IsBitEnabled(6),
                dataLen > 10 && message[9].IsBitEnabled(5),
                new bool[32]);

            int functionIndex = 0;
            for (int i = 9; i < dataLen - 1; i++) //last byte is the XOR checksum
            {
                switch (i)
                {
                    case 9:
                        args.Functions[0] = message[9].IsBitEnabled(4);
                        message[9].BitsToBoolArray(0, 3, args.Functions, 1);
                        functionIndex += 5;
                        continue;
                    case 13:
                        message[13].BitsToBoolArray(0, 2, args.Functions, functionIndex);
                        functionIndex += 3;
                        continue;
                    default:
                        message[i].BitsToBoolArray(0, 7, args.Functions,  functionIndex);
                        functionIndex += 8;
                        break;
                }
            }

            _messageEventHandler?.Invoke(this, args);

            return true;
        }
    }
}