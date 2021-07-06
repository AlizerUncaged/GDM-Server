using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer.Packets
{
    /// <summary>
    /// Representation of the first byte.
    /// </summary>
    public enum Prefix
    {
        Hello = 0x3,
        Ping = 0x0,
        Message = 0x1,
        Disconnect = 0x2,
        HelloAck = 0x4,
        ServerData = 0x5,
        PlayerDisconnect = 0x7,
        PlayerIcons = 0x8,
        ReceiveIcons = 0x9,
        OutsideLevel = 0x10,
        VipActions = 0x11,
        BadKey = 0x12,
    }
}
