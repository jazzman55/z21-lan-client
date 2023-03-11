using System;
using System.Collections;
using System.Threading;
using Microsoft.Extensions.Logging;
using Z21LanClient.Commands;
using Z21LanClient.Handlers;
using Z21LanClient.Interface;

namespace Z21LanClient
{
    public class Z21CommandStation : IDisposable
    {
        private readonly IUdpClient _udpClient;
        private readonly ILogger _logger;

        private readonly IHandler[] _handlers;

        private readonly Timer _renewSubscriptionTimer;

        private readonly ICommand _getStatusCommand = new GetStatus();

        private readonly TimeSpan _renewSubscriptionInterval = TimeSpan.FromSeconds(50);

        public event EventHandler? FirmwareVersionReceived;
        public event EventHandler? HwInfoReceived;
        public event EventHandler? LocoInfoReceived;
        public event EventHandler? ProgrammingModeReceived;
        public event EventHandler? SerialNumberReceived;
        public event EventHandler? StatusChangedReceived;
        public event EventHandler? StoppedReceived;
        public event EventHandler? SystemStateChangedReceived;
        public event EventHandler? TrackPowerOffReceived;
        public event EventHandler? TrackPowerOnReceived;
        public event EventHandler? TrackShortCircuitReceived;
        public event EventHandler? VersionReceived;

        public Z21CommandStation(IUdpClient udpClient, ILogger logger)
        {
            _udpClient = udpClient;
            _logger = logger;

            _handlers = new IHandler[]
            {
                new FirmwareVersion((s, a) => FirmwareVersionReceived?.Invoke(s, a)),
                new HwInfo((s, a) => HwInfoReceived?.Invoke(s, a)),
                new LocoInfo((s, a) => LocoInfoReceived?.Invoke(s, a)),
                new ProgrammingMode((s, a) => ProgrammingModeReceived?.Invoke(s, a)),
                new SerialNumber((s, a) => SerialNumberReceived?.Invoke(s, a)),
                new StatusChanged((s, a) => StatusChangedReceived?.Invoke(s, a)),
                new Stopped((s, a) => StoppedReceived?.Invoke(s, a)),
                new SystemStateChanged((s, a) => SystemStateChangedReceived?.Invoke(s, a)),
                new TrackPowerOff((s, a) => TrackPowerOffReceived?.Invoke(s, a)),
                new TrackPowerOn((s, a) => TrackPowerOnReceived?.Invoke(s, a)),
                new TrackShortCircuit((s, a) => TrackShortCircuitReceived?.Invoke(s, a)),
                new Handlers.Version((s, a) => VersionReceived?.Invoke(s, a))
            };

            _renewSubscriptionTimer = new Timer(_ => Send(_getStatusCommand), null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        }

        public void Dispose()
        {
            _renewSubscriptionTimer.Dispose();
            _udpClient.Dispose();
        }

        public void Connect(string host, int port = 21105)
        {
            _udpClient.Connect(host, port);
            _udpClient.ReceivedCallback = ReceivedCallback;

            _renewSubscriptionTimer.Change(TimeSpan.Zero, _renewSubscriptionInterval);
        }

        private void ReceivedCallback(byte[] data)
        {
            var messages = SplitMessages(data);
            foreach (byte[] message in messages)
            {
                ProcessMessage(message);
            }
        }

        public void Send(ICommand command)
        {
            _udpClient.Send(command.Bytes, command.Bytes.Length);
        }

        private void ProcessMessage(byte[] message)
        {
            foreach (var handler in _handlers)
            {
                if (handler.Handle(message))
                    break;
            }
        }

        public static ArrayList SplitMessages(byte[] message)
        {
            var list = new ArrayList();
            int i = 0;
            while (i < message.Length)
            {
                var len = message[i];
                if (len < 4 || i + len > message.Length)
                {
                    //invalid message, skip
                    break;
                }

                var m = new byte[len];
                Array.Copy(message, i, m, 0, len);
                list.Add(m);
                i += len;
            }
            return list;
        }
    }
}
