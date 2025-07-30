using UnityEngine;

/// <summary>
/// Handles player jump mechanics including ground check, variable jump height,
/// and gravity manipulation during jump and fall.
/// </summary>
public class JumpController : MonoBehaviour
{
    // Rigidbody2D component reference
    Rigidbody2D playerRigidbody;

    [Header("Jump Settings")]
    [Tooltip("Maximum duration for which the jump button can be held.")]
    [SerializeField] float maxJumpHoldTime = 0.2f;

    [Tooltip("Initial force applied when the jump starts.")]
    [SerializeField] int jumpForce = 10;

    [Tooltip("Multiplier to increase gravity when falling.")]
    [SerializeField] float fallGravityMultiplier = 2f;

    [Tooltip("Multiplier to control upward motion during variable jump.")]
    [SerializeField] float jumpControlMultiplier = 1f;

    [Header("Ground Check Settings")]
    [Tooltip("Transform used to check if the player is on the ground.")]
    public Transform groundCheckTransform;

    [Tooltip("Layer used to identify ground objects.")]
    public LayerMask groundLayer;

    // Internal state
    Vector2 gravityDirection;
    bool isJumping;
    float currentJumpTime;

    // Called once when the script instance is being loaded
    void Start()
    {
        gravityDirection = new Vector2(0, -Physics2D.gravity.y);
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Called once per frame
    void Update()
    {
        // Start jump
        if (Input.GetKeyDown(KeyCode.Space) && IsPlayerGrounded())
        {
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, jumpForce);
            isJumping = true;
            currentJumpTime = 0;
        }

        // Handle variable jump height
        if (playerRigidbody.linearVelocity.y > 0 && isJumping)
        {
            currentJumpTime += Time.deltaTime;
            if (currentJumpTime > maxJumpHoldTime) isJumping = false;

            float timeRatio = maxJumpHoldTime > 0 ? currentJumpTime / maxJumpHoldTime : 0f;
            float adjustedJumpMultiplier = jumpControlMultiplier;

            if (timeRatio > 0.5f)
            {
                adjustedJumpMultiplier *= (1 - timeRatio);
            }

            playerRigidbody.linearVelocity += gravityDirection * adjustedJumpMultiplier * Time.deltaTime;
        }

        // Handle jump release
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            currentJumpTime = 0;

            if (playerRigidbody.linearVelocity.y > 0)
            {
                playerRigidbody.linearVelocity = new Vector2(
                    playerRigidbody.linearVelocity.x,
                    playerRigidbody.linearVelocity.y * 0.6f
                );
            }
        }

        // Apply extra gravity when falling
        if (playerRigidbody.linearVelocity.y < 0)
        {
            playerRigidbody.linearVelocity -= gravityDirection * fallGravityMultiplier * Time.deltaTime;
        }
    }

    /// <summary>
    /// Checks if the player is currently standing on a ground surface.
    /// </summary>
    /// <returns>True if grounded, false otherwise.</returns>
    bool IsPlayerGrounded()
    {
        return Physics2D.OverlapCapsule(
            groundCheckTransform.position,
            new Vector2(0.19f, 0.09f),
            CapsuleDirection2D.Vertical,
            0f,
            groundLayer
        );
    }
}
