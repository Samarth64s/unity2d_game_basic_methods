using UnityEngine;

/// <summary>
/// CameraController smoothly follows the assigned target (usually the player)
/// with a specified offset and smoothing factor. It ensures the camera transitions
/// fluidly from one position to another, enhancing gameplay visuals.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Camera Target")]
    [Tooltip("The target the camera should follow (usually the player).")]
    public Transform target;

    [Header("Camera Offset")]
    [Tooltip("Offset from the target's position to keep the camera away.")]
    public Vector3 positionOffset;

    [Header("Smoothing")]
    [Tooltip("Smoothing factor for camera movement.")]
    public float smooth = 1f;

    // Called after all Update functions have been called
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + positionOffset;

        // Only follow target's X position, keep camera's current Y and Z
        Vector3 newPosition = new Vector3(targetPosition.x, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, smooth * Time.deltaTime);
    }

}
