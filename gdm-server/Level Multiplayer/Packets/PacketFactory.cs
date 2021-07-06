using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer.Packets
{
    /// <summary>
    /// Creates packets.
    /// </summary>
    public static class PacketFactory
    {
        /// <summary>
        /// Sent to confirm client hello is received and client is online.
        /// </summary>
        public static byte[] HelloAck() {
            return new byte[] { (byte)Prefix.HelloAck };
        }

        /// <summary>
        /// Sent whenever a ping is received from a client.
        /// </summary>
        public static byte[] PingAck() {
            return new byte[] { (byte)Prefix.Ping };
        }

        /// <summary>
        /// Sent whenever a client joins with a bad key.
        /// </summary>
        public static byte[] BadKey() {
            return new byte[] { (byte)Prefix.BadKey };
        }
    }
}
