using UnityEngine;

/// <summary>
/// Component to play audio clips.
/// </summary>
public class AudioPlayer : MonoBehaviour
{
    public AudioSource AudioSource { get; private set; }

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AudioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays an audio clip.
    /// </summary>
    /// <param name="audioClip">Audio clip to be played.</param>
    public void Play(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }
}
