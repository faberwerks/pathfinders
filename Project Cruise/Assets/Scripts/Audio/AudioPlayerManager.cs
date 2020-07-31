using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that manages all Audio Players.
/// </summary>
public class AudioPlayerManager : MonoBehaviour
{
    public static AudioPlayerManager Instance { get; private set; }
    public GameObject audioPlayerPrefab = null;
    public GameObject bgmPlayerPrefab = null;
    private List<AudioPlayer> audioPlayers;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        audioPlayers = new List<AudioPlayer>();
        PlayBGM();
    }

    /// <summary>
    /// Checks whether there is an available Audio Player to play the clip and instantiates a new one if not.
    /// </summary>
    /// <param name="audioClip">Audio Clip to be played.</param>
    public void Play(AudioClip audioClip)
    {
        foreach (AudioPlayer audioPlayer in audioPlayers)
        {
            if (!audioPlayer.AudioSource.isPlaying)
            {
                audioPlayer.Play(audioClip);
                return;
            }
        }

        GameObject newAudioPlayer = (GameObject)Instantiate(audioPlayerPrefab);
        newAudioPlayer.transform.SetParent(transform);
        audioPlayers.Add(newAudioPlayer.GetComponent<AudioPlayer>());
        newAudioPlayer.GetComponent<AudioPlayer>().Play(audioClip);
        return;
    }

    /// <summary>
    /// Intantiates the BGM Player.
    /// </summary>
    public void PlayBGM()
    {
        GameObject bgmPlayer = (GameObject)Instantiate(bgmPlayerPrefab);
        bgmPlayer.transform.SetParent(transform);
    }
}
