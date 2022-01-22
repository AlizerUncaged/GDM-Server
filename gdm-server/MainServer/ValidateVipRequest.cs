using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.MainServer {
    /// <summary>
    /// Validates the Vip key of the player.
    /// </summary>
    public class ValidateVipRequest : DatabaseServerRequest {
        // will explode if the player is not vip but attempting to validate his key
        public ValidateVipRequest(int playerID, string vipKey) : base($"accounts", $"playerID={playerID}&key={vipKey}") {

        }
        public async Task<bool> IsKeyValid() {
            return await GetDataAsTypeAsync<bool>();
        }
    }
}
