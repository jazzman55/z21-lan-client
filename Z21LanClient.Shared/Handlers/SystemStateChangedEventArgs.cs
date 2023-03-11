using System;
using Z21LanClient.Model;

namespace Z21LanClient.Handlers
{
    public class SystemStateChangedEventArgs : EventArgs
    {
        public Int16 MainCurrent { get; set; }
        public Int16 ProgCurrent { get; set; }
        public Int16 FilteredMainCurrent { get; set; }
        public Int16 Temperature { get; set; }
        public UInt16 SupplyVoltage { get; set; }
        public UInt16 VccVoltage { get; set; }
        public CentralState CentralState { get; set; }
        public CentralStateEx CentralStateEx { get; set; }
        public Capabilities Capabilities { get; set; }

        public SystemStateChangedEventArgs(short mainCurrent, short progCurrent, short filteredMainCurrent, short temperature, ushort supplyVoltage, ushort vccVoltage, CentralState centralState, CentralStateEx centralStateEx, Capabilities capabilities)
        {
            MainCurrent = mainCurrent;
            ProgCurrent = progCurrent;
            FilteredMainCurrent = filteredMainCurrent;
            Temperature = temperature;
            SupplyVoltage = supplyVoltage;
            VccVoltage = vccVoltage;
            CentralState = centralState;
            CentralStateEx = centralStateEx;
            Capabilities = capabilities;
        }
    }
}