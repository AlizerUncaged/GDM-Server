using log4net;
using log4net.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace gdm_server
{
    class Program {
        public static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// Main entry point.
        static async Task Main(string[] args) {
#if DEBUG
            Config.IP = "127.0.0.1";
#endif
            var LogRespository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(LogRespository, new FileInfo("log4net.config"));
            Logger.Info($@"
 ._       __          ____
;  `\--,-' /`)    _.-'    `-._
 \_/    ' | /`--,'            `-.     .--....____
  /                              `._.'           `---...
  |-.   _      ;                        .-----..._______)
,,\q/ (q_>'_...                      .-'
===/ ; _.-'~~-             /       ,'
`""""`-'_,;  `""""         ___(       |
         \         ; /'/   \      \
          `.      //' (    ;`\    `\
          / \    ;     `-  /  `-.  /
         (  (;   ;     (__/    /  /
          \,_)\  ;           ,'  /
  .-.          |  |           `--'
 (""_.) -._(__,> Log started {DateTime.UtcNow}

");


            var LevelMultiplayerServer = new Level_Multiplayer.LevelServer();
            // LevelMultiplayerServer.Start();

            var watsonServer = new Level_Multiplayer.Watson.WServer();
            await watsonServer.StartServer();
        }
    }
}
