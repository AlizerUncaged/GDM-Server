using gdm_server.Level_Multiplayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.MainServer {
    public class DatabaseServerRequest {
        /// <summary>
        /// GDM-X-Database-Server
        /// </summary>
        public const string DataServerHostname = @"http://95.111.251.138:7101/";

        private string url;
        public DatabaseServerRequest(string endpoint, string getArgs) {
            url = $"{DataServerHostname}{(endpoint.StartsWith("/") ? endpoint.Substring(1) : endpoint)}?{getArgs}";
        }

        public async Task<IEnumerable<byte>> GetDataAsync() {
            return await QuickTCP.GetDataAsync(url);
        }

        // everything the master server respond is json
        public async Task<T> GetDataAsTypeAsync<T>() {
            var response = await GetDataAsync();
            var stringEncoded = Encoding.UTF8.GetString(response.ToArray());
            var toJsoned = JsonConvert.DeserializeObject<T>(stringEncoded);
            return toJsoned;
        }
    }
}
