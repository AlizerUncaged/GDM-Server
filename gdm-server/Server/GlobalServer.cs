using gdm_server.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gdm_server.Server {
    public class GlobalServer {

        /// <summary>
        /// The reader thread.
        /// </summary>
        private Thread ServerReader;
        private bool _keepReading = true;


        private IPEndPoint ServerEndpoint;
        private UdpClient ServerSocket;

        /// <summary>
        /// Start the server reader.
        /// </summary>
        public void Start() {

            Program.Logger.Info($"Starting Server on Port {Config.Port}");

            // bind to socket
            ServerEndpoint = new IPEndPoint(IPAddress.Parse(Config.IP), Config.Port);
            ServerSocket = new UdpClient(ServerEndpoint);

            ServerReader = new Thread(() => {
                while (_keepReading) {
                    // read thread, never block with outputs 
                    // the sender's endpoint would be stored here
                    IPEndPoint Sender = new IPEndPoint(IPAddress.Any, 0);

                    var ReceivedBuffer = ServerSocket.Receive(ref Sender);
                    if (ReceivedBuffer.Length > 0) {
                        // received some shit
                        var packet = new Packet(ReceivedBuffer);
                        // ignore packet if invalid
                        if (packet.WasInvalid) continue;
                    }
                }
            });
            ServerReader.Start();
        }
    }
}
