using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component handling toggling obstacles.
/// </summary>
/// Don't forget to set the size (i.e. number of objects) to be toggled in the inspector!
public class Toggler : Interactable
{
    public enum TogglerType
    {
        PressurePlate,
        Lever,
        Timer
    }

    public List<GameObject> toggledObjects;

    public TogglerType togglerType;

    private TriggeredTimer timer = null;
    private bool hasBeenTriggered;  // used to check state of triggered timer

    private void Start()
    {
        hasBeenTriggered = false;

        if (togglerType == TogglerType.Timer)
        {
            timer = gameObject.GetComponent<TriggeredTimer>();
        }   
    }

    // only used for LEVERS
    public override void Interact()
    {
        if (togglerType == TogglerType.Lever)
        {
            ToggleObjects();
        }
    }

    /// <summary>
    /// A method to toggle the state of the toggled objects (active / inactive).
    /// </summary>
    public void ToggleObjects()
    {
        foreach (GameObject toggledObject in toggledObjects)
        {
            toggledObject.SetActive(!toggledObject.activeInHierarchy);
        }
    }

    // only used for PRESSURE PLATES and TIMERS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            switch (togglerType)
            {
                case TogglerType.PressurePlate:
                    ToggleObjects();
                    break;
                case TogglerType.Timer:
                    if (!hasBeenTriggered)
                    {
                        timer.StartTimer();
                        hasBeenTriggered = true;
                    }
                    break;
            }
        }
    }

    // only used for LEVERS
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            if (togglerType == TogglerType.PressurePlate)
            {
                ToggleObjects();
            }
        }
    }
}
