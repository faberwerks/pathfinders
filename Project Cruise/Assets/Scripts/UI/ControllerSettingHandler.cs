using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component to handle controller type settings.
/// </summary>
public class ControllerSettingHandler : MonoBehaviour
{
    public Toggle defaultToggle;
    public Toggle classicToggle;
    public AudioClip audioClip;

    private AudioHandler audioHandler;

    private void Start()
    {
        switch (PlayerPrefs.GetInt("ClassicController", 0) == 1 ? true : false)
        {   
            //  True    : Turn CLASSIC On
            //  False   : Turn Default On
            case true:
                classicToggle.isOn = true;
                break;
            case false:
                defaultToggle.isOn = true;
                break;
        }
        audioHandler = GetComponent<AudioHandler>();

        // Only adding to one since there are only 2 options for controllers.
        // So when one changed, there other will DEFINITELY changed, so only 1 sound is played.
        // In case of having >2 options in the Toggle group, this needs to be refactored, so does OnDefaultChanged().
        defaultToggle.onValueChanged.AddListener(delegate { PlaySFX(); });
    }

    /// <summary>
    /// Callback method called after changing the value of the Default Controller toggle setting.
    /// </summary>
    /// Since it's a Toggle Group, changing one will affect the other
    /// And toggle's callback is OnValueChange, called every time value changes
    /// So only 1 callback is needed
    /// i.e. When Classic is turned on, the Default will be changed to off/false thus calling the callback in the Default toggle
    /// <param name="status"></param>
    public void OnDefaultChanged(bool status)
    {
        // if DefaultToggle is on, set PlayerPrefs to 0/false
        if (status)
        {
            PlayerPrefs.SetInt("ClassicController", 0);
        }
        else
        {
            PlayerPrefs.SetInt("ClassicController", 1);
        }
    }

    /// <summary>
    /// Callback method to play the SFX.
    /// </summary>
    private void PlaySFX()
    {
        audioHandler.Play(audioClip);
    }
}
