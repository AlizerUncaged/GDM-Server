using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatsonWebserver;

namespace gdm_server {
    public static class JsonHelpers {
        /// <summary>
        /// Serializes an object to Json and sends it to the client.
        /// </summary>
        public static async Task RespondObjectAsJson(this HttpContext context, object targetObj) {
            var serializedObject = JsonConvert.SerializeObject(targetObj, Formatting.Indented);
            // clean
            context.Response.ContentType = "application/json";
            await context.Response.Send(serializedObject);
        }
    }
}
