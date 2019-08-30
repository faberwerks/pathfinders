using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A script to manage toggle-able objects.
/// </summary>
/// Do not forget to determine size Object that need to be toggled and put GameObjects in inspector.
public class Toggler : MonoBehaviour
{
    public enum TogglerType
    {
        Normal
    }

    public List<GameObject> toggledObjects;

    public TogglerType togglerType = TogglerType.Normal;

    // Variable used for checking "Triggered-timer" trigger object.
    private bool hasBeenToggled;
   

    private void Start()
    {
        // Variable used for checking "Triggered-timer" trigger object.
        hasBeenToggled = false;
    }

    /// <summary>
    /// A method to change toggle-able objects state to active / inactive. 
    /// </summary>
    public void ToggleObjects()
    {
        for (int i = 0; i < toggledObjects.Count; i++)
        {
            toggledObjects[i].SetActive(!toggledObjects[i].activeInHierarchy);
        }
    }
}
