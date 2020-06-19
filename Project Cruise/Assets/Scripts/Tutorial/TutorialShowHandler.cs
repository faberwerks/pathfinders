using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialShowHandler : MonoBehaviour
{
    private bool tutorialAlwaysOn;
    public Toggle tutorialSettingToggle;
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
}
