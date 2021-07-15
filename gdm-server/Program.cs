using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Linq;

namespace gdm_server
{
    class Program
    {
        private static string config_file = "server.json";
        /// Main entry point.
        static void Main(string[] args)
        {
            // Initialize config
            var config = Config.LoadConfig(config_file);


            var logLevel = LogEventLevel.Information;
#if DEBUG
            logLevel = LogEventLevel.Verbose;
#endif
            if (args.Contains("--verbose"))
            {
                logLevel = LogEventLevel.Verbose;
            }
            else if (args.Contains("--errors-only"))
            {
                logLevel = LogEventLevel.Error;
            }

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Is(logLevel)
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .Enrich.FromLogContext()
               .WriteTo.Console(theme: AnsiConsoleTheme.Code)
               .CreateLogger();
            Log.Information("Starting Geometry Dash Level Editor Multiplayer");
            Log.Verbose("Reading config.json");

            var LevelMultiplayerServer = new Level_Multiplayer.Server(config);
            LevelMultiplayerServer.Start();
        }
    }
}
