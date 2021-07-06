using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer.Client
{
    public class Client
    {
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

        public byte Color1, Color2, IsGlow, IsDead;

    }
}
