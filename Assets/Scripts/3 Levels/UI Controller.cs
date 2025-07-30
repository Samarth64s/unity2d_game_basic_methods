using UnityEngine;

/// <summary>
/// Controls UI button events to trigger scene transitions.
/// </summary>
public class UIController : MonoBehaviour
{

    [Tooltip("Loads scene with index 1 (e.g., Play button).")]
    public void Play()
    {
        SceneController.LoadScene(1);
    }

    [Tooltip("Restarts the current scene.")]
    public void Restart()
    {
        SceneController.Restart();
    }

    [Tooltip("Loads the next scene in the build order.")]
    public void NextLevel()
    {
        SceneController.NextLevel();
    }

    [Tooltip("Loads a scene by its build index.")]
    public void SceneLoad(int sceneIndex)
    {
        SceneController.LoadScene(sceneIndex);
    }
}
