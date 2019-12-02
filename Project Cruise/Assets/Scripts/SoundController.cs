using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// A component to handle sound setting.
/// </summary>
public class SoundController : MonoBehaviour
{
    public AudioMixer mixer;
    public Image BGMSprite;
    public Image SFXSprite;
    public Sprite[] toggleSprite = new Sprite[2];

    private float volume;
    private bool BGMOn;
    private bool SFXOn;

    private void Start()
    {
        BGMOn = PlayerPrefs.GetInt("BGMVol") == 1 ? true : false;
        SFXOn = PlayerPrefs.GetInt("SFXVol") == 1 ? true : false;
        ToggleSprite("BGMVol", BGMOn);
        ToggleSprite("SFXVol", SFXOn);
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
            ToggleSprite(group, false);
        }
        else
        {
            mixer.SetFloat(group, 0.0f);
            PlayerPrefs.SetInt(group, 1);
            ToggleSprite(group, true);
        }
    }

    private void ToggleSprite(string group, bool isOn)
    {
        if (group == "BGMVol")
        {
            if(isOn) BGMSprite.sprite = toggleSprite[1];
            else BGMSprite.sprite = toggleSprite[0];
        }
        if(group == "SFXVol")
        {
            if (isOn) SFXSprite.sprite = toggleSprite[1];
            else SFXSprite.sprite = toggleSprite[0];
        }
    }
}