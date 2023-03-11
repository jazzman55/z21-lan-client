
namespace Z21LanClient.Handlers
{
    public interface IHandler
    {
        bool Handle(byte[] message);
    }
}