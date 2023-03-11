using System.Net;
using System.Net.Sockets;
using Z21LanClient.Interface;

namespace Z21.Core
{
    public class CoreUdpClient : IUdpClient
    {
        private readonly UdpClient _client;

        public ReceivedCallbackDelegate? ReceivedCallback { get; set; }
        
        public CoreUdpClient()
        {
            _client = new UdpClient();
        }
        
        public void Dispose()
        {
            _client.Dispose();
        }

        public void Connect(string host, int port)
        {
            _client.Connect(host, port);
            _client.BeginReceive(OnReceived, null);
        }

        public void Close()
        {
            _client.Close();
        }

        public void Send(byte[] bytes, int length)
        {
            _client.Send(bytes, length);
        }

        private void OnReceived(IAsyncResult res)
        {
            IPEndPoint remoteIpEndPoint = null!;
            byte[] data = _client.EndReceive(res, ref remoteIpEndPoint!);
            _client.BeginReceive(OnReceived, null);
            ReceivedCallback?.Invoke(data);
        }

    }
}
