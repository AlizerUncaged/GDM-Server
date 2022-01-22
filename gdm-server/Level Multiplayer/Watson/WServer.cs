using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatsonWebserver;
namespace gdm_server.Level_Multiplayer.Watson {
    public class WServer {

        private WatsonWebserver.Server server;
        public WServer() {
            server = new(Config.IP, Config.TcpPort, false);
        }

        public async Task StartServer() {
            await server.StartAsync();
        }
    }
}

