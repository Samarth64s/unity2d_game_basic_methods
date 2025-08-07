using TMPro;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// GameManager handles the core game flow: starting, timing, spawning coins, ending the game,
/// and updating UI and networked states. It works as the central controller for the networked coin collection game.
/// </summary>
public class GameManager : NetworkBehaviour
{
    [Header("🔁 Singleton Instance")]
    [Tooltip("Static instance of GameManager for global access.")]
    public static GameManager instance { get; private set; }

    [Header("⏱️ Game Settings")]
    [Tooltip("Total time for the game (networked variable).")]
    private NetworkVariable<float> gameTIme = new NetworkVariable<float>(10f);

    [Tooltip("Indicates whether the game is currently active.")]
    private bool gameActive = false;

    [Header("📺 UI Elements")]
    [Tooltip("Text element to display game information.")]
    [SerializeField] private TextMeshProUGUI gameInfoText;

    [Header("🪙 Coin Spawning")]
    [Tooltip("Prefab of the coin object to spawn.")]
    [SerializeField] private GameObject coinPrefab;

    [Tooltip("Minimum XY coordinates for the coin spawn area.")]
    [SerializeField] private Vector2 spawnAreaMin;

    [Tooltip("Maximum XY coordinates for the coin spawn area.")]
    [SerializeField] private Vector2 spawnAreaMax;

    [Tooltip("Interval (in seconds) between coin spawns.")]
    [SerializeField] private float spawnInterval = 1f;

    // Setup the singleton instance
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    // Called once before the first frame update
    void Start()
    {
        gameInfoText.text = "Press Enter To Start!";
        gameTIme.OnValueChanged += UpdateGameInfoText;
    }

    // Called every frame
    private void Update()
    {
        // Host triggers the game start with Enter
        if (IsHost && Input.GetKeyDown(KeyCode.Return) && !gameActive)
        {
            StartGame();
        }

        // Server controls the game timer countdown
        if (!IsServer || !gameActive) return;

        gameTIme.Value -= Time.deltaTime;

        // End game when timer hits 0
        if (gameTIme.Value <= 0)
        {
            EndGame();
        }
    }

    /// <summary>
    /// Starts the game by setting the timer, enabling active state, and spawning coins.
    /// Only the server can trigger this.
    /// </summary>
    void StartGame()
    {
        if (!IsServer) return;

        Debug.Log("Game Started");

        gameTIme.Value = 60f;
        SetGameActiveRpc(true);
        InvokeRepeating(nameof(SpawnCoin), 0f, spawnInterval);
    }

    /// <summary>
    /// RPC to update all clients about the game's active state.
    /// Also updates the UI text accordingly.
    /// </summary>
    [Rpc(SendTo.ClientsAndHost)]
    void SetGameActiveRpc(bool active)
    {
        gameActive = active;
        gameInfoText.text = active ? $"{gameTIme.Value:F1}" : "Game Over!";
    }

    /// <summary>
    /// Spawns a coin at a random position within the defined area and spawns it as a networked object.
    /// </summary>
    private void SpawnCoin()
    {
        if (!gameActive) return;

        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject coinInstance = Instantiate(coinPrefab, randomPosition, Quaternion.identity);

        var networkObject = coinInstance.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            networkObject.Spawn();
        }
    }

    /// <summary>
    /// Ends the game, stops coin spawning, removes all existing coins,
    /// and announces the winner across clients.
    /// </summary>
    void EndGame()
    {
        gameTIme.Value = 0;
        SetGameActiveRpc(false);
        CancelInvoke(nameof(SpawnCoin));

        Coin[] coins = FindObjectsByType<Coin>(FindObjectsSortMode.None);
        foreach (Coin coin in coins)
        {
            coin.DestroyCoinRpc();
        }

        UpdateWinnerRpc();
    }

    /// <summary>
    /// RPC to announce the winner using the ScoreBoardManager and update the UI.
    /// </summary>
    [Rpc(SendTo.ClientsAndHost)]
    void UpdateWinnerRpc()
    {
        string winnerText = ScoreBoardManager.Instance.GetWinnerName();
        gameInfoText.text = winnerText;
    }

    /// <summary>
    /// Updates the game info UI text whenever the game time value changes.
    /// </summary>
    void UpdateGameInfoText(float previousTime, float newTime)
    {
        if (gameActive)
        {
            gameInfoText.text = $"{newTime:F1}";
        }
    }
}
