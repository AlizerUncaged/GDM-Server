using gdm_server.Rooms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer.Client {
    public class Player {
        #region Player Data
        /// <summary>
        /// The key on main database if VIP.
        /// </summary>
        public string DatabaseKey { get; set; }

        /// <summary>
        /// The player ID of the client. Relies on
        /// Geometry Dash player ID.
        /// </summary>
        public int PlayerId { get; set; }

        public int AccountId { get; set; }

        public bool IsVip { get; set; }
        public string Username { get; set; }

        /// <summary>
        /// The player's Col1 and Col2.
        /// </summary>
        public (byte Color1, byte Color2) Colors { get; set; }

        // server only-var
        /// <summary>
        /// The room the player is in.
        /// </summary>
        [JsonIgnore]
        public Room Room { get; set; } 

        [JsonIgnore]
        // server generated once
        public int SessionKey { get; set; }= GRandom.RandomInt();

        #endregion
        
        public IPEndPoint PlayerEndpoint;

        public override bool Equals(object obj)
        {
            if (obj is Player player)
            {
                return player.PlayerId == this.PlayerId;
            }

            return false;
        }

        public static bool operator ==(Player left, Player right)
        {
            return right != null && left != null && left.PlayerId == right.PlayerId;
        }

        public static bool operator !=(Player left, Player right)
        {
            return !(left == right);
        }
        // player position and transform is not stored anywhere in the server.
    }
}
