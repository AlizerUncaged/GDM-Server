using gdm_server.Level_Multiplayer.Client;
using gdm_server.Rooms;
using gdm_server.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Server;

public static class RoomDatabase {

    /// <summary>
    /// All rooms in the server, be it level multiplayer or collab editor.
    /// </summary>
    private static readonly List<Room> Rooms = new();

    /// <summary>
    /// Get the player and room from session key.
    /// </summary>
    public static readonly Dictionary<int, Player> SessionKeyAndData = new();

    public static SafeResult<Room> GetOrCreateRoom(AccountJoinRoomRequest joinRequest) {
        // vip has already been checked
        var player = joinRequest.Player;
        var roomId = joinRequest.RoomID;
        var addRoomResult = GetOrAddRoom(player, roomId);
        if (!addRoomResult.WasSuccess)
            return addRoomResult;
        
        // set room.
        player.Room = addRoomResult.Result;
        if (!player.Room.Players.Contains(player, new PlayerEqualityComparer()))
            player.Room.Players.Add(player);
        
        // cache the player's session key.
        SessionKeyAndData[player.SessionKey] = joinRequest.Player;
        return new SafeResult<Room> {  WasSuccess = true, Result = player.Room};
    }

    private static SafeResult<Room> GetOrAddRoom(Player owner, long roomId)
    {
        var room = GetRoom(roomId);
        if (room is null) {
            var roomAddResult = AddRoom(owner, new Room
            {
                RoomID = roomId,
                Gamemode = Gamemodes.Normal,
                RoomPassword = String.Empty,
                RoomName = $"{owner.Username}'s Room"
            });
            return roomAddResult;
        }

        return new SafeResult<Room>{ WasSuccess = true};
    }

    private static Room GetRoom(long roomId) {
        return Rooms.FirstOrDefault(x => x.RoomID == roomId);
    }

    /// <summary>
    /// Creates a room.
    /// </summary>
    /// <param name="player">The room owner.</param>
    /// <param name="room">The room to add.</param>
    private static SafeResult<Room> AddRoom(Player player, Room room)
    {
        if (player.IsVip)
            Rooms.Add(room);
        else return new SafeResult<Room> {  WasSuccess = false, Reason = "You need VIP to create rooms."};
        return new SafeResult<Room> {  WasSuccess = true };
    }
}