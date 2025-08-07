using TMPro;
using UnityEngine;

/// <summary>
/// Handles the UI display for a single player's entry in the scoreboard,
/// showing their name and score.
/// </summary>
public class PlayerEntry : MonoBehaviour
{
    [Header("👤 Player UI Elements")]

    [Tooltip("Text component to display the player's name.")]
    [SerializeField] private TextMeshProUGUI playerName;

    [Tooltip("Text component to display the player's score.")]
    [SerializeField] private TextMeshProUGUI playerScore;

    /// <summary>
    /// Sets the player's name and score in the UI.
    /// </summary>
    /// <param name="name">The name of the player.</param>
    /// <param name="score">The score of the player.</param>
    public void SetPlayerEntry(string name, int score)
    {
        playerName.text = name;
        playerScore.text = score.ToString();
    }
}
