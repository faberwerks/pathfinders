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

    public AudioClip toggleSound;

    public List<GameObject> toggledObjects;

    public TogglerType togglerType;

    public List<Sprite> toggleSprites;

    private TriggeredTimer timer = null;
    private SpriteRenderer sprRend = null;
    private AudioHandler audioHandler = null;
    private int maxIndex = 0;
    private int spriteIndex;
    private bool hasBeenTriggered;  // used to check state of triggered timer

    private void Start()
    {
        hasBeenTriggered = false;

        if (togglerType == TogglerType.Timer)
        {
            timer = gameObject.GetComponent<TriggeredTimer>();
        }

        sprRend = GetComponent<SpriteRenderer>();

        audioHandler = GetComponent<AudioHandler>();

        maxIndex = toggleSprites.Count;
        spriteIndex = 0;
    }

    // only used for LEVERS and TRIGGERED TIMERS
    public override void Interact()
    {
        switch (togglerType)
        {
            case TogglerType.Lever:
                ToggleObjects();
                Blackboard.Instance.LevelManager.SaveCheckpoint();
                break;
        }
    }

    /// <summary>
    /// A method to toggle the state of the toggled objects (active / inactive).
    /// </summary>
    public void ToggleObjects()
    {
        if (toggleSound)
        {
            // triggered timers only play the sound once
            if (togglerType == TogglerType.Timer)
            {
                if (!hasBeenTriggered)
                {
                    audioHandler.Play(toggleSound);
                }
            }
            else
            {
                audioHandler.Play(toggleSound);
            }
        }
       
        foreach (GameObject toggledObject in toggledObjects)
        {
            toggledObject.SetActive(!toggledObject.activeInHierarchy);
        }

        if (maxIndex > 0)
        {
            sprRend.sprite = toggleSprites[++spriteIndex % maxIndex];
        }
    }

    // only used for PRESSURE PLATES
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (togglerType)
        {
            case TogglerType.PressurePlate:
                ToggleObjects();
                Blackboard.Instance.LevelManager.SaveCheckpoint();
                break;
            case TogglerType.Timer:
                if (!hasBeenTriggered)
                {
                    ToggleObjects();
                    Blackboard.Instance.LevelManager.SaveCheckpoint();
                    timer.StartTimer();
                    hasBeenTriggered = true;
                }
                break;
        }
    }

    // only used for PRESSURE PLATES
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
