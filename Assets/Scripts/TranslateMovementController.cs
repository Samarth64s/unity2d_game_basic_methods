using UnityEngine;

/// <summary>
/// Controls 2D movement using input axes and flips the sprite based on direction.
/// </summary>
public class MovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Stores movement direction based on input")]
    Vector2 move;

    [Tooltip("Movement speed of the player")]
    public int speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialization logic (if needed)
    }

    // Update is called once per frame
    void Update()
    {
        // Get smooth movement input (values between -1 to 1)
      //  move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        move = new Vector2(Input.GetAxis("Horizontal"),0);


        // Move the object in world space based on input direction and speed
        transform.Translate(move * speed * Time.deltaTime, Space.World);
        //transform.position += new Vector3(move.x, move.y, 0) * speed * Time.deltaTime;


        // Flip the object based on horizontal movement
        flip();
    }

    /// <summary>
    /// Flips the character sprite horizontally based on movement direction.
    /// </summary>
    void flip()
    {
        // If moving left, flip to negative scale
        if (move.x < -0.01f) transform.localScale = new Vector3(-4, 4, 1);

        // If moving right, flip to positive scale
        if (move.x > 0.01f) transform.localScale = new Vector3(4, 4, 1);
    }
}
