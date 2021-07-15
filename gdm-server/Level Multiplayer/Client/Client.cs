using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer.Client
{
    public class Client
    {
        /// <summary>
        /// The client's key, this only gets set once and gets
        /// checked every time the client sends packets. Discards
        /// packets if the Key is not the same.
        /// </summary>
        public int Key;

        /// <summary>
        /// The player ID of the client. Relies on
        /// Geometry Dash player ID.
        /// </summary>
        public int PlayerID;

        private UdpClient _server;

        public Client(int id, int key, UdpClient masterserver, IPEndPoint clientipep) {
            Key = key; PlayerID = id; _server = masterserver; _ipep = clientipep;
        }

        /// <summary>
        /// This should contain the IP and Port of the player.
        /// </summary>
        private IPEndPoint _ipep;

        /// <summary>
        /// Set to true whenever the client is about to be removed on the next server refresh.
        /// </summary>
        public bool ToBeRemoved = false;

        /// <summary>
        /// Current level the player is in, set to -1 if the player is outside a level.
        /// </summary>
        public int LevelID = -1;

        /// <summary>
        /// The current Icon ID of the current form the player has.
        /// </summary>
        public byte ActiveIconID;

        /// <summary>
        /// Positions for player 1 and 2.
        /// </summary>
        public PlayerPosition Player1, Player2;

        public byte Color1, Color2, IsGlow, IsDead;

        /// <summary>
        /// The current room the player is in, recommended public lobby is 0.
        /// </summary>
        public short Room = 0;

        /// <summary>
        /// The iconIDs of the player.
        /// </summary>
        public byte[] IconIDs = new byte[] { 
            0, // cube
            0, // ship

            /// for some reason these are flipped everywhere on robtop's code
            0, // ball or ball
            0, // ufo or ufo 
            
            0, // wave
            0, // robot
            0  // spider
        };

        /// <summary>
        /// Normally I check the accountID if it exists on Geometry Dash servers but we wont have to worry about it now.
        /// </summary>
        public bool IsAuthenticated = true;

        /// <summary>
        /// This gets decremented everytime the server refreshes client list
        /// and gets reset everytime the player sends a packet. If this gets
        /// lower than or equal to 0, this client gets removed.
        /// </summary>
        public int TimeoutCount = 0;

        /// <summary>
        /// Check if key is the same.
        /// </summary>
        public bool CheckKey(int key) {
            return Key == key;
        }

        /// <summary>
        /// Sends data to the client.
        /// </summary>
        /// <param name="buffer">The UDP Packet.</param>
        public void WriteBytes(byte[] buffer)
        {
            try {
                _server.SendAsync(buffer, buffer.Length, _ipep);
            }
            // normally an exception will be called naturally when
            // the client disconnected just before writing the packet
            // or the client has connectivity problems.......
            // or if the server has connectivity problems
            catch (Exception ex)
            {
                Log.Error(ex.ToString(), "An error occured sending bytes to client");
            }
        }

    }
}
