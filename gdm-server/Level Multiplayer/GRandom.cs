using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer {
    /// <summary>
    /// Global instance of a random generator.
    /// </summary>
    public static class GRandom {
        public static Random Random = new();

        public static short RandomShort() {
            var intInRange = Random.Next(-32768, 32767);
            var convertedToShort = (short)intInRange;
            return convertedToShort;
        }

        public static int RandomInt() {
            return Random.Next();
        }
    }
}
