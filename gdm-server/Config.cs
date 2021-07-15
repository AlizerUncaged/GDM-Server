using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server
{
    /// <summary>
    /// Server configuration, parsed from config.json the variables set are default and are replaced when the json is loaded.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// The port the entire server will listen to.
        /// </summary>
        public short Port = 7010;

        /// <summary>
        /// The interface the server will listen to.
        /// 127.0.0.1 for local and 0.0.0.0 (or localhost) for public.
        /// </summary>
        public string IP = "0.0.0.0";

        /// <summary>
        /// The maximum number of players playing on the server.
        /// </summary>
        public long MaxPlayers = long.MaxValue;

        /// <summary>
        /// The maximum time allotted for players to not send any UDP packets before they get disconnected.
        /// Useful for saving server resources.
        /// </summary>
        public long MaxTimeoutSeconds = 10;

        /// <summary>
        /// The log file where %date% has the date representation of yyyy-dd-M--HH-mm-ss
        /// if the folder where the log file doesn't exists, it automatically creates it.
        /// </summary>
        public string LogFile = "gdm-logs/%date%-log.txt";

        /// <summary>
        /// Loads config file from path.
        /// </summary>
        public static Config LoadConfig(string path)
        {
            var Global = JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));

            // rewrite to config file
            // necessary in case of new versions having new config 
            string output = JsonConvert.SerializeObject(Global);
            File.WriteAllText(path, output); return Global;
        }
    }
}
