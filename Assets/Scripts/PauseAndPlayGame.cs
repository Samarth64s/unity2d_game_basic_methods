using UnityEngine;
using UnityEngine.UI;

public class PauseAndPlayGame : MonoBehaviour
{
    [Header("UI Buttons")]
    [Tooltip("Button used to pause the game")]
    public GameObject pauseButton;

    [Tooltip("Button used to resume the game")]
    public GameObject playButton;

    // Tracks whether the game is currently paused
    private bool isPaused = false;

    /// <summary>
    /// Pauses the game by setting Time.timeScale to 0
    /// Hides the pause button and shows the play button
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseButton.SetActive(false);
        playButton.SetActive(true);
    }

    /// <summary>
    /// Resumes the game by setting Time.timeScale to 1
    /// Hides the play button and shows the pause button
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseButton.SetActive(true);
        playButton.SetActive(false);
    }
}
