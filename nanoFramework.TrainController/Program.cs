using System;
using System.Diagnostics;
using System.Threading;
using nanoFramework.Logging.Debug;
using nanoFramework.Networking;
using Z21.Device;
using Z21LanClient;
using Z21LanClient.Commands;
using Z21LanClient.Handlers;
using Z21LanClient.Model;

namespace nanoFramework.TrainController
{
    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine("Hello from nanoFramework!");

            IHandler h = null;

            WifiNetworkHelper.ConnectDhcp("RAILROAD", "12345678", requiresDateTime: false);


            var client = new Z21CommandStation(new NanoUdpClient(), new DebugLogger(nameof(Z21CommandStation)));

            client.LocoInfoReceived += Client_LocoInfoReceived;
            client.StatusChangedReceived += Client_StatusChangedReceived;
            client.SystemStateChangedReceived += Client_SystemStateChangedReceived;

            client.Connect("192.168.4.111");

            while (true)
            {
                client.Send(new SetLocoDrive(3, Direction.Forward, 10));
                Thread.Sleep(5000);
                client.Send(new SetLocoDrive(3, Direction.Backward, 10));
                Thread.Sleep(5000);
            }

        }

        private static void Client_SystemStateChangedReceived(object sender, EventArgs e)
        {
            var data = e as SystemStateChangedEventArgs;
            Debug.WriteLine($"{data.MainCurrent} {data.FilteredMainCurrent} {data.SupplyVoltage} {data.VccVoltage} {data.Temperature} {data.CentralStateEx} {data.CentralStateEx} {data.Capabilities}");
            Debug.WriteLine();
        }

        private static void Client_StatusChangedReceived(object sender, EventArgs e)
        {
            var data = e as StatusChangedEventArgs;
            Debug.WriteLine($"{data.CentralState}");
            Debug.WriteLine();
        }

        private static void Client_LocoInfoReceived(object sender, EventArgs e)
        {
            var data = e as LocoInfoEventArgs;
            Debug.WriteLine($"{data.Address} {data.Direction} {data.Speed}");
            Debug.WriteLine();
        }
    }
}
