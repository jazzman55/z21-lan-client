using Z21LanClient.Commands;
using Z21LanClient.Model;

namespace Z21LanClient.Tests.Commands
{
    public class SetBroadcastFlagsTest
    {
        [Theory]
        [InlineData(BroadcastFlags.DrivingAndSwitching, new byte[] { 0x01, 0x00, 0x00, 0x00 })]
        [InlineData(BroadcastFlags.DrivingAndSwitching | BroadcastFlags.SystemStatus, new byte[] { 0x01, 0x01, 0x00, 0x00 })]
        [InlineData(BroadcastFlags.DrivingAndSwitching | BroadcastFlags.LocoInfo, new byte[] { 0x01, 0x00, 0x01, 0x00 })]
        [InlineData(BroadcastFlags.DrivingAndSwitching | BroadcastFlags.LocoInfo | BroadcastFlags.LocoNetLocos, new byte[] { 0x01, 0x00, 0x01, 0x02 })]
        public void SetBroadcastFlags_should_set_ORed_flags_into_bytes(BroadcastFlags flags, byte[] bytes)
        {
            var result = new SetBroadcastFlags(flags).Bytes;
            for (int i = 0; i < 4; i++)
            {
                Assert.Equal(bytes[i], result[i + 4]);
            }
        }
    }
}
