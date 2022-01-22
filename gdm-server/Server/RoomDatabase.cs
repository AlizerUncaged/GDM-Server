using gdm_server.Level_Multiplayer.Client;
using gdm_server.Rooms;
using gdm_server.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdm_server.Server {
    public static class RoomDatabase {

        /// <summary>
        /// All rooms in the server.
        /// </summary>
        public static List<Room> Rooms = new();

        public static void SetPlayerRoom(ref Room room, ref Player player) {
            player.Room = room;
            room.Players.Add(player);
        }

        public static SafeResult<Room> GetOrCreateRoom(AccountJoinRoomRequest joinRequest) {
            // vip has already been checked
            var player = joinRequest.Player;
            var roomID = joinRequest.RoomID;
            var room = GetRoom(roomID);
            if (room is null) {
                if (player.IsVip)
                    AddRoom(new Room {
                        RoomID = roomID,
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
}
