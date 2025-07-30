using UnityEngine;
using UnityEngine.UI;

public class MusicCanvasControler : MonoBehaviour
{
    [Header("UI Sliders")]
    [Tooltip("Slider to control background music volume.")]
    public Slider musicSlider;

    [Tooltip("Slider to control sound effect volume.")]
    public Slider sfxSlider;

    // Called whenever the canvas becomes active/enabled
    private void OnEnable()
    {
        // Initialize slider values with the current volume levels from AudioManager
        musicSlider.value = AudioManager.Instance.musicAudioSource.volume;
        sfxSlider.value = AudioManager.Instance.sfxAudioSource.volume;
    }

    // Toggle background music on/off
    public void ToggleBackgroundMusic()
    {
        AudioManager.Instance.ToggleBackgroundMusic();
    }

    // Toggle sound effects on/off
    public void ToggleSoundEffect()
    {
        AudioManager.Instance.ToggleSoundEffect();
    }

    // Called when background music slider value changes
    public void backgroundMusicSlider()
    {
        AudioManager.Instance.backgroundMusicVolume(musicSlider.value);
    }

    // Called when sound effect slider value changes
    public void soundEffectSlider()
    {
        AudioManager.Instance.soundEffectVolume(sfxSlider.value);
    }

    // Hides the music settings canvas
    public void closeCanvas()
    {
        gameObject.SetActive(false);
    }
}
