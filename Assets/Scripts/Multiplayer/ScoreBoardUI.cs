using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// ScoreBoardUI manages the scoreboard UI in the game.
/// It displays player names and scores using UI entries and updates them in real-time.
/// </summary>
public class ScoreBoardUI : MonoBehaviour
{
    [Header("📦 UI Prefabs")]

    [Tooltip("Prefab for each player's entry in the scoreboard.")]
    [SerializeField] private GameObject playerEntryPrefab;

    [Header("🧭 Singleton Instance")]

    [Tooltip("Singleton reference for accessing ScoreBoardUI globally.")]
    public static ScoreBoardUI Instance { get; private set; }

    [Header("📊 Score Data")]

    [Tooltip("Dictionary to keep track of player entries by their player ID.")]
    private Dictionary<ulong, PlayerEntry> playerEntries = new Dictionary<ulong, PlayerEntry>();

    /// <summary>
    /// Ensures a single instance of ScoreBoardUI exists.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Updates the scoreboard UI based on the provided list of PlayerStats.
    /// It also listens for player name changes via the PlayerName component.
    /// </summary>
    /// <param name="playerList">List of player statistics to display.</param>
    public void UpdateScoreBoard(List<PlayerStats> playerList)
    {
        foreach (var player in playerList)
        {
            string playerName = "Unknown";

            // Try to get the player's name from their connected client
            if (NetworkManager.Singleton.ConnectedClients.TryGetValue(player.playerId, out var client)
                && client.PlayerObject != null)
            {
                if (client.PlayerObject.TryGetComponent(out PlayerName playerNameComponent))
                {
                    playerName = playerNameComponent.GetPlayerName();

                    // Subscribe to name change events for real-time UI update
                    playerNameComponent.OnNameChanged += (playerName) =>
                        UpdatePlayerEntry(player.playerId, playerName, player.score);
                }
            }

            // Update or create the UI entry for the player
            UpdatePlayerEntry(player.playerId, playerName, player.score);
        }
    }

    /// <summary>
    /// Updates the player's UI entry if it exists; otherwise, creates a new one.
    /// </summary>
    /// <param name="playerId">Unique network ID of the player.</param>
    /// <param name="playerName">Name of the player to display.</param>
    /// <param name="score">Score of the player to display.</param>
    private void UpdatePlayerEntry(ulong playerId, string playerName, int score)
    {
        // Check if entry already exists; if not, instantiate a new one
        if (!playerEntries.TryGetValue(playerId, out var entry))
        {
            GameObject playerEntryObject = Instantiate(playerEntryPrefab, transform);
            entry = playerEntryObject.GetComponent<PlayerEntry>();
            playerEntries[playerId] = entry;
        }

        // Update the entry with current player name and score
        entry.SetPlayerEntry(playerName, score);
    }
}
