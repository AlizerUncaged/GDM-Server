using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server {
    /// <summary>
    /// Server configuration, parsed from config.json the variables set are default and are replaced when the json is loaded.
    /// </summary>
    public static class Config {
        /// <summary>
        /// The port the entire server will listen to.
        /// </summary>
        public static short Port = 7010;

        /// <summary>
        /// For sending static data and authentication.
        /// </summary>
        public static short TcpPort = (short)(Port + 1);

        /// <summary>
        /// The interface the server will listen to.
        /// 127.0.0.1 for local and 0.0.0.0 (or localhost) for public.
        /// </summary>
        public static string IP = "*";

        /// <summary>
        /// The maximum number of players playing on the server.
        /// </summary>
        public static long MaxPlayers = long.MaxValue;

        public static short GlobalDefaultFPS = 50;
    }
}
