namespace Z21LanClient.Commands
{
    public class LogOn : SetBroadcastFlags
    {
        public LogOn(): base(BroadcastFlags.DrivingAndSwitching | BroadcastFlags.LocoInfo)
        {
            
        }
    }
}