using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer.Client {
    public struct Icon {
        public int IconID { get; set; }
        public (byte Color1, byte Color2) Colors { get; set; }
    }
}
