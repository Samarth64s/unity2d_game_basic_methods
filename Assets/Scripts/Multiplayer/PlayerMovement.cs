using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Controls the player's movement using Rigidbody2D, restricted to the local player (owner).
/// Movement input is captured and applied only for the client that owns this player object.
/// </summary>
public class PlayerMovement : NetworkBehaviour
{
    [Header("🔁 Rigidbody Component")]
    [Tooltip("The Rigidbody2D component attached to the player.")]
    Rigidbody2D rb;

    [Header("🎮 Input Axes")]
    [Tooltip("Horizontal input value.")]
    float moveHorizontal;

    [Tooltip("Vertical input value.")]
    float moveVertical;

    [Header("🚀 Movement Settings")]
    [Tooltip("The speed at which the player moves.")]
    float moveSpeed = 10;

    [Tooltip("The current movement vector based on player input.")]
    Vector2 movement;

    /// <summary>
    /// Called once before the first frame update.
    /// Initializes the Rigidbody2D component.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Called at a fixed time interval.
    /// Handles movement input and applies movement via physics for the owner client only.
    /// </summary>
    void FixedUpdate()
    {
        if (!IsOwner) return;
        Move();
    }

    /// <summary>
    /// Captures player input and applies it to the Rigidbody2D as linear velocity.
    /// </summary>
    void Move()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical).normalized;

        rb.linearVelocity = movement * moveSpeed;
    }
}
