using Z21LanClient.Commands;
using Z21LanClient.Model;

namespace Z21LanClient.Tests.Commands
{
    public class SetLocoFunctionTest
    {
        [Theory]
        [InlineData(0x03, FunctionToggle.Off, 0x03)]
        [InlineData(0x03, FunctionToggle.On, 0x43)]
        [InlineData(0x03, FunctionToggle.Toggle, 0x83)]
        [InlineData(0x10, FunctionToggle.Toggle, 0x90)]
        public void SetLocoFunction_should_set_function_byte(int function, FunctionToggle toggle, byte b)
        {
            Assert.Equal(b, new SetLocoFunction(3, function, toggle).Bytes[8]);
        }
    }
}
