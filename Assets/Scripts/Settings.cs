using UnityEngine;

public class Settings : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("The canvas GameObject that controls music settings (volume, mute, etc).")]
    public GameObject musicCanvas;

    // Toggles the visibility of the music settings canvas
    public void ToggleMusicCanvas()
    {
        musicCanvas.SetActive(!musicCanvas.activeSelf);
    }
}
