using UnityEngine;
using TMPro;

public class CircleController : MonoBehaviour
{
    [Header("Rigidbody Components")]
    [Tooltip("Rigidbody2D component of the Player GameObject")]
    Rigidbody2D playerRB;

    [Tooltip("Rigidbody2D component of the Enemy GameObject")]
    Rigidbody2D enemyRB;

    [Header("Movement Settings")]
    [Tooltip("Speed at which the enemy moves towards the player")]
    public int speed;

    [Header("Game Objects")]
    [Tooltip("Reference to the Player GameObject")]
    public GameObject player;

    [Tooltip("Reference to the Enemy GameObject")]
    public GameObject enemy;

    [Header("Distance Display")]
    [Tooltip("UI Text element to display the distance between player and enemy")]
    public TextMeshProUGUI distanceText;

    float distance;

    void Start()
    {
        // Get Rigidbody2D components of the player and enemy
        playerRB = player.GetComponent<Rigidbody2D>();
        enemyRB = enemy.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Calculate the distance between enemy and player
        distance = Vector2.Distance(enemy.transform.position, player.transform.position);

        // Update the UI text with the current distance (formatted to 2 decimal places)
        distanceText.text = "Distance: " + distance.ToString("F2");

        // If distance is greater than 2 units, enemy moves toward player
        if (distance > 2)
        {
            enemyRB.linearVelocity = (player.transform.position - enemy.transform.position).normalized * speed;
        }
        else
        {
            // Stop enemy movement if within 2 units of player
            enemyRB.linearVelocity = Vector2.zero;
        }

        // Uncomment below code if you want to move player manually using Space key
        /*
        if (Input.GetKey(KeyCode.Space))
        {
            playerRB.linearVelocity = vector.normalized * speed;  // Apply movement while space is held
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerRB.linearVelocity = Vector2.zero;  // Stop movement when key is released
        }
        */
    }
}
