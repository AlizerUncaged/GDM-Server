using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Rooms {
    public enum Gamemodes : byte {
        /// <summary>
        /// Just shows other players on Geometry Dash.
        /// </summary>
        Normal,
        /// <summary>
        /// One player dies, everyone dies.
        /// </summary>
        OneForAll,
        /// <summary>
        /// When someone dies, turn them into spectators
        /// until a player finishes a level. All players
        /// should respawn when everyone dies.
        /// </summary>
        Racemode,
        Custom
    }
}
