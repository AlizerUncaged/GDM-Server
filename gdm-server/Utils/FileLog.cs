using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Utils
{
    public class FileLog
    {
        /// <summary>
        /// This will be set when log level is not 0.
        /// </summary>
        public static FileLog Instance;

        private FileStream logfile;

        /// <summary>
        /// Create a new instance of logger.
        /// </summary>
        /// <param name="log_path">File path to log file, will append logs there.</param>
        public FileLog(string log_path)
        {
            string filename = Config.LogFile.Replace("%date%", DateTime.UtcNow.ToString("yyyy-dd-M--HH-mm-ss"));
            // check first if the directory exists, if not create it
            if (!Directory.Exists(Path.GetDirectoryName(filename))) Directory.CreateDirectory(filename);

            // initialize the log file
            logfile = new FileStream(filename,
                     FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        }

        /// <summary>
        /// Write the log to the file.
        /// </summary>
        public void Log(string message, Utils.LogLevel level)
        {
            // wtf o.o
            var log = "[" + (int)level + "] [" + DateTime.UtcNow.ToString("yyyy-dd-M--HH-mm-ss") + "] " + message + Environment.NewLine;
            var bytes = Encoding.Default.GetBytes(log);
            logfile.Write(bytes, 0, bytes.Length);
            logfile.Flush();
        }
    }
}
