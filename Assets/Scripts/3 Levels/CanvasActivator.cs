using UnityEngine;

/// <summary>
/// Activates a UI canvas when the player enters a 2D trigger area.
/// </summary>
public class CanvasActivator : MonoBehaviour
{
    [Tooltip("Assign the canvas you want to activate.")]
    public GameObject canvasToActivate;

    // Called when another Collider2D enters the trigger (2D only)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasToActivate.SetActive(true);  // Show the canvas
            AudioManager.Instance.PlaySoundEffect("Mission Completed");
            AudioManager.Instance.musicAudioSource.Stop();
        }
    }
}
