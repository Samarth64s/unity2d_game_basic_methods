using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Singleton Instance")]
    public static AudioManager Instance;

    [Header("Audio Clips")]
    [Tooltip("List of background music clips to choose from.")]
    public SoundClip[] backgroundMusicClips;

    [Tooltip("List of sound effect clips to choose from.")]
    public SoundClip[] soundEffectClips;

    [Header("Audio Sources")]
    [Tooltip("Audio source responsible for playing background music.")]
    public AudioSource musicAudioSource;

    [Tooltip("Audio source responsible for playing sound effects.")]
    public AudioSource sfxAudioSource;

    // Called when the script instance is being loaded
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("AudioManager initialized.");
        }
        else
        {
            Debug.LogWarning("Duplicate AudioManager destroyed.");
            Destroy(gameObject);
        }
    }

    // Called before the first frame update
    private void Start()
    {
        PlayBackgroundMusic("Theme");
    }

    // Plays background music by clip name
    public void PlayBackgroundMusic(string clipName)
    {
        SoundClip music = Array.Find(backgroundMusicClips, clip => clip.name == clipName);

        if (music == null)
        {
            Debug.LogWarning($"Music clip '{clipName}' not found!");
            return;
        }

        musicAudioSource.clip = music.audioClip;
        musicAudioSource.Play();
    }

    // Plays a sound effect by clip name
    public void PlaySoundEffect(string clipName)
    {
        SoundClip sfx = Array.Find(soundEffectClips, clip => clip.name == clipName);

        if (sfx == null)
        {
            Debug.LogWarning($"SFX clip '{clipName}' not found!");
            return;
        }

        sfxAudioSource.PlayOneShot(sfx.audioClip);
    }

    // Toggles mute state for background music
    public void ToggleBackgroundMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
    }

    // Toggles mute state for sound effects
    public void ToggleSoundEffect()
    {
        sfxAudioSource.mute = !sfxAudioSource.mute;
    }

    // Sets the volume level for background music
    public void backgroundMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }

    // Sets the volume level for sound effects
    public void soundEffectVolume(float volume)
    {
        sfxAudioSource.volume = volume;
    }
}
