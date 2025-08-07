using System;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Manages the player's name in a multiplayer networked game using Unity Netcode.
/// This script synchronizes the player's name across all clients using a NetworkVariable,
/// updates the name in the UI, and listens for any name changes in real-time.
/// </summary>
public class PlayerName : NetworkBehaviour
{
    [Header("🎮 UI Reference")]
    [Tooltip("TextMeshPro component displaying the player's name above the character.")]
    [SerializeField] private TextMeshPro playerName;

    [Header("🌐 Network Variable")]
    [Tooltip("Network-synced player name visible to all clients.")]
    public NetworkVariable<FixedString32Bytes> networkPlayerName =
        new NetworkVariable<FixedString32Bytes>("Unknown",
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    /// <summary>
    /// Event triggered whenever the player's name changes.
    /// </summary>
    public event Action<string> OnNameChanged;

    /// <summary>
    /// Called when the object is spawned in the network.
    /// Sets the player name from UI if this is the local player,
    /// subscribes to value changes, and updates UI display.
    /// </summary>
    public override void OnNetworkSpawn()
    {
        // If this player owns the object, get the name from input field and assign it to the network variable
        if (IsOwner)
        {
            string inputName = FindFirstObjectByType<UIManager>()
             .GetComponent<UIManager>()
             .nameInputField.text;

            networkPlayerName.Value = new FixedString32Bytes(inputName);
        }

        // Set the name display in the world
        playerName.text = networkPlayerName.Value.ToString();

        // Subscribe to changes in the network variable
        networkPlayerName.OnValueChanged += NetworkPlayerName_OnValueChanged;

        // Invoke the OnNameChanged event
        OnNameChanged?.Invoke(networkPlayerName.Value.ToString());
    }

    /// <summary>
    /// Callback when the player name changes on the network.
    /// Updates the displayed name and notifies listeners.
    /// </summary>
    private void NetworkPlayerName_OnValueChanged(FixedString32Bytes previousValue, FixedString32Bytes newValue)
    {
        // Update the visible name above the player
        playerName.text = newValue.ToString();

        // Notify other systems about the name change
        OnNameChanged?.Invoke(newValue.Value);
    }

    /// <summary>
    /// Returns the current player name.
    /// </summary>
    /// <returns>String representing the player's name.</returns>
    public string GetPlayerName()
    {
        return networkPlayerName.Value.ToString();
    }
}
