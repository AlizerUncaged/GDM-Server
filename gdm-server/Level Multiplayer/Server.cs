using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer
{
    public class Server
    {
        private Task ServerReader;
        private bool _keepReading = true;

        /// <summary>
        /// Start the server reader.
        /// </summary>
        public async void Start()
        {
            ServerReader = new Task(() => {

                while (_keepReading) {

                    try { 
                    
                    }
                    catch (Exception ex) {
                        Utils.ConsoleLog.Write(ex.ToString(), Utils.LogLevel.Warn, ConsoleColor.Red);
                    }
                }

                Utils.ConsoleLog.Write("Server reader stopped.", Utils.LogLevel.All, ConsoleColor.Green);

            });

            // forgive me
            await Task.Run(() => { ServerReader.Start(); });
        }

        /// <summary>
        /// Stops the server reader.
        /// </summary>
        public void Stop() {
            _keepReading = false;
            // Thread.Abort is nowhere to be found
        }
    }
}
