using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Rooms.Models {
    public struct AccountJoinRoomResponse {
        public bool IsSuccess { get; set; }
        /// <summary>
        /// The reason why failed if failed.
        /// </summary>
        public string Reason { get; set; }
        public long RoomID { get; set; }
        public short FPS { get; set; }
        // room id + player + vip key in one int, we wont have 2billion+ players anyways
        public int SessionKey { get; set; }
    }
}
