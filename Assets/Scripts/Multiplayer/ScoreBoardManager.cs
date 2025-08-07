using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Manages the players' scores and handles scoreboard updates across the network.
/// Tracks connected players, updates scores, and determines the game winner.
/// </summary>
public class ScoreBoardManager : NetworkBehaviour
{
    [Header("🧭 Singleton")]
    [Tooltip("Singleton instance for accessing the ScoreBoardManager globally.")]
    public static ScoreBoardManager Instance { get; private set; }

    [Header("📊 Player Score List")]
    [Tooltip("A networked list of player statistics (ID and score).")]
    private NetworkList<PlayerStats> networkPlayerList = new NetworkList<PlayerStats>();

    /// <summary>
    /// Sets up the singleton instance and registers the list change callback.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Called whenever the player list is modified (e.g., score change, player added)
        networkPlayerList.OnListChanged += OnPlayerListChanged;
    }

    /// <summary>
    /// Called when the network player list changes.
    /// It collects all current player stats and updates the scoreboard UI.
    /// </summary>
    /// <param name="changeEvent">The event detailing the change.</param>
    private void OnPlayerListChanged(NetworkListEvent<PlayerStats> changeEvent)
    {
        List<PlayerStats> playerList = new List<PlayerStats>();

        foreach (var player in networkPlayerList)
        {
            playerList.Add(player);
            ScoreBoardUI.Instance.UpdateScoreBoard(playerList);
        }
    }

    /// <summary>
    /// Registers a callback to add new players when they connect to the server.
    /// </summary>
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += HandlePlayerConnected;
        }
    }

    /// <summary>
    /// Adds a new player to the scoreboard when they connect.
    /// </summary>
    /// <param name="playerId">The unique network ID of the connected player.</param>
    private void HandlePlayerConnected(ulong playerId)
    {
        PlayerStats newPlayer = new PlayerStats
        {
            playerId = playerId,
            score = 0
        };

        networkPlayerList.Add(newPlayer);
    }

    /// <summary>
    /// Server-side RPC to increase a player's score by a specified value.
    /// </summary>
    /// <param name="playerId">The ID of the player whose score should be increased.</param>
    /// <param name="scoreIncrease">The amount to add to the player's score.</param>
    [Rpc(SendTo.Server)]
    public void IncreasePlayerScoreRpc(ulong playerId, int scoreIncrease)
    {
        for (int i = 0; i < networkPlayerList.Count; i++)
        {
            if (networkPlayerList[i].playerId == playerId)
            {
                var player = networkPlayerList[i];
                player.score += scoreIncrease;
                networkPlayerList[i] = player; // Must reassign to trigger change event
                break;
            }
        }
    }

    /// <summary>
    /// Determines the player with the highest score and returns their name.
    /// </summary>
    /// <returns>Winner’s name followed by "won!", or fallback messages if unavailable.</returns>
    public string GetWinnerName()
    {
        if (networkPlayerList.Count == 0)
        {
            return "No Players";
        }

        PlayerStats topPlayer = networkPlayerList[0];

        foreach (var player in networkPlayerList)
        {
            if (player.score > topPlayer.score)
            {
                topPlayer = player;
            }
        }

        // Try to get the winner's name using their networked player object
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(topPlayer.playerId, out var client) &&
            client.PlayerObject != null && client.PlayerObject.TryGetComponent(out PlayerName playerNameComponent))
        {
            return $"{playerNameComponent.GetPlayerName()} won!";
        }

        return "Unknown player won!";
    }
}
