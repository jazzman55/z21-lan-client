
namespace Z21LanClient.Handlers
{
    /// <summary>
    /// Interface for received message handlers
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Handles message, if it can.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>True, if message was handled; otherwise false.</returns>
        bool Handle(byte[] message);
    }
}