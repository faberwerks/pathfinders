using UnityEngine;

/// <summary>
/// Component to send a request to the Audio Player Manager to play an audio clip.
/// </summary>
public class AudioHandler : MonoBehaviour
{
    /// <summary>
    /// Sends an audio clip to the Audio Player Manager to play.
    /// </summary>
    /// <param name="audioClip">Audio clip to be played.</param>
    public void Play(AudioClip audioClip)
    {
        AudioPlayerManager.Instance.Play(audioClip);
    }
}
