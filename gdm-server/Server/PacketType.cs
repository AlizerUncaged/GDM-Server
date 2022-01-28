namespace gdm_server.Server;


public enum PacketType : byte
{
    /// <summary>
    /// For player change in position.
    /// </summary>
    PlayerMove,
    /// <summary>
    /// When the player dies.
    /// </summary>
    PlayerDies,
    /// <summary>
    /// When the player enters a portal.
    /// </summary>
    PlayerEnteredPortal,
    /// <summary>
    /// When the player exits a level.
    /// </summary>
    PlayerExitLevel,
    /// <summary>
    /// When the player quits.
    /// </summary>
    PlayerExitGame
}
