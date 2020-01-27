using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLight : MonoBehaviour
{
    public GameObject globalLightDim, globalLightBright;

    public void TurnOffLight()
    {
        globalLightDim.SetActive(true);
        globalLightBright.SetActive(false);
    }

    public void TurnOnLight()
    {
        globalLightDim.SetActive(false);
        globalLightBright.SetActive(true);
        Invoke("TurnOffLight", 3f);
    }
}
