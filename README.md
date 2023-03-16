# Z21 LAN Client
.NET Core & [nanoFramework](https://www.nanoframework.net/) library for communication with [Z21](https://www.z21.eu/en) DCC command station.
## Description
- Based on [Z21 LAN Protocol Specification](https://www.z21.eu/media/Kwc_Basic_DownloadTag_Component/root-en-main_47-1652-959-downloadTag-download/default/d559b9cf/1628743384/z21-lan-protokoll-en.pdf)
- Uses UDP for network communication to Z21
- Not all features of Z21 protocol are implemented, however you can drive trains and do most useful things on model railway :)
## Notes
Due to code sharing between .NET Core and nanoFramework implementation, some nice C# language features had to be sacrificed (like generics).
## Usage
nanoFramework:
```c#
WifiNetworkHelper.ConnectDhcp(Ssid, Password, requiresDateTime: false);

//Create client instance
var client = new Z21CommandStation(new NanoUdpClient(new DebugLogger(nameof(NanoUdpClient))), new DebugLogger(nameof(Z21CommandStation)));

//Subscribe to received messages
client.LocoInfoReceived += Client_LocoInfoReceived;
client.StatusChangedReceived += Client_StatusChangedReceived;
client.SystemStateChangedReceived += Client_SystemStateChangedReceived;

client.Connect("192.168.4.111");

//Drive train
client.Send(new SetLocoDrive(3, Direction.Forward, 10));

[...]

private static void Client_LocoInfoReceived(object sender, EventArgs e)
{
    var data = e as LocoInfoEventArgs;
    Debug.WriteLine($"{data.Address} {data.Direction} {data.Speed}");
}
```
