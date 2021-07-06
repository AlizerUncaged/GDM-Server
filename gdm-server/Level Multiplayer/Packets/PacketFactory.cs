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


        public static byte[] SendDisconnectMessage(string message) {
            List<byte> bytes = new List<byte>();
            bytes.Add((byte)Prefix.Disconnect);
            bytes.AddRange(Encoding.UTF8.GetBytes(message));
            return bytes.ToArray();
        }

        public static byte[] PositionsToBytes(Client.PlayerPosition pm)
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes(pm.X));
            bytes.AddRange(BitConverter.GetBytes(pm.Y));
            bytes.AddRange(BitConverter.GetBytes(pm.XR));
            bytes.AddRange(BitConverter.GetBytes(pm.YR));
            bytes.Add(pm.Form);
            bytes.Add(pm.ActiveIconID);
            bytes.AddRange(BitConverter.GetBytes(pm.Size));
            bytes.Add(pm.Gravity); // TODO CHECK THIS
            return bytes.ToArray();
        }

        public static byte[] SendPlayerData(Client.Client client)
        {
            List<byte> bytes = new List<byte>();
            bytes.Add((byte)Prefix.Message);
            bytes.AddRange(BitConverter.GetBytes(client.PlayerID));

            bytes.AddRange(PositionsToBytes(client.Player1));
            bytes.AddRange(PositionsToBytes(client.Player2));

            bytes.Add(client.IsDead);
            bytes.Add(0x0); // hardcoded
            bytes.Add(client.Color1);
            bytes.Add(client.Color2);
            bytes.Add(client.IsGlow);
            bytes.AddRange(client.IconIDs);
            bytes.Add(0x0); // hardcoded
            return bytes.ToArray();
        }

        public static byte[] SendPlayerDisconnect(int clientID)
        {
            List<byte> bytes = new List<byte>();
            bytes.Add((byte)Prefix.PlayerDisconnect);
            bytes.AddRange(BitConverter.GetBytes(clientID));
            return bytes.ToArray();
        }
    }
}
