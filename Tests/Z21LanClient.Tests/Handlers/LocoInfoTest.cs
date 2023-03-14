using Z21LanClient.Handlers;
using Z21LanClient.Model;

namespace Z21LanClient.Tests.Handlers
{
    public class LocoInfoTest
    {
        [Fact]
        public void Handle_should_set_Address()
        {
            var data = new byte[] { 0x0A, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x29 };

            LocoInfoEventArgs? args = null;
            var sut = new LocoInfo((s, e) =>
            {
                args = (LocoInfoEventArgs)e;
            });
            var result = sut.Handle(data);

            Assert.True(result);
            Assert.Equal(555, args?.Address);
        }

        [Theory]
        [InlineData(new byte[] { 0x0A, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x04, 0x80 | 127, 0x29 }, SpeedSteps.Dcc128, Direction.Forward, 126)]
        [InlineData(new byte[] { 0x0A, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x04, 0x80 | 0, 0x29 }, SpeedSteps.Dcc128, Direction.Forward, 0)]
        [InlineData(new byte[] { 0x0A, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x04, 0x80 | 1, 0x29 }, SpeedSteps.Dcc128, Direction.Forward, -1)]
        [InlineData(new byte[] { 0x0A, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 11, 0x29 }, SpeedSteps.Dcc14, Direction.Backward, 10)]
        public void Handle_should_set_Direction_and_Speed(byte[] data, SpeedSteps speedSteps, Direction direction, int speed)
        {
            LocoInfoEventArgs? args = null;
            var sut = new LocoInfo((s, e) =>
            {
                args = (LocoInfoEventArgs)e;
            });
            var result = sut.Handle(data);

            Assert.True(result);
            Assert.Equal(speedSteps, args?.SpeedSteps);
            Assert.Equal(direction, args?.Direction);
            Assert.Equal(speed, args?.Speed);
        }

        [Theory]
        [InlineData(new byte[] { 0x0B, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x40, 0x29 }, true, false)]
        [InlineData(new byte[] { 0x0B, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x20, 0x29 }, false, true)]
        [InlineData(new byte[] { 0x0A, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0xFF }, false, false)] //no data byte - both values should be false
        public void Handle_should_set_DoubleTraction_and_Smartsearch(byte[] data, bool doubleTraction, bool smartsearch)
        {
            LocoInfoEventArgs? args = null;
            var sut = new LocoInfo((s, e) =>
            {
                args = (LocoInfoEventArgs)e;
            });
            var result = sut.Handle(data);

            Assert.True(result);
            Assert.Equal(doubleTraction, args?.DoubleTraction);
            Assert.Equal(smartsearch, args?.SmartSearch);
        }

        [Theory]
        [InlineData(new byte[] { 0x0B, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x10, 0xFF }, new bool[] { true, false, false, false, false })]
        [InlineData(new byte[] { 0x0B, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x11, 0xFF }, new bool[] { true, true, false, false, false })]
        [InlineData(new byte[] { 0x0B, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x08, 0xFF }, new bool[] { false, false, false, false, true })]
        [InlineData(new byte[] { 0x0B, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x06, 0xFF }, new bool[] { false, false, true, true, false})]
        public void Handle_should_set_Function_0_4(byte[] data, bool[] functions)
        {
            LocoInfoEventArgs? args = null;
            var sut = new LocoInfo((s, e) => { args = (LocoInfoEventArgs)e; });
            var result = sut.Handle(data);

            Assert.True(result);
            for (int i = 0; i < args?.Functions.Length; i++)
            {
                Assert.Equal(i < 5 && functions[i], args?.Functions[i]);
            }
        }

        [Theory]
        [InlineData(new byte[] { 0x0C, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x00, 0x01, 0xFF }, new bool[] { true, false, false, false, false, false, false, false })]
        [InlineData(new byte[] { 0x0C, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x00, 0x80, 0xFF }, new bool[] { false, false, false, false, false, false, false, true })]
        [InlineData(new byte[] { 0x0C, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x00, 0x7E, 0xFF }, new bool[] { false, true, true, true, true, true, true, false })]
        public void Handle_should_set_Function_5_12(byte[] data, bool[] functions)
        {
            LocoInfoEventArgs? args = null;
            var sut = new LocoInfo((s, e) => { args = (LocoInfoEventArgs)e; });
            var result = sut.Handle(data);

            Assert.True(result);
            for (int i = 0; i < args?.Functions.Length; i++)
            {
                Assert.Equal(i is > 4 and < 13 && functions[i - 5], args?.Functions[i]);
            }
        }

        [Theory]
        [InlineData(new byte[] { 0x0D, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x00, 0x00, 0x01, 0xFF }, new bool[] { true, false, false, false, false, false, false, false })]
        [InlineData(new byte[] { 0x0D, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x00, 0x00, 0x80, 0xFF }, new bool[] { false, false, false, false, false, false, false, true })]
        [InlineData(new byte[] { 0x0D, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x00, 0x00, 0x7E, 0xFF }, new bool[] { false, true, true, true, true, true, true, false })]
        public void Handle_should_set_Function_13_20(byte[] data, bool[] functions)
        {
            LocoInfoEventArgs? args = null;
            var sut = new LocoInfo((s, e) => { args = (LocoInfoEventArgs)e; });
            var result = sut.Handle(data);

            Assert.True(result);
            for (int i = 0; i < args?.Functions.Length; i++)
            {
                Assert.Equal(i is > 12 and < 21 && functions[i - 13], args?.Functions[i]);
            }
        }

        [Theory]
        [InlineData(new byte[] { 0x0F, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0xFF }, new bool[] { true, false, false })]
        [InlineData(new byte[] { 0x0F, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0xFF }, new bool[] { false, true, false })]
        [InlineData(new byte[] { 0x0F, 0x00, 0x40, 0x00, 0xEF, 0x02, 0x2B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0xFF }, new bool[] { false, false, true })]
        public void Handle_should_set_Function_29_31(byte[] data, bool[] functions)
        {
            LocoInfoEventArgs? args = null;
            var sut = new LocoInfo((s, e) => { args = (LocoInfoEventArgs)e; });
            var result = sut.Handle(data);

            Assert.True(result);
            for (int i = 0; i < args?.Functions.Length; i++)
            {
                Assert.Equal(i is > 28 and < 32 && functions[i - 29], args?.Functions[i]);
            }
        }
    }
}
