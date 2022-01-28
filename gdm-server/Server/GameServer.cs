using gdm_server.Rooms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gdm_server.Server;

public class GlobalServer {

    /// <summary>
    /// The reader thread.
    /// </summary>
    private Task _serverReader;
    private bool _keepReading = true;


    private IPEndPoint _serverEndpoint;
    private UdpClient _serverSocket;

    /// <summary>
    /// Sends data to endpoint.
    /// </summary>
    public async void WriteTo(IPEndPoint endpoint, byte[] bytes)
    {
        await _serverSocket.SendAsync(bytes, endpoint);
    }

    /// <summary>
    /// Start the server reader.
    /// </summary>
    public void Start() {

        Program.Logger.Info($"Starting Server on Port {Config.Port}");

        // bind to socket
        _serverEndpoint = new IPEndPoint(IPAddress.Parse(Config.IP), Config.Port);
        _serverSocket = new UdpClient(_serverEndpoint);

        _serverReader = Task.Run(async() => {
            while (_keepReading) {
                // read thread, never block with outputs 
                // the sender's endpoint would be stored here
                var received = await _serverSocket.ReceiveAsync();
                var receivedBuffer = received.Buffer;
                if (receivedBuffer.Length > 0) {
                    // received some shit
                    var packet = new Packet(receivedBuffer);
                    // ignore packet if invalid
                    if (packet.WasInvalid) continue;
                    // packet was valid, find the player who owns that session key.
                        
                    // session key already has value
                    // ReSharper disable once PossibleInvalidOperationException
                    var playerFromSessionKey = RoomDatabase.SessionKeyAndData[packet.SessionKey.Value];
                    // set the endpoint from the current fetched one.
                    playerFromSessionKey.PlayerEndpoint = received.RemoteEndPoint;
                    // broadcast to other players what happened.
                }
            }
        });
            
        _serverReader.Start();
    }
}