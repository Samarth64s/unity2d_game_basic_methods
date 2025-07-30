using UnityEngine;

public class RigidbodyMovementSystem : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Stores movement direction based on input")]
    Vector2 move;

    [Tooltip("Movement speed of the player")]
    public int speed;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get smooth movement input (values between -1 to 1)
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Flip the object based on horizontal movement
        flip();
    }

    void FixedUpdate()
    {
        // ---------- Movement Methods Summary ----------
        // Uncomment any one of the following lines to use that movement type:

        // 1. MovePosition: Instant movement, frame-independent, ignores inertia
        // rb.MovePosition(rb.position + move * speed * Time.deltaTime);

        // 2. linearVelocity: Directly sets velocity each frame, allows smooth sliding
        // rb.linearVelocity = move * speed;

        // 3. AddForce: Physics-based movement with natural acceleration
        rb.AddForce(move * speed);
    }

    /// <summary>
    /// Flips the character sprite horizontally based on movement direction.
    /// </summary>
    void flip()
    {
        // If moving left, flip to negative scale
        if (move.x < -0.01f) transform.localScale = new Vector3(-6, 6, 1);

        // If moving right, flip to positive scale
        if (move.x > 0.01f) transform.localScale = new Vector3(6, 6, 1);
    }
}
