using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Handles coin interaction in the game. When a player collides with the coin,
/// it increases their score on the server and despawns the coin.
/// </summary>
public class Coin : NetworkBehaviour
{

    [Tooltip("Triggered when any collider enters the coin's 2D trigger zone.")]

    /// <summary>
    /// Detects collision with the player. If a player collides with this coin,
    /// increase their score and destroy the coin.
    /// </summary>
    /// <param name="collision">The collider that entered the trigger.</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get the unique player ID from the colliding player object
            ulong playerId = collision.GetComponent<NetworkObject>().OwnerClientId;

            // Increase the score of the player by 1
            ScoreBoardManager.Instance.IncreasePlayerScoreRpc(playerId, 1);

            // Destroy this coin across the network
            DestroyCoinRpc();
        }
    }


    [Tooltip("Despawns the coin from the network and destroys the game object.")]

    /// <summary>
    /// Destroys the coin object both on the network and locally.
    /// Should be called from the server using RPC.
    /// </summary>
    [Rpc(SendTo.Server)]
    public void DestroyCoinRpc()
    {
        // Despawn the coin from the network and destroy the GameObject
        GetComponent<NetworkObject>().Despawn(true);
        Destroy(gameObject);
    }
}
