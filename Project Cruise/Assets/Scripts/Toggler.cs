using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A script to manage toogleObjects
/// Do not forget to determine size Object that need to be toogled and put GameObjects in inspector
/// </summary>
public class Toggler : MonoBehaviour
{
    public enum TogglerType
    {
        NORMAL
    }

    public List<GameObject> toggledObjects;

    public TogglerType togglerType = TogglerType.NORMAL;

   // private bool hasBeenToggled;

    private void Start()
    {
        //hasBeenToggled = false;
    }

    public void ToggleObjects()
    {
        Debug.Log("Lever: ToggleObjects called.");
        for (int i = 0; i < toggledObjects.Count; i++)
        {
            toggledObjects[i].SetActive(!toggledObjects[i].activeInHierarchy);
        }
    }
}
