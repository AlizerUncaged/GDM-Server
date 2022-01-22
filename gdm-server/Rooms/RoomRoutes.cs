using gdm_server.Level_Multiplayer.Watson;
using gdm_server.MainServer;
using gdm_server.Rooms.Models;
using gdm_server.Server;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WatsonWebserver;

namespace gdm_server.Rooms {
    public class RoomRoutes {
        // the only route for adding or settings players
        [StaticRoute(HttpMethod.POST, "/add")]
        public async Task GetHelloRoute(HttpContext ctx) {
            var request = ctx.Request.DataAsString;
            var accountJoinRoomRequest = JsonConvert.DeserializeObject<AccountJoinRoomRequest>(request);
            // deserialize
            var playerProfile = accountJoinRoomRequest.Player;
            var roomID = accountJoinRoomRequest.RoomID;
            var vipKey = accountJoinRoomRequest.VipKey; // optional required only when VIP
            // check or add player on database server
            // check if vip
            var playerID = playerProfile.PlayerID;
            var playerAccountInfo = new PlayerDataRequest(playerID);
            var playerStatus = await playerAccountInfo.GetPlayerData();
            if (playerStatus.IsVip) {
                var isVipValidRequest = new ValidateVipRequest(playerID, vipKey);
                var isVipValid = await isVipValidRequest.IsKeyValid();
                if (!isVipValid) {
                    // invalid key
                    await ctx.RespondObjectAsJson(new AccountJoinRoomResponse {
                        IsSuccess = false,
                        Reason = "Invalid VIP key! Please change it on settings."
                    });
                    return;
                }
            }
            // vip was valid or not vip
            // get or craete the room first
            var getRoomResult = RoomDatabase.GetOrCreateRoom(accountJoinRoomRequest);
            if (!getRoomResult.WasSuccess) {
                await ctx.RespondObjectAsJson(new AccountJoinRoomResponse {
                    IsSuccess = false,
                    Reason = getRoomResult.Reason
                });
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
            return;
        }
    }
}
