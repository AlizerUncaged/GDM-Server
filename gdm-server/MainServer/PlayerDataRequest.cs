using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.MainServer {
    public class PlayerDataRequest : DatabaseServerRequest {
        public PlayerDataRequest(int playerID) : base($"accounts", $"playerID={playerID}") {

        }
        public async Task<AccountStatus> GetPlayerData() {
            return await GetDataAsTypeAsync<AccountStatus>();
        }
    }
}
