using System;

namespace Z21LanClient.Interface
{
    public interface IUdpClient : IDisposable
    {
        public delegate void ReceivedCallbackDelegate(byte[] data);

        ReceivedCallbackDelegate ReceivedCallback { get; set; }

        void Connect(string host, int port);
        void Close();
        void Send(byte[] bytes, int length);
    }
}