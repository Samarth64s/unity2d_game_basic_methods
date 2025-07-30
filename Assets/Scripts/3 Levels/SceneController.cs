using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // ============================
    // SCENE MANAGEMENT FUNCTIONS
    // ============================

    /// <summary>
    /// Loads a scene by its build index.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to load.</param>
    
    [Tooltip("Loads the scene with the specified build index.")]
    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Restarts the current scene.
    /// </summary>

    [Tooltip("Restarts the currently active scene.")]
    public static void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Loads the next scene if available, otherwise logs 'Coming Soon...'.
    /// </summary>

    [Tooltip("Loads the next scene in build settings if available.")]
    public static void NextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("Coming Soon...");
        }
    }
}
