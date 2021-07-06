using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer.Packets
{ 
    /// <summary>
    /// Received packet parser class.
    /// </summary>
    public class Parser
    {
        private byte[] _buffer;
        private int _position = 0;

        /// <summary>
        /// Initialize a new instance of the packet parser class.
        /// </summary>
        /// <param name="buffer">The received packet.</param>
        public Parser(byte[] buffer) {
            _buffer = buffer;
        }

        /// <summary>
        /// Gets the prefix (very first byte) of the received packet.
        /// </summary>
        public Prefix GetPrefix() {
            return (Prefix)ReadByte();
        }

        /// <summary>
        /// Read a single byte.
        /// </summary>
        public byte ReadByte() {
            return ReadBytes(1).FirstOrDefault();
        }

        /// <summary>
        /// Reads 4 bytes then converts it into an integer.
        /// </summary>
        public int ReadInt32() {
            return BitConverter.ToInt32(ReadBytes(4).ToArray());
        }

        /// <summary>
        /// Reads 2 bytes then converts it into short.
        /// </summary>
        public short ReadInt16() {
            return BitConverter.ToInt16(ReadBytes(2).ToArray());
        }

        /// <summary>
        /// Read desired number of bytes.
        /// </summary>
        public IEnumerable<byte> ReadBytes(int length)
        {
            var segment = new ArraySegment<byte>(_buffer, _position, length);
            _position += length;
            return segment;
        }

        ///
        /// Now parse some objects.
        ///

    }
}
