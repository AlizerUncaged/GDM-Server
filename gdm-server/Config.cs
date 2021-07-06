using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server
{
    /// <summary>
    /// Server configuration, parsed from config.json the variables set are default and are replaced when the json is loaded.
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// The port the entire server will listen to.
        /// </summary>
        public static short Port = 7010;

        /// <summary>
        /// The interface the server will listen to.
        /// 127.0.0.1 for local and 0.0.0.0 (or localhost) for public.
        /// </summary>
        public static string IP = "0.0.0.0";

        /// <summary>
        /// The maximum number of players playing on the server.
        /// </summary>
        public static long MaxPlayers = long.MaxValue;

        /// <summary>
        /// The maximum time allotted for players to not send any UDP packets before they get disconnected.
        /// Useful for saving server resources.
        /// </summary>
        public static long MaxTimeoutSeconds = 10;

        /// <summary>
        /// Set the log level.
        /// 0, Off = nothing to log, doesn't create any file, nothing
        /// 1, All = log everything
        /// 2, Warn = logs only errors
        /// 
        /// Keep in mind that everything is written on the console.
        /// </summary>
        public static int LogLevel = (int)Utils.LogLevel.All;

        /// <summary>
        /// The log file where %date% has the date representation of yyyy-dd-M--HH-mm-ss
        /// if the folder where the log file doesn't exists, it automatically creates it.
        /// </summary>
        public static string LogFile = "gdm-logs/%date%-log.txt";

    }
}
