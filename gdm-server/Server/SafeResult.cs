using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Server {
    /// <summary>
    /// Prevent our dumbass self from wrapping everything in try catch.
    /// </summary>
    public struct SafeResult<T> {
        public bool WasSuccess;
        public string Reason;
        public T Result;
    }
}
