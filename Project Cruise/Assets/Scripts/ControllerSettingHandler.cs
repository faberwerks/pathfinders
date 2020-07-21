using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSettingHandler : MonoBehaviour
{

    public Toggle defaultToggle;
    public Toggle classicToggle;

    private void Start()
    {
        switch (PlayerPrefs.GetInt("ClassicController", 0) == 1 ? true : false)
        {
            case true:
                //Debug.Log("use classic");
                classicToggle.isOn = true;
                break;
            case false:
                //Debug.Log("use default");
                defaultToggle.isOn = true;
                break;
        }
        //classicToggle.isOn = true;
    }

    // since it's a Toggle Group, changing one of the two will affect the other and Toggle callback is OnValueChange (anytime changed called), 
    // so only 1 callback needed
    // ie. when the Classic is turned on, the Default will be *changed* to false thus calling the callback in the Default toggle
    public void onDefaultChanged(bool status)
    {
        //Debug.Log("default stat: " + status);
        if (status) //if DefaultToggle is on, set playerpref to 0/false
        {
            PlayerPrefs.SetInt("ClassicController", 0);
        }
        else
        {
            PlayerPrefs.SetInt("ClassicController", 1);
        }
    }
}
