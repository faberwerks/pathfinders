using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialShowHandler : MonoBehaviour
{
    public Toggle tutorialSettingToggle;
    public AudioClip audioClip;

    private AudioHandler audioHandler;
    private bool tutorialAlwaysOn;
    
    // Start is called before the first frame update
    void Start()
    {
        tutorialAlwaysOn = PlayerPrefs.GetInt("TutorialAlwaysOn", 0) == 1 ? true :  false;
        if(tutorialAlwaysOn)
        {
            tutorialSettingToggle.isOn = true;
        }
        else if(!tutorialAlwaysOn)
        {
            tutorialSettingToggle.isOn = false;
        }
        audioHandler = GetComponent<AudioHandler>();
        tutorialSettingToggle.onValueChanged.AddListener(delegate { PlaySFX(); });
    }

    public void ChangeTutorialState()
    {
        if (tutorialSettingToggle.isOn)
        {
            PlayerPrefs.SetInt("TutorialAlwaysOn", 1);
        }
        else if (!tutorialSettingToggle.isOn)
        {
            PlayerPrefs.SetInt("TutorialAlwaysOn", 0);
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
