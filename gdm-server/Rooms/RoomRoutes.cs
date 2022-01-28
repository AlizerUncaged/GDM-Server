using gdm_server.Level_Multiplayer.Watson;
using gdm_server.MainServer;
using gdm_server.Rooms.Models;
using gdm_server.Server;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WatsonWebserver;

namespace gdm_server.Rooms {
    public class RoomRoutes {
        public async void SendError(HttpContext ctx, string errorMessage)
        {    
            await ctx.RespondObjectAsJson(new AccountJoinRoomResponse {
                IsSuccess = false,
                Reason = errorMessage
            });
        }

        // add or set player room.
        [StaticRoute(HttpMethod.POST, "/api/add")]
        public async Task GetHelloRoute(HttpContext ctx) {
            var request = ctx.Request.DataAsString;
            var accountJoinRoomRequest = JsonConvert.DeserializeObject<AccountJoinRoomRequest>(request);
            // variables.
            var playerProfile = accountJoinRoomRequest.Player;
            var roomId = accountJoinRoomRequest.RoomID;
            var vipKey = accountJoinRoomRequest.VipKey; 
            // check or add player on database server
            // check if vip -------------------------------------------------------------
            var playerId = playerProfile.PlayerId;
            var playerAccountInfo = new PlayerDataRequest(playerId);
            var playerStatus = await playerAccountInfo.GetPlayerData();
            if (playerStatus.IsVip) {
                var isVipValidRequest = new ValidateVipRequest(playerId, vipKey);
                var isVipValid = await isVipValidRequest.IsKeyValid();
                if (!isVipValid) {
                    SendError(ctx, "Invalid VIP key! Please change it on settings.");
                    return;
                }
            }
            // vip was valid or not vip -------------------------------------------------
            // get or create the room first
            var getRoomResult = RoomDatabase.GetOrCreateRoom(accountJoinRoomRequest);
            if (!getRoomResult.WasSuccess) {
                SendError(ctx, getRoomResult.Reason);
                return;
            }
            var room = getRoomResult.Result;
            // add the player on it
            RoomDatabase.SetPlayerRoom(ref room, ref playerProfile);
            await ctx.RespondObjectAsJson(new AccountJoinRoomResponse {
                IsSuccess = true,
                FPS = Config.GlobalDefaultFPS,
                RoomID = room.RoomID,
                SessionKey = playerProfile.SessionKey
            });
        }
    }
}
