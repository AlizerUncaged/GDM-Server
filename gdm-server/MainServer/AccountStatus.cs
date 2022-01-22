using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.MainServer {
    public struct AccountStatus {
        public bool IsVip { get; set; }
        public override string ToString() {
            var jsoned = JsonConvert.SerializeObject(this, Formatting.Indented);
            return jsoned;
        }
    }
}
