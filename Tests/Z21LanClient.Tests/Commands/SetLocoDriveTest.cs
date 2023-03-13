using Z21LanClient.Commands;
using Z21LanClient.Model;

namespace Z21LanClient.Tests.Commands
{
    public class SetLocoDriveTest
    {
        [Theory]
        [InlineData(Direction.Forward)]
        [InlineData(Direction.Backward)]
        public void SetLocoDrive_should_output_direction_bit(Direction direction)
        {
            var result = new SetLocoDrive(3, direction, 30).Bytes;
            Assert.Equal(direction == Direction.Forward ? 0x80 : 0x00, result[8] & 0x80);
        }

        [Theory]
        [InlineData(10, 0x8B)]
        [InlineData(0, 0x80)]
        [InlineData(-1, 0x81)]
        [InlineData(126, 0xFF)]
        public void SetLocoDrive_should_output_speed_byte(int speed, byte b)
        {
            var result = new SetLocoDrive(3, Direction.Forward, speed).Bytes;
            Assert.Equal(b, result[8]);
        }

        [Fact]
        public void SetLocoDrive_given_DCC128_speed_127_should_throw_ArgumentOutOfRangeException()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => new SetLocoDrive(3, Direction.Forward, 127));
        }

        [Fact]
        public void SetLocoDrive_given_DCC14_speed_15_should_throw_ArgumentOutOfRangeException()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => new SetLocoDrive(3, Direction.Forward, 15, SpeedSteps.Dcc14));
        }

    }
}
