using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Component to play background music.
/// </summary>
public class BGMAudioPlayer : AudioPlayer
{
    [SerializeField]
    private AudioClip mainMenuBGM = null;
    [SerializeField]
    private AudioClip inGameBGM = null;
    [SerializeField]
    private List<string> menuScenes;

    protected override void Awake()
    {
        base.Awake();
        PlayBGM(mainMenuBGM);
        SceneManager.activeSceneChanged += CheckSceneName;
    }

    /// <summary>
    /// Checks the scene name and changes the BGM played if necessary.
    /// </summary>
    private void CheckSceneName(Scene current, Scene next)
    {
        if (menuScenes.Contains(next.name))
        {
            //Debug.Log("[BGMAudioPlayer] Menu Scene: " + next.name);
            if (AudioSource.clip != mainMenuBGM)
            {
                PlayBGM(mainMenuBGM);
            }
        }
        else
        {
            //Debug.Log("[BGMAudioPlayer] Not Menu Scene: " + next.name);
            if (AudioSource.clip != inGameBGM)
            {
                PlayBGM(inGameBGM);
            }
        }
    }

    /// <summary>
    /// Play a BGM.
    /// </summary>
    /// <param name="bgm">BGM to be played.</param>
    private void PlayBGM(AudioClip bgm)
    {
        AudioSource.clip = bgm;
        AudioSource.Play();
    }
}
