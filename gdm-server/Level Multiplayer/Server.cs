using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gdm_server.Level_Multiplayer
{
    public class Server
    {
        /// <summary>
        /// The reader thread.
        /// </summary>
        private Thread ServerReader;
        private bool _keepReading = true;

        /// <summary>
        /// PlayerID and the Client class of the players connected to the server.
        /// </summary>
        private Dictionary<int, Client.Client> Clients = new Dictionary<int, Client.Client>();
        
        private IPEndPoint ServerEndpoint;
        private UdpClient ServerSocket;
        private System.Timers.Timer
            ClientsRefresher = new System.Timers.Timer { Interval = 1000 };
        /// <summary>
        /// Keep track of how many bytes are send in and out. It just looks cool.
        /// </summary>
        private long BytesOutAndInt = 0;

        /// <summary>
        /// Start the server reader.
        /// </summary>
        public void Start()
        {
            // initialize the server refresher timer
            ClientsRefresher.Elapsed += ClientsRefresher_Elapsed;
            ClientsRefresher.Start();

            // bind to socket
            ServerEndpoint = new IPEndPoint(IPAddress.Parse(Config.Global.IP), Config.Global.Port);
            ServerSocket = new UdpClient(ServerEndpoint);

            ServerReader = new Thread(() => {

                // the sender's endpoint would be stored here
                IPEndPoint Sender = new IPEndPoint(IPAddress.Any, 0);

                Utils.ConsoleLog.Write("Server is now listening on port " + Config.Global.Port.ToString() + "!", Utils.LogLevel.Off, ConsoleColor.Yellow);

                while (_keepReading) {

                    try
                    {
                        var ReceivedBuffer = ServerSocket.Receive(ref Sender);

                        // make sure the buffer is not trolling
                        if (ReceivedBuffer is not null && ReceivedBuffer.Length > 0)
                        {
                            BytesOutAndInt += ReceivedBuffer.Length; // add length of buffer to byte count.
                            var PParser = new Packets.Parser(ReceivedBuffer);
                            var Prefix = PParser.GetPrefix();

                            // get player auth data
                            int PlayerID = PParser.ReadInt32();
                            int Key = PParser.ReadInt32();

                            Client.Client client;
                            // now check if the client already exist
                            if (Clients.TryGetValue(PlayerID, out client))
                            {
                                // the client already exists!

                                // now lets check the key
                                if (!client.CheckKey(Key) && client.IsAuthenticated)
                                {
                                    SendDisconnect(Sender, "Bad key! Your account is already being used on Multiplayer.");
                                    return;
                                }
                                else; // do nothing if the client has been found on the list and key is correct
                            }

                            // parse the buffer if everything else is OK
                            switch (Prefix)
                            {
                                case Packets.Prefix.Hello:
                                    /// the client doesn't exist since the client 
                                    /// was not on the list, hence add it
                                    client = new Client.Client(
                                        PlayerID, // player id
                                        Key       // client key
                                        /// now that the client key is stored
                                        /// any request sent of the same player
                                        /// id but different key will be discarded
                                        /// the key only changes whenever the client
                                        /// joins.
                                        );
                                    // add client to list if not in here yet
                                    if (!Clients.ContainsKey(PlayerID))
                                        Clients.Add(PlayerID, client);
                                    break;
                                case Packets.Prefix.Ping:
                                    /// a ping is received, thus
                                    /// we refresh the timeout
                                    client.TimeoutCount = 0;
                                    break;
                                case Packets.Prefix.Disconnect:
                                    /// set the player to be disconnected on clients refresh
                                    client.ToBeRemoved = true;
                                    Utils.ConsoleLog.Write(client.PlayerID.ToString() + " requests disconnection.", Utils.LogLevel.Off, ConsoleColor.DarkRed);
                                    break;
                                case Packets.Prefix.Message:
                                    // player 1
                                    client.Player1 = new Client.PlayerPosition();
                                    client.Player1.X = PParser.ReadInt32();
                                    client.Player1.Y = PParser.ReadInt32();
                                    client.Player1.XR = PParser.ReadInt32();
                                    client.Player1.YR = PParser.ReadInt32();
                                    client.Player1.Form = PParser.ReadByte();
                                    client.Player1.ActiveIconID = PParser.ReadByte();
                                    client.Player1.Size = PParser.ReadInt32();
                                    client.Player1.Gravity = PParser.ReadByte();
                                    // player 2
                                    client.Player2 = new Client.PlayerPosition();
                                    client.Player2.X = PParser.ReadInt32();
                                    client.Player2.Y = PParser.ReadInt32();
                                    client.Player2.XR = PParser.ReadInt32();
                                    client.Player2.YR = PParser.ReadInt32();
                                    client.Player2.Form = PParser.ReadByte();
                                    client.Player2.ActiveIconID = PParser.ReadByte();
                                    client.Player2.Size = PParser.ReadInt32();
                                    client.Player2.Gravity = PParser.ReadByte();
                                    // player data and cosmetics
                                    client.IsDead = PParser.ReadByte();
                                    client.LevelID = PParser.ReadInt32();
                                    client.Room = PParser.ReadInt16();
                                    client.Color1 = PParser.ReadByte();
                                    client.Color2 = PParser.ReadByte();
                                    client.IsGlow = PParser.ReadByte();
                                    client.IconIDs = PParser.ReadBytes(7).ToArray();

                                    break;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Utils.ConsoleLog.Write(ex.ToString(), Utils.LogLevel.Warn, ConsoleColor.Red);
                    }

                }
                Utils.ConsoleLog.Write("Server reader stopped.", Utils.LogLevel.All, ConsoleColor.Red);
            });


            ServerReader.Start();
        }

        /// <summary>
        /// Gets called every 1 second to refresh the state of the clients.
        /// </summary>
        private void ClientsRefresher_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (var Client in Clients.Values.ToList() /* required because lists in foreach are readonly */) {
                ++Client.TimeoutCount;
                if (Client.TimeoutCount > Config.Global.MaxTimeoutSeconds || Client.ToBeRemoved)
                {
                    SendToClientsOfLevelID(Packets.PacketFactory.SendPlayerDisconnect(Client.PlayerID), Client);
                    Clients.Remove(Client.PlayerID);
                }
            }
        }

        /// <summary>
        /// Sends buffer to players of the same level id as the owner.
        /// </summary>
        public void SendToClientsOfLevelID(byte[] buffer, Client.Client owner) {
            var PlayersOnLevel = Clients.Values.Where(
                x =>
                x.LevelID > 0 &&
                x.PlayerID != owner.PlayerID &&
                !x.ToBeRemoved &&
                x.LevelID == owner.LevelID
                );
            foreach (var Client in PlayersOnLevel)
            {
                Client.WriteBytes(buffer, ServerSocket);
            }
        }
        /// <summary>
        /// Send disconnect to an endpoint without the need of a Client class.
        /// </summary>
        public void SendDisconnect(IPEndPoint ipep, string message) {
            var buffer = Packets.PacketFactory.SendDisconnectMessage(message);
            ServerSocket.SendAsync(buffer, buffer.Length, ipep);
        }
        /// <summary>
        /// Stops the server reader.
        /// </summary>
        public void Stop() {
            _keepReading = false;

            // for the love of god please let me abort the thread.
            try
            {
                ServerReader.Abort();
            }
            catch { }
        }
    }
}
