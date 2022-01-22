using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer.Client {
    /// <summary>
    /// A class used for storing player position.
    /// </summary>
    public struct PlayerPosition {
        public PlayerPositionTransform Player1;
        public PlayerPositionTransform Player2;

        public byte Form, ActiveIconID, Gravity, IsDead;
    }
}
