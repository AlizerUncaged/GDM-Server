using System.Collections.Generic;
using gdm_server.Level_Multiplayer.Client;

namespace gdm_server.Server;

public class PlayerEqualityComparer : IEqualityComparer<Player>
{
    public bool Equals(Player x, Player y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.PlayerId == y.PlayerId;
    }

    public int GetHashCode(Player obj)
    {
        return obj.PlayerId;
    }
}