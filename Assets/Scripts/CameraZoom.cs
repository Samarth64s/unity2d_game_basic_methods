using UnityEngine;

/// <summary>
/// Dynamically zooms the camera in when the player is idle,
/// and zooms out when the player is moving. Includes a delay
/// before zooming in to avoid instant zoom on brief stops.
/// </summary>
public class CameraZoom : MonoBehaviour
{
    Camera mainCamera;
    Rigidbody2D playerRB;

    bool zoomIn;

    [Header("Zoom Settings")]
    [Tooltip("Target zoom size when player is idle (smaller = more zoomed in)")]
    [Range(2, 10)]
    public float zoomSize = 5f;

    [Tooltip("Speed of the zoom transition (lower = smoother)")]
    [Range(0.01f, 0.1f)]
    public float zoomSpeed = 0.05f;

    [Tooltip("Time in seconds the player must be idle before zooming in")]
    [Range(1, 3)]
    public float waitTime = 1.5f;

    float waitCounter;

    /// <summary>
    /// Initializes references to the main camera and player Rigidbody2D.
    /// </summary>
    private void Awake()
    {
        mainCamera = Camera.main;
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Gradually zooms the camera in to the specified zoom size.
    /// </summary>
    void ZoomIn()
    {
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoomSize, zoomSpeed);
    }

    /// <summary>
    /// Gradually zooms the camera out to default size (10).
    /// </summary>
    void ZoomOut()
    {
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 10, zoomSpeed);
    }

    /// <summary>
    /// Handles zoom behavior based on player movement each frame.
    /// </summary>
    private void LateUpdate()
    {
        // Get the current movement speed of the player
        float speed = playerRB.linearVelocity.magnitude;

        if (speed < 0.1f) // If the player is idle
        {
            waitCounter += Time.deltaTime;

            // Start zooming in after wait time
            if (waitCounter > waitTime)
            {
                zoomIn = true;
            }
        }
        else
        {
            // Player is moving, reset zoom and counter
            zoomIn = false;
            waitCounter = 0;
        }

        // Apply zoom based on movement state
        if (zoomIn)
            ZoomIn();
        else
            ZoomOut();
    }
}
