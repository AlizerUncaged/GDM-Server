using System.Threading.Tasks;
using gdm_server.MainServer;
using gdm_server.Rooms.Models;

namespace gdm_server.Rooms;

public class AccountValidator
{
    private AccountJoinRoomRequest _request;
    public AccountValidator(AccountJoinRoomRequest request)
    {
        _request = request;
    }

    public async Task<bool> IsKeyValid()
    {
        var playerId = _request.Player.PlayerId;
        var playerAccountInfo = new PlayerDataRequest(playerId);
        var playerStatus = await playerAccountInfo.GetPlayerData();
        if (playerStatus.IsVip) {
            var isVipValidRequest = new ValidateVipRequest(playerId, _request.VipKey);
            var isVipValid = await isVipValidRequest.IsKeyValid();
            if (!isVipValid)
            {
                return false;
            }
        }

        return true;
    }
}