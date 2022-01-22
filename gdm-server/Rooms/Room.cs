using gdm_server.Level_Multiplayer.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Rooms {
    public class Room {
        /// <summary>
        /// One gamemode for each room.
        /// </summary>
        public Gamemodes Gamemode { get; set; }

        /// <summary>
        /// We wont have more than 65,000+ rooms anyways.
        /// </summary>
        public long RoomID { get; set; }

        public string RoomName { get; set; }

        public string RoomPassword { get; set; } = string.Empty;

        /// <summary>
        /// Online players in the room.
        /// </summary>
        public List<Player> Players { get; set; } = new();
    }
}
