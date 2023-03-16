using System;
using System.Collections;
using System.Threading;
using Microsoft.Extensions.Logging;
using Z21LanClient.Commands;
using Z21LanClient.Handlers;
using Z21LanClient.Interface;
using Z21LanClient.Extensions;

namespace Z21LanClient
{
    public class Z21CommandStation : IDisposable
    {
        private readonly IUdpClient _udpClient;
        private readonly ILogger _logger;
        private readonly IHandler[] _handlers;
        private readonly IHandler[]? _customHandlers;
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
        public event EventHandler? UnknownCommandReceived;

        public Z21CommandStation(IUdpClient udpClient, ILogger logger, IHandler[]? customHandlers = null)
        {
            _udpClient = udpClient;
            _logger = logger;
            _customHandlers = customHandlers;

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
                new Handlers.Version((s, a) => VersionReceived?.Invoke(s, a)),
                new UnknownCommand((s, a) => UnknownCommandReceived?.Invoke(s, a))
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
            try
            {
                foreach (byte[] message in data.SplitMessages())
                {
                    ProcessMessage(message);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Invalid message", e);
            }
        }

        public void Send(ICommand command)
        {
            try
            {
                _udpClient.Send(command.Bytes, command.Bytes.Length);
                _logger.LogDebug($"Command {command.GetType().Name} sent");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error sending command {command.GetType().Name}");
                throw;
            }
        }

        private void ProcessMessage(byte[] message)
        {
            var handled = false;

            if (_customHandlers is not null && _customHandlers.Length > 0)
                handled = HandleMessage(message, _customHandlers);

            if (!handled)
                handled = HandleMessage(message, _handlers);

            if (!handled)
            {
                _logger.LogDebug($"Unhandled message: {message.ToHexString()}");
            }
        }

        private bool HandleMessage(byte[] message, IHandler[] handlers)
        {
            var handled = false;
            foreach (var handler in handlers)
            {
                try
                {
                    handled = handler.Handle(message);
                    if (handled)
                    {
                        _logger.LogDebug($"Message {handler.GetType().Name} received");
                        break;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Error processing message by {handler.GetType().Name}");
                }
            }

            return handled;
        }
    }
}
