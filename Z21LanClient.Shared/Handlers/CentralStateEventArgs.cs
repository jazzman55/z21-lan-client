using System;
using Z21LanClient.Model;

namespace Z21LanClient.Handlers
{
    public class CentralStateEventArgs: EventArgs
    {
        public CentralStateEventArgs(CentralState centralState)
        {
            CentralState = centralState;
        }

        public CentralState CentralState { get;}
    }
}