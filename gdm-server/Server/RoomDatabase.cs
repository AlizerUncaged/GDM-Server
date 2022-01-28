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
    public static List<Room> Rooms = new();

    /// <summary>
    /// Get the player and room from session key.
    /// </summary>
    public static Dictionary<int, Player> SessionKeyAndData = new();

    public static void SetPlayerRoom(ref Room room, ref Player player) {
        player.Room = room;
        room.Players.Add(player);
    }

    public static SafeResult<Room> GetOrCreateRoom(AccountJoinRoomRequest joinRequest) {
        // vip has already been checked
        var player = joinRequest.Player;
        var roomId = joinRequest.RoomID;
        var room = GetRoom(roomId);
        if (room is null) {
            // if room does not exist just make it.
            if (player.IsVip)
                AddRoom(new Room {
                    RoomID = roomId,
                    Gamemode = Gamemodes.Normal,
                    RoomPassword = String.Empty,
                    RoomName = $"{player.Username}'s Room"
                });
            else
                return new SafeResult<Room> {
                    WasSuccess = false,
                    Reason = "The lobby doesn't exists, you need VIP to create your own lobby."
                };
        }

        // set room.
        player.Room = room;
        // cache the player's session key.
        SessionKeyAndData[player.SessionKey] = joinRequest.Player;
            
        return new SafeResult<Room> {
            WasSuccess = true,
            Result = room
        };
    }

    public static Room GetRoom(long roomID) {
        return Rooms.FirstOrDefault(x => x.RoomID == roomID);
    }

    public static void AddRoom(Room room) {
        Rooms.Add(room);
    }
}