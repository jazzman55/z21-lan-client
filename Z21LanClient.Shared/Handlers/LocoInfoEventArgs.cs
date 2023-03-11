using System;
using Z21LanClient.Model;

namespace Z21LanClient.Handlers
{
    public class LocoInfoEventArgs : EventArgs
    {
        public LocoInfoEventArgs(int address, bool isBusy, SpeedSteps speedSteps, Direction direction, int speed, bool doubleTraction, bool smartSearch, bool[] functions)
        {
            Address = address;
            IsBusy = isBusy;
            SpeedSteps = speedSteps;
            Direction = direction;
            Speed = speed;
            DoubleTraction = doubleTraction;
            SmartSearch = smartSearch;
            Functions = functions;
        }

        public int Address { get; }
        public bool IsBusy { get; }
        public SpeedSteps SpeedSteps { get; }
        public Direction Direction { get; }
        public int Speed { get; }
        public bool DoubleTraction { get; }
        public bool SmartSearch { get; }
        public bool[] Functions { get; }

    }
}