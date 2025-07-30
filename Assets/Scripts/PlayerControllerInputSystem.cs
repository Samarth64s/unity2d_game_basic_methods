using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the player's movement and jumping using the new Input System.
/// </summary>
public class PlayerControllerInputSystem : MonoBehaviour
{
    // Rigidbody2D component for physics interactions
    Rigidbody2D rb;

    [Header("Movement Settings")]
    [Tooltip("Speed at which the player moves horizontally.")]
    public int speed = 5;

    [Tooltip("Force applied when the player jumps.")]
    public int jumpPower = 10;

    // Stores directional input from the player
    Vector2 vecMove;

    // Called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Reserved for non-physics input updates if needed
    }

    // Called when the player triggers the jump input
    public void Jump(InputAction.CallbackContext value)
    {
        Debug.Log("Jump");

        // Check if jump input was just pressed
        if (value.started)
        {
            // Apply upward velocity to jump
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            AudioManager.Instance.PlaySoundEffect("Jump");
        }
    }

    // Called when the player provides movement input
    public void Movement(InputAction.CallbackContext value)
    {
        Debug.Log("Movement");
        Debug.Log(value.ReadValue<Vector2>());

        // Read movement input and store it
        vecMove = value.ReadValue<Vector2>();

        // Flip the player sprite depending on direction
        flip();
    }

    // FixedUpdate is called on a fixed time interval, used for physics updates
    private void FixedUpdate()
    {
        // Apply horizontal velocity based on input
        rb.linearVelocity = new Vector2(vecMove.x * speed, rb.linearVelocity.y);
    }

    // Flips the character's sprite based on movement direction
    void flip()
    {
        // If moving left, flip to negative scale
        if (vecMove.x < -0.01f) transform.localScale = new Vector3(-4, 4, 1);

        // If moving right, flip to positive scale
        if (vecMove.x > 0.01f) transform.localScale = new Vector3(4, 4, 1);
    }
}
