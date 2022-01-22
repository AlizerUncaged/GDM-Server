using gdm_server.Level_Multiplayer.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Rooms.Models {
    public struct AccountJoinRoomRequest {
        /// <summary>
        /// Just send the entire player.
        /// </summary>
        public Player Player { get; set; }
        public long RoomID { get; set; }
        public string VipKey { get; set; }
    }
}
