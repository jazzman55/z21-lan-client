using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z21LanClient.Handlers;
using Z21LanClient.Model;

namespace Z21LanClient.Tests.Handlers
{
    public class SystemStateChangedTest
    {
        [Fact]
        public void Handle_should_set_Current_Voltage_Temperature()
        {
            var data = new byte[] { 0x14, 0x00, 0x84, 0x00, 0xE8, 0x03, 0x20, 0x03, 0x4C, 0x04, 0x19, 0x00, 0x12, 0x00, 0x15, 0x00, 0xFF, 0xFF, 0xFF, 0xFF };

            SystemStateChangedEventArgs? args = null;
            var sut = new SystemStateChanged((s, e) =>
            {
                args = (SystemStateChangedEventArgs)e;
            });
            var result = sut.Handle(data);

            Assert.True(result);
            Assert.Equal((short)1000, args?.MainCurrent);
            Assert.Equal((short)800, args?.ProgCurrent);
            Assert.Equal((short)1100, args?.FilteredMainCurrent);
            Assert.Equal((short)25, args?.Temperature);
            Assert.Equal((ushort)18, args?.SupplyVoltage);
            Assert.Equal((ushort)21, args?.VccVoltage);
        }

        [Fact]
        public void Handle_should_set_Flags()
        {
            var data = new byte[] { 0x14, 0x00, 0x84, 0x00, 0xE8, 0x03, 0x20, 0x03, 0x4C, 0x04, 0x19, 0x00, 0x12, 0x00, 0x15, 0x00, 0x05, 0x0A, 0xFF, 0x19 };

            SystemStateChangedEventArgs? args = null;
            var sut = new SystemStateChanged((s, e) =>
            {
                args = (SystemStateChangedEventArgs)e;
            });
            var result = sut.Handle(data);

            Assert.True(result);
            Assert.Equal(CentralState.EmergencyStop | CentralState.ShortCircuit, args?.CentralState);
            Assert.Equal(CentralStateEx.PowerLost| CentralStateEx.ShortCircuitInternal, args?.CentralStateEx);
            Assert.Equal(Capabilities.DCC | Capabilities.RailCom | Capabilities.LocoCmds, args?.Capabilities);
        }
    }
}
