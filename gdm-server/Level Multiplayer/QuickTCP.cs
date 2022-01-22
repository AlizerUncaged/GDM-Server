using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer {
    public static class QuickTCP {
        public static async Task<string> ReadURL(string path) {
            var data = await GetDataAsync(path);
            var stringed = Encoding.UTF8.GetString(data);
            return stringed;
        }
        public static async Task<byte[]> GetDataAsync(string url) {
            using (WebClient client = new WebClient()) {
                var s = await client.DownloadDataTaskAsync(url);
                return s;
            }
        }
        // everything the master server respond is json
        public static async Task<T> GetDataAsTypeAsync<T>(string url) {
            var response = await ReadURL(url);
            var toJsoned = JsonConvert.DeserializeObject<T>(response);
            return toJsoned;
        }

    }
}
