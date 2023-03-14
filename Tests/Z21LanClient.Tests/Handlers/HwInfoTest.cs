using Z21LanClient.Handlers;

namespace Z21LanClient.Tests.Handlers
{
    public class HwInfoTest
    {
        [Fact]
        public void Handle_should_invoke_hw_ver_strings()
        {

            var data = new byte[] { 0x0C, 0x00, 0x1A, 0x00, 0x04, 0x02, 0x00, 0x00, 0x20, 0x01, 0x00, 0x00};

            HwInfoEventArgs? args = null;
            var sut = new HwInfo((s, e) =>
            {
                args = (HwInfoEventArgs)e;
            });
            var result = sut.Handle(data);

            Assert.True(result);
            Assert.Equal("Z21 START", args?.HardwareType);
            Assert.Equal("1.20", args?.FirmwareVersion);
        }
    }
}
