using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// A component to handle sound setting.
/// </summary>
public class SoundController : MonoBehaviour
{
    public AudioMixer mixer;
    private float volume;
    private bool BGMOn;
    private bool SFXOn;

    private void Start()
    {
        BGMOn = PlayerPrefs.GetInt("BGMVol") == 1 ? true : false;
        SFXOn = PlayerPrefs.GetInt("SFXVol") == 1 ? true : false;
        //SET THE ICON ASSET
    }

    /// <summary>
    /// Toggles an AudioGroup on and off.
    /// </summary>
    /// <param name="group"></param>
    public void ToggleSound(string group)
    {
        if (PlayerPrefs.GetInt(group) == 1)
        {
            mixer.SetFloat(group, -80.0f);
            PlayerPrefs.SetInt(group, 0);
        }
        else
        {
            mixer.SetFloat(group, 0.0f);
            PlayerPrefs.SetInt(group, 1);
        }
    }
}
