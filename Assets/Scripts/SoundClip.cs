using UnityEngine;

[System.Serializable]
public class SoundClip
{
    public string name;         // Name used to find this clip (e.g., "Jump", "Explosion")
    public AudioClip audioClip; // Actual audio clip to be played
}
