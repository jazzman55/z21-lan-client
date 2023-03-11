using System;

namespace Z21LanClient.Interface
{
    public delegate void ReceivedCallbackDelegate(byte[] data);

    public interface IUdpClient : IDisposable
    {
        ReceivedCallbackDelegate? ReceivedCallback { get; set; }

        void Connect(string host, int port);
        void Close();
        void Send(byte[] bytes, int length);
    }
}