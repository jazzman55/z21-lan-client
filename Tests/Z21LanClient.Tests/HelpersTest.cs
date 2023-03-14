using Xunit.Abstractions;

namespace Z21LanClient.Tests
{
    public class HelpersTest
    {
        private readonly ITestOutputHelper _output;

        public HelpersTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(3, 0, 3)]
        [InlineData(127, 0, 127)]
        [InlineData(128, 192, 128)]
        [InlineData(255, 192, 255)]
        [InlineData(256, 193, 0)]
        [InlineData(1000, 195, 232)]
        [InlineData(9999, 231, 15)]
        public void SetAddress_should_set_MSB_and_LSB_to_given_array(int address, byte msb, byte lsb)
        {
            var bytes = new byte[5];

            Helpers.SetAddress(address, bytes, 2);

            Assert.Equal(msb, bytes[2]);
            Assert.Equal(lsb, bytes[3]);
        }

        [Theory]
        [InlineData(0x01, 1)]
        [InlineData(0x11, 11)]
        [InlineData(0x85, 85)]
        [InlineData(0x00, 00)]
        [InlineData(0x99, 99)]
        public void BcdToInt_should_return_digits(byte b, int result)
        {
            Assert.Equal(result, Helpers.BcdToInt(b));
        }

        [Theory]
        [InlineData(new byte[] {0x01, 0x22, 0x80, 0xF4}, 0, 3, 0x57)]
        [InlineData(new byte[] {0x01, 0x22, 0x80, 0xF4}, 2, 3, 0x74)]
        [InlineData(new byte[] {0x01, 0x22, 0x80, 0xF4}, 0, -1, 0xA3)]
        [InlineData(new byte[] {0x03, 0x55, 0x67, 0x00, 0x01, 0x22, 0x80, 0xF4}, 0, -1, 0x92)]
        public void Checksum_should_return_XOR_of_bytes(byte[] bytes, int startIndex, int endIndex, byte result)
        {
            Assert.Equal(result, Helpers.Checksum(bytes, startIndex, endIndex));
        }

        [Fact]
        public void Checksum_given_default_index_should_calculate_from_4_to_Length_minus_2()
        {
            Assert.Equal(0xA3, Helpers.Checksum(new byte[] { 0x03, 0x55, 0x67, 0x00, 0x01, 0x22, 0x80, 0xF4 }));
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(7, true)]
        public void BitEnabled_should_return_bool(int position, bool result)
        {
            Assert.Equal(result, Helpers.BitEnabled(0b10011010, position));
        }

        [Fact]
        public void BitsToBoolArray_should_return_array()
        {
            bool[] a = new bool[10];
            Helpers.BitsToBoolArray(0b10011010, 1, 3, a, 3);

            Assert.True(a[3]);
            Assert.False(a[4]);
            Assert.True(a[5]);
        }
    }
}