using System;

namespace gdm_server
{
    class Program
    {
        private static string config_file = "server.json";
        /// Main entry point.
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing...");

            // Initialize config
            Config.LoadConfig(config_file);

            // Intialize the logger.
            if (Config.Global.LogLevel > 0)
            {
                // create logger
                Utils.FileLog.Instance = new Utils.FileLog(Config.Global.LogFile);
                Utils.FileLog.Instance.Log("Started GDM-Server", Utils.LogLevel.Off);
            }

            // initialize the server
            Utils.ConsoleLog.Write("Server is starting...", Utils.LogLevel.Off, ConsoleColor.Yellow);

            // var LevelMultiplayerServer = new Level_Multiplayer.Server();
            // LevelMultiplayerServer.Start();
        }
    }
}
