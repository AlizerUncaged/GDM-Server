using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Server;

/// <summary>
/// Helper class for sequencing through the bytes.
/// </summary>
public class PacketSequencer {
    private int position = 0;
    private byte[] packet;
    private int maxPosition;
    public PacketSequencer(byte[] bytes) {
        packet = bytes;
        maxPosition = packet.Length;
    }

    /// <summary>
    /// Returns 1 byte from the array.
    /// </summary>
    public byte? ReadByte() {
        var segment = ReadArray(1);
        if (segment is { }) {
            return segment[0];
        }
        return null;
    }

    /// <summary>
    /// Reads 4 bytes and returns it as int.
    /// </summary>
    public int? ReadInt() {
        var segment = ReadArray(4);
        if (segment is { }) {
            return BitConverter.ToInt32(segment);
        }
        return null;
    }

    /// <summary>
    /// Reads 2 bytes and returns it as short.
    /// </summary>
    public short? ReadShort() {
        var segment = ReadArray(2);
        if (segment is { }) {
            return BitConverter.ToInt16(segment);
        }
        return null;
    }

    public long? ReadLong() {
        var segment = ReadArray(8);
        if (segment is { }) {
            return BitConverter.ToInt64(segment);
        }
        return null;
    }

    public byte[] ReadArray(int bytes) {
        int newPosition = position + bytes;
        if (newPosition > maxPosition) {
            return null;
        }

        var segment = new ArraySegment<byte>(packet, position, bytes);
        position = newPosition;

        return segment.ToArray();
    }
}