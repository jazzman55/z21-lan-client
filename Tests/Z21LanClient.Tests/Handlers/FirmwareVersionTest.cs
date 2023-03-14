using Z21LanClient.Handlers;

namespace Z21LanClient.Tests.Handlers
{
    public class FirmwareVersionTest
    {
        [Fact]
        public void Handle_should_invoke_version_string()
        {
            var data = new byte[] { 0x09, 0x00, 0x40, 0x00, 0xF3, 0x0A, 0x01, 0x23, 0x28};

            FirmwareVersionEventArgs? args = null;
            var sut = new FirmwareVersion((s, e) =>
            {
                args = (FirmwareVersionEventArgs)e;
            });
            var result = sut.Handle(data);
            
            Assert.True(result);
            Assert.Equal("1.23", args?.FirmwareVersion);
        }
    }
}
