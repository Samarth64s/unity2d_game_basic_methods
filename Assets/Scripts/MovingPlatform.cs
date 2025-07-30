using UnityEngine;

/// <summary>
/// Moves a platform back and forth between two points. 
/// When a player is on the platform, it moves along with it.
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Points")]
    [Tooltip("The starting point of the platform movement.")]
    public Vector3 pointA = new Vector3(-3f, 0f, 0f);

    [Tooltip("The end point of the platform movement.")]
    public Vector3 pointB = new Vector3(3f, 0f, 0f);

    [Header("Movement Settings")]
    [Tooltip("Speed at which the platform moves.")]
    public float speed = 2f;

    // Internal variable to track current target position
    private Vector3 targetPosition;

    void Start()
    {
        // Initialize movement to point B
        targetPosition = pointB;
    }

    void Update()
    {
        // Move platform toward the current target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if platform is close to target, then swap target
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            if (Vector3.Distance(transform.position, pointA) < 0.1f)
                targetPosition = pointB;
            else if (Vector3.Distance(transform.position, pointB) < 0.1f)
                targetPosition = pointA;
        }
    }

    // Attach player to platform when they enter the trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform, true); // Preserve world position
        }
    }

    // Detach player from platform when they exit the trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null, true); // Detach while preserving position
        }
    }
}
