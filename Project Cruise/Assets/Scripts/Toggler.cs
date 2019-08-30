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
        Normal,
        triggerTime
    }

    public List<GameObject> toggledObjects;

    public TogglerType togglerType = TogglerType.Normal;

    // Variable used for checking "Triggered-timer" trigger object.
    private bool hasBeenToggled;

    public TriggeredTimer timer;
   

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
        Debug.Log("Works");
    }

    //To check if preasure plate have been pressed or not
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasBeenToggled && togglerType == TogglerType.triggerTime)
        {
            timer.IsActivating();
            hasBeenToggled = true;
        }
    }
}
