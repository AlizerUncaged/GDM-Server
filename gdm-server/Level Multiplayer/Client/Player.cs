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
        /// <summary>
        /// The key on main database if VIP.
        /// </summary>
        public string DatabaseKey { get; set; }

        /// <summary>
        /// The player ID of the client. Relies on
        /// Geometry Dash player ID.
        /// </summary>
        public int PlayerID { get; set; }

        public int AccountID { get; set; }

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
        public int SessionKey { get; set; }
        /// Values that change when playing.
        #region Changes every frame.
        /// <summary>
        /// Positions and transforms.
        /// </summary>
        public PlayerPosition Position;
        #endregion
    }
}
