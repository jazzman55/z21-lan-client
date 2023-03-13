using System;
using Z21LanClient.Model;

namespace Z21LanClient.Handlers
{
    public class StatusChangedEventArgs: EventArgs
    {
        public StatusChangedEventArgs(CentralState centralState)
        {
            CentralState = centralState;
        }

        public CentralState CentralState { get;}
    }
}