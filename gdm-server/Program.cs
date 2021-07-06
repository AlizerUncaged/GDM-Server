using System;

namespace gdm_server
{
    class Program
    {
        // Main entry point.
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing logs...");
            // Intialize the logger.
            if (Config.LogLevel > 0)
            {
                // create logger
                Utils.FileLog.Instance = new Utils.FileLog(Config.LogFile);
                Utils.FileLog.Instance.Log("Started GDM-Server", Utils.LogLevel.Off);
            }

            // initialize the server
            Utils.ConsoleLog.Write("Server is starting...", Utils.LogLevel.Off, ConsoleColor.Yellow);

            var LevelMultiplayerServer = new Level_Multiplayer.Server();
            LevelMultiplayerServer.Start();


        }
    }
}
