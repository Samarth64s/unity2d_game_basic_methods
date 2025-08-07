using System;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Represents a player's statistics in the game, including their unique player ID and score.
/// Implements INetworkSerializable for network syncing and IEquatable for comparison.
/// </summary>
public struct PlayerStats : INetworkSerializable, IEquatable<PlayerStats>
{
    [Header("👤 Player Information")]
    // The unique network ID of the player
    public ulong playerId;

    [Header("🎯 Player Score")]
    // The score earned by the player during the game
    public int score;

    /// <summary>
    /// Serializes player stats data over the network using Unity Netcode.
    /// Ensures that playerId and score are synced correctly across clients and server.
    /// </summary>
    /// <typeparam name="T">The serializer type implementing IReaderWriter</typeparam>
    /// <param name="serializer">Serializer used for reading/writing network data</param>
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerId);
        serializer.SerializeValue(ref score);
    }

    /// <summary>
    /// Compares two PlayerStats objects based on playerId.
    /// </summary>
    /// <param name="other">Another PlayerStats object to compare with</param>
    /// <returns>True if both have the same playerId; otherwise, false</returns>
    public bool Equals(PlayerStats other)
    {
        return playerId == other.playerId;
    }
}
