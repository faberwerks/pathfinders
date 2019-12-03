using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// A component to set up initial audio settings.
/// </summary>
public class AudioSetup : MonoBehaviour
{
    public AudioMixer mixer;
    public GameObject audioPlayerManagerPrefab = null;

    // Start is called before the first frame update
    private void Start()
    {
        SetUpAudioPlayerManager();
        SetUpAudioGroup("BGMVol");
        SetUpAudioGroup("SFXVol");
    }

    /// <summary>
    /// A method to check if Audio Group exists and set the mixer value accordingly.
    /// </summary>
    /// <param name="group"></param>
    private void SetUpAudioGroup(string group)
    {
        if (!PlayerPrefs.HasKey(group))
        {
            PlayerPrefs.SetInt(group, 1);
        }

        if (PlayerPrefs.GetInt(group) == 0)
        {
            mixer.SetFloat(group, -80.0f);
        }
        else
        {
            mixer.SetFloat(group, 0.0f);
        }
    }

    /// <summary>
    /// Checks if the Audio Player Manager exists and instantiates it if not.
    /// </summary>
    private void SetUpAudioPlayerManager()
    {
        if (AudioPlayerManager.Instance == null)
        {
            ((GameObject)Instantiate(audioPlayerManagerPrefab)).name = audioPlayerManagerPrefab.name;
        }
    }
}
