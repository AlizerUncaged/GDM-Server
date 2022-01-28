using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Server;

public struct Packet {
    public Packet(byte[] packetByte) {
        var sequencer = new PacketSequencer(packetByte);
        // set variables
        SessionKey = sequencer.ReadInt();
        PacketMessageType = (PacketType) sequencer.ReadByte();
    }

    public bool WasInvalid => SessionKey is null;

    /// <summary>
    /// A valid room which should be greater than 0.
    /// </summary>
    public readonly int? SessionKey;

    public PacketType PacketMessageType;
}