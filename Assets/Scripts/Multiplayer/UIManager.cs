using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages UI interactions for hosting or joining a multiplayer session,
/// including player name input and displaying game status messages.
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("📝 Player Input Field")]
    [Tooltip("Input field for the player to enter their name.")]
    [SerializeField] public TMP_InputField nameInputField;

    [Header("ℹ️ Game Info Display")]
    [Tooltip("Displays status messages like 'Waiting for host' or 'Press Enter to start'.")]
    [SerializeField] private TextMeshProUGUI gameInfoText;

    [Header("🖱️ Menu Buttons")]
    [Tooltip("Button used to host the game as server.")]
    [SerializeField] Button hostButton;

    [Tooltip("Button used to join an existing game as client.")]
    [SerializeField] Button joinButton;

    [Header("📋 UI Panels")]
    [Tooltip("Main menu panel containing input and buttons.")]
    [SerializeField] GameObject menu;

    /// <summary>
    /// Initializes button click listeners for host and join actions.
    /// </summary>
    void Start()
    {
        hostButton.onClick.AddListener(Host);
        joinButton.onClick.AddListener(Join);
    }

    /// <summary>
    /// Starts the game as host, hides menu, and shows start instructions.
    /// </summary>
    void Host()
    {
        NetworkManager.Singleton.StartHost();
        menu.SetActive(false);
        gameInfoText.gameObject.SetActive(true);
        gameInfoText.text = "Press Enter to start";
    }

    /// <summary>
    /// Joins the game as a client, hides menu, and shows waiting message.
    /// </summary>
    void Join()
    {
        NetworkManager.Singleton.StartClient();
        menu.SetActive(false);
        gameInfoText.gameObject.SetActive(true);
        gameInfoText.text = "Waiting for host.....";
    }
}
