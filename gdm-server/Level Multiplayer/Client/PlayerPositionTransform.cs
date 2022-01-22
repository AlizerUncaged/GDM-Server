using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer.Client {
    public struct PlayerPositionTransform {
        // fields to reduce overhead
        public int X, Y, XR, YR, Size;
    }
}
