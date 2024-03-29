﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioHandler))]
/// <summary>
/// A component for key-triggered doors.
/// </summary>
public class Door : MonoBehaviour
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private AudioClip unlockSound;
    private AudioHandler audioHandler;

    //added relic that works like in Toggler.cs
    public GameObject relic;

    public int ID { get { return id; } }

    private void Awake()
    {
        audioHandler = GetComponent<AudioHandler>();
    }

    //protected void Start()
    //{
    //    base.Start();
    //}

    public void Interact()
    {
        Blackboard.Instance.LevelManager.SaveCheckpoint();
        if (audioHandler && unlockSound)
        {
            audioHandler.Play(unlockSound);
        }

        //to activate relic
        if (relic)
        {
            relic.SetActive(true);
        }

        Destroy(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    AddCharacter(collision);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    RemoveCharacter(collision);
    //}
}
