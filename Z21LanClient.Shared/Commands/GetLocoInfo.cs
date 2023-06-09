﻿using Z21LanClient.Extensions;

namespace Z21LanClient.Commands
{
    /// <summary>
    /// LAN_X_GET_LOCO_INFO
    /// </summary>
    public class GetLocoInfo : ICommand
    {
        public byte[] Bytes { get; }

        public GetLocoInfo(int address)
        {
            Bytes = new byte[] { 0x09, 0x00, 0x40, 0x00, 0xE3, 0xF0, 0x00, 0x00, 0x00 };

            Bytes.SetAddress(address, 6);

            Bytes.SetChecksum();
        }

    }
}