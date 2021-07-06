using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Utils
{
    public static class ConsoleLog
    {
        /// <summary>
        /// Writes a line to console.
        /// </summary>
        /// <param name="message">Message to print.</param>
        /// <param name="forecolor">Foreground color of the message.</param>
        public static void Write(string message, Utils.LogLevel level = Utils.LogLevel.Off, ConsoleColor forecolor = ConsoleColor.Gray)
        {
            if ((int)level <= Config.LogLevel)
            {
                if ((int)Config.LogLevel > 0)
                    FileLog.Instance.Log(message, level);

                Console.ForegroundColor = forecolor;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.Gray;

            }
        }
    }
}
