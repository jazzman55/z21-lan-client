using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Extensions.Logging;
using nanoFramework.Z21LanClient.Extensions;
using Z21LanClient.Interface;

namespace nanoFramework.Z21LanClient
{
    public class NanoUdpClient : IUdpClient
    {
        private readonly UdpClient _client;
        private Thread _receiveThread;
        private readonly byte[] _buffer = new byte[1024];
        private readonly ILogger _logger;

        public ReceivedCallbackDelegate ReceivedCallback { get; set; }

        public NanoUdpClient(ILogger logger)
        {
            _logger = logger;
            _client = new UdpClient();
        }

        public void Dispose()
        {
            _receiveThread?.Abort();
            _client.Dispose();
        }

        public void Connect(string host, int port)
        {
            _client.Connect(host, port);

            _receiveThread = new Thread(Received);
            _receiveThread.Start();
        }

        private void Received()
        {
            IPEndPoint remoteIpEndPoint = null!;

            while (true)
            {
                try
                {
                    var length = _client.Receive(_buffer, ref remoteIpEndPoint);
                    
                    ReceivedCallback?.Invoke(_buffer.GetFragment(0, length));
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Receiving error");
                }

                Thread.Sleep(1);
            }
        }

        public void Close()
        {
            _receiveThread?.Abort();
            _receiveThread = null!;
            _client.Close();
        }

        public void Send(byte[] bytes, int length)
        {
            _client.Send(bytes, 0, length);
        }
    }
}
