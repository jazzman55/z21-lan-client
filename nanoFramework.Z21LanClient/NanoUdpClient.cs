using System.Net;
using System.Net.Sockets;
using System.Threading;
using Z21LanClient.Interface;

namespace Z21.Device
{
    public class NanoUdpClient : IUdpClient
    {
        private readonly UdpClient _client;
        private Thread _receiveThread;
        private readonly byte[] _buffer = new byte[1024];

        public ReceivedCallbackDelegate? ReceivedCallback { get; set; }

        public NanoUdpClient()
        {
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
            try
            {
                IPEndPoint remoteIpEndPoint = null!;
                var length = _client.Receive(_buffer, ref remoteIpEndPoint);

                _receiveThread = new Thread(Received);
                _receiveThread.Start();

                var data = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    data[i] = _buffer[i];
                }
                ReceivedCallback?.Invoke(data);
            }
            catch (ThreadAbortException) { }
        }

        public void Close()
        {
            _client.Close();
        }

        public void Send(byte[] bytes, int length)
        {
            _client.Send(bytes, 0, length);
        }
    }
}
